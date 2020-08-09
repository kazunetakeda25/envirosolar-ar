/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Internal {

    using UnityEngine;
    using UnityEngine.Scripting;
    using System;
    using System.Runtime.InteropServices;

    public sealed class MediaRecorderAndroid : AndroidJavaProxy, IMediaRecorder {
        
        #region --IMediaRecorder--

        public int pixelWidth { get; private set; }

        public int pixelHeight { get; private set; }

        public MediaRecorderAndroid (AndroidJavaObject recorder, int width, int height, string recordingPath, Action<string> callback) : base(@"com.yusufolokoba.natcorder.MediaRecorder$Callback") {
            this.recorder = recorder;
            this.pixelWidth = width;
            this.pixelHeight = height;
            this.callback = callback;
            // Start recording
            recorder.Call(@"startRecording", recordingPath, this);
            // Create commit pixel buffer
            using (var ByteBuffer = new AndroidJavaClass("java.nio.ByteBuffer"))
                using (var ByteOrder = new AndroidJavaClass("java.nio.ByteOrder"))
                    using (var nativeOrder = ByteOrder.CallStatic<AndroidJavaObject>("nativeOrder"))
                        using (var pixelBuffer = ByteBuffer.CallStatic<AndroidJavaObject>("allocateDirect", width * height * 4))
                            this.nativeBuffer = pixelBuffer.Call<AndroidJavaObject>("order", nativeOrder);
        }

        public void Dispose () {
            AndroidJNI.AttachCurrentThread();
            recorder.Call(@"stopRecording");
            recorder.Dispose();
            nativeBuffer.Dispose();
        }
        
        public void CommitFrame<T> (T[] pixelBuffer, long timestamp) where T : struct {
            var bufferHandle = GCHandle.Alloc(pixelBuffer, GCHandleType.Pinned);
            CommitFrame(bufferHandle.AddrOfPinnedObject(), timestamp);
            bufferHandle.Free();            
        }

        public void CommitFrame (IntPtr pixelBuffer, long timestamp) {
            AndroidJNI.AttachCurrentThread();
            var dstPtr = ByteBufferAddress(nativeBuffer.GetRawObject());
            memcpy(dstPtr, pixelBuffer, (UIntPtr)(pixelWidth * pixelHeight * 4));
            using (var clearedBuffer = nativeBuffer.Call<AndroidJavaObject>("clear"))
                recorder.Call(@"encodeFrame", clearedBuffer, timestamp);
        }

        public void CommitSamples (float[] sampleBuffer, long timestamp) {
            AndroidJNI.AttachCurrentThread();
            recorder.Call(@"encodeSamples", sampleBuffer, timestamp);
        }
        #endregion


        #region --Operations--

        public readonly AndroidJavaObject recorder; // Used by RenderTextureInput to sidestep readback
        private readonly Action<string> callback;
        private readonly AndroidJavaObject nativeBuffer;

        [Preserve]
        private void onRecording (string path) {
            callback(path);
        }
        #endregion


        #region --Utility--

        #if UNITY_ANDROID && !UNITY_EDITOR
        [DllImport(@"c")]
        private static extern IntPtr memcpy (IntPtr dst, IntPtr src, UIntPtr size);
        [DllImport(@"NatRender", EntryPoint = @"NRByteBufferAddress")]
        private static extern IntPtr ByteBufferAddress (IntPtr buffer);
        #else
        private static IntPtr memcpy (IntPtr dst, IntPtr src, UIntPtr size) { return IntPtr.Zero; }
        private static IntPtr ByteBufferAddress (IntPtr buffer) { return IntPtr.Zero; }
        #endif
        #endregion
    }
}