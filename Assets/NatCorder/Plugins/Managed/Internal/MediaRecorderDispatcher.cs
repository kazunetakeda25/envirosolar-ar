/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Internal {

    using AOT;
    using UnityEngine;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public interface IDispatcher : IDisposable {
        void Dispatch (Action workload);
    }

    public class MainDispatcher : IDispatcher { // Must be constructed on the Unity main thread

        private readonly DispatcherAttachment attachment;

        public MainDispatcher () {
            this.attachment = new GameObject("NatCorder Dispatch Utility").AddComponent<DispatcherAttachment>();
        }

        public void Dispose () {
            attachment.Dispose();
        }

        public void Dispatch (Action workload) {
            attachment.Dispatch(workload);
        }
        
        private class DispatcherAttachment : MonoBehaviour, IDispatcher {
            
            private readonly Queue<Action> queue = new Queue<Action>();

            public void Dispose () {
                Destroy(this);
                Destroy(this.gameObject);
                queue.Clear();
            }

            public void Dispatch (Action workload) {
                lock ((queue as ICollection).SyncRoot)
                    queue.Enqueue(workload);
            }

            void Awake () {
                DontDestroyOnLoad(this.gameObject);
                DontDestroyOnLoad(this);
            }

            void Update () {
                for (;;)
                    lock ((queue as ICollection).SyncRoot)
                        if (queue.Count > 0)
                            queue.Dequeue()();
                        else
                            break;
            }
        }
    }

    public class RenderDispatcher : IDispatcher {

        public void Dispose () { } // Nop

        public void Dispatch (Action workload) {
            GL.IssuePluginEvent(renderDelegateHandle, ((IntPtr)GCHandle.Alloc(workload, GCHandleType.Normal)).ToInt32());
        }

        static RenderDispatcher () {
            renderDelegateHandle = Marshal.GetFunctionPointerForDelegate((UnityRenderingEvent)DequeueRender); 
        }

        private static readonly IntPtr renderDelegateHandle;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void UnityRenderingEvent (int context);

        [MonoPInvokeCallback(typeof(UnityRenderingEvent))]
        private static void DequeueRender (int context) {
            GCHandle handle = (GCHandle)(IntPtr)context;
            Action target = handle.Target as Action;
            handle.Free();
            target();
        }
    }
}