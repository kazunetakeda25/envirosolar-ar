/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Inputs {

    using AOT;
    using UnityEngine;
    using UnityEngine.Rendering;
    using System;
    using System.Runtime.InteropServices;
    using Clocks;
    using Internal;

    /// <summary>
    /// Recorder input for recording video from RenderTextures
    /// </summary>
    [Doc(@"RenderTextureInput")]
    public sealed class RenderTextureInput : IDisposable {

        #region --Client API--

        /// <summary>
        /// Create a video recording input for RenderTextures
        /// </summary>
        /// <param name="mediaRecorder">Media recorder to receive committed frames</param>
        /// <param name="clock">Clock for generating timestamps</param>
        [Doc(@"RenderTextureInputCtor")] // DOC
        public RenderTextureInput (IMediaRecorder mediaRecorder, IClock clock) {
            // Construct state
            this.mediaRecorder = mediaRecorder;
            this.clock = clock;
            this.pixelBuffer = new byte[mediaRecorder.pixelWidth * mediaRecorder.pixelHeight * 4];
            this.readbackBuffer = new Texture2D(mediaRecorder.pixelWidth, mediaRecorder.pixelHeight, TextureFormat.RGBA32, false, false);
            this.readbackiOS = MTLReadbackCreate(mediaRecorder.pixelWidth, mediaRecorder.pixelHeight);
        }

        /// <summary>
        /// Stop recorder input and teardown resources
        /// </summary>
        [Doc(@"CameraInputDispose")]
        public void Dispose () {
            Texture2D.Destroy(readbackBuffer);
            lock (this)
                disposed = true;
            using (var dispatcher = new RenderDispatcher())
                dispatcher.Dispatch(() => MTLReadbackDispose(readbackiOS));
        }

        /// <summary>
        /// Commit a RenderTexture for encoding
        /// </summary>
        [Doc(@"RenderTextureInputCommitFrame")] // DOC
        public void CommitFrame (RenderTexture framebuffer) {
            if (Application.platform == RuntimePlatform.Android)
                CommitAndroid(framebuffer);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                CommitiOS(framebuffer);
            else if (SystemInfo.supportsAsyncGPUReadback)
                CommitAsync(framebuffer);
            else
                CommitSync(framebuffer);
        }
        #endregion


        #region --Operations--

        private readonly IMediaRecorder mediaRecorder;
        private readonly IClock clock;
        private readonly byte[] pixelBuffer;
        private readonly Texture2D readbackBuffer;
        private readonly IntPtr readbackiOS;
        private bool disposed;

        private void CommitAndroid (RenderTexture framebuffer) {
            /*
             * Because Android is terrible at everything, we have to *break* the IMediaRecorder contract and directly
             * send the framebuffer texture to the native recorder for encoding. We wouldn't have to do this if 
             * Android OEM's properly implemented PBO's, WHICH SHOULD NOT BLOCK with `glReadPixels`. So infuriating.
             */
            var nativeRecorder = ((mediaRecorder as IAbstractRecorder).recorder as MediaRecorderAndroid).recorder;
            var textureID = framebuffer.GetNativeTexturePtr().ToInt32();
            var timestamp = clock.Timestamp;
            if (mediaRecorder is MP4Recorder || mediaRecorder is HEVCRecorder) // GIFRecorder can't use this trick
                using (var dispatcher = new RenderDispatcher())
                    dispatcher.Dispatch(() => {
                        AndroidJNI.AttachCurrentThread();
                        nativeRecorder.Call(@"encodeFrame", textureID, timestamp);
                    });
            else
                CommitSync(framebuffer); // For GIFRecorder, do standard readback
        }

        private void CommitiOS (RenderTexture framebuffer) {
            var _ = framebuffer.colorBuffer;
            var texturePtr = framebuffer.GetNativeTexturePtr();
            var timestamp = clock.Timestamp;
            Action<IntPtr> commitFrame = pixelBuffer => {
                lock (this)
                    if (!disposed)
                        mediaRecorder.CommitFrame(pixelBuffer, timestamp);
            };
            using (var dispatcher = new RenderDispatcher())
                dispatcher.Dispatch(() => MTLReadbackReadback(readbackiOS, texturePtr, OnReadback, (IntPtr)GCHandle.Alloc(commitFrame, GCHandleType.Normal)));
        }

        private void CommitSync (RenderTexture framebuffer) {
            RenderTexture.active = framebuffer;
            readbackBuffer.ReadPixels(new Rect(0, 0, readbackBuffer.width, readbackBuffer.height), 0, 0, false);
            readbackBuffer.GetRawTextureData<byte>().CopyTo(pixelBuffer);
            mediaRecorder.CommitFrame(pixelBuffer, clock.Timestamp);
        }

        private void CommitAsync (RenderTexture framebuffer) {
            var timestamp = clock.Timestamp;
            AsyncGPUReadback.Request(framebuffer, 0, request => {
                request.GetData<byte>().CopyTo(pixelBuffer);
                if (!disposed) // No need to synchronize, called on main thread
                    mediaRecorder.CommitFrame(pixelBuffer, timestamp);
            });
        }
        #endregion


        #region --Bridge--

        #if UNITY_IOS && !UNITY_EDITOR
        [DllImport(@"__Internal", EntryPoint = @"NCMTLReadbackCreate")]
        private static extern IntPtr MTLReadbackCreate (int width, int height);
        [DllImport(@"__Internal", EntryPoint = @"NCMTLReadbackDispose")]
        private static extern void MTLReadbackDispose (IntPtr readback);
        [DllImport(@"__Internal", EntryPoint = @"NCMTLReadbackReadback")]
        private static extern void MTLReadbackReadback (IntPtr readback, IntPtr texture, Action<IntPtr, IntPtr> handler, IntPtr context);
        #else
        private static IntPtr MTLReadbackCreate (int width, int height) { return IntPtr.Zero; }
        private static void MTLReadbackDispose (IntPtr readback) {}
        private static void MTLReadbackReadback (IntPtr readback, IntPtr texture, Action<IntPtr, IntPtr> handler, IntPtr context) {}
        #endif

        [MonoPInvokeCallback(typeof(Action<IntPtr, IntPtr>))]
        private static void OnReadback (IntPtr context, IntPtr pixelBuffer) {
            var handle = (GCHandle)context;
            Action<IntPtr> commitFrame = handle.Target as Action<IntPtr>;
            handle.Free();
            commitFrame(pixelBuffer);
        }
        #endregion
    }
}