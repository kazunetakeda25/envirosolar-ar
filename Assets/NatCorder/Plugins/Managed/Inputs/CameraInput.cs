/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Inputs {

    using UnityEngine;
    using System;
    using System.Collections;
    using Clocks;
    using Internal;

    /// <summary>
    /// Recorder input for recording video one or more game cameras
    /// </summary>
    [Doc(@"CameraInput")]
    public sealed class CameraInput : IDisposable {

        #region --Client API--
        /// <summary>
        /// Control number of successive camera frames to skip while recording.
        /// This is very useful for GIF recording, which typically has a lower framerate appearance
        /// </summary>
        [Doc(@"CameraInputFrameSkip", @"CameraInputFrameSkipDiscussion")]
        public int frameSkip;

        /// <summary>
        /// Create a video recording input from a game camera
        /// </summary>
        /// <param name="mediaRecorder">Media recorder to receive committed frames</param>
        /// <param name="clock">Clock for generating timestamps</param>
        /// <param name="cameras">Game cameras to record</param>
        [Doc(@"CameraInputCtor")] // DOC
        public CameraInput (IMediaRecorder mediaRecorder, IClock clock, params Camera[] cameras) {
            // Save state
            this.frameInput = new RenderTextureInput(mediaRecorder, clock); // async??
            this.cameras = cameras;
            this.frameDescriptor = new RenderTextureDescriptor(mediaRecorder.pixelWidth, mediaRecorder.pixelHeight, RenderTextureFormat.Default, 24);
            this.frameDescriptor.sRGB = true;
            this.frameHelper = cameras[0].gameObject.AddComponent<CameraInputAttachment>();
            // Start recording
            frameHelper.StartCoroutine(OnFrame());
        }

        /// <summary>
        /// Stop recorder input and teardown resources
        /// </summary>
        [Doc(@"CameraInputDispose")]
        public void Dispose () {
            CameraInputAttachment.Destroy(frameHelper);
            frameInput.Dispose();
        }
        #endregion


        #region --Operations--

        private readonly RenderTextureInput frameInput;
        private readonly Camera[] cameras;
        private readonly RenderTextureDescriptor frameDescriptor;
        private readonly CameraInputAttachment frameHelper;
        private int frameCount;

        private IEnumerator OnFrame () {
            var yielder = new WaitForEndOfFrame();
            for (;;) {
                // Check frame index
                yield return yielder;
                var recordFrame = frameCount++ % (frameSkip + 1) == 0;
                if (recordFrame) {
                    // Acquire frame
                    var framebuffer = RenderTexture.GetTemporary(frameDescriptor);
                    // Render every camera
                    for (var i = 0; i < cameras.Length; i++) {
                        var prevActive = RenderTexture.active;
                        var prevTarget = cameras[i].targetTexture;
                        cameras[i].targetTexture = framebuffer;
                        cameras[i].Render();
                        cameras[i].targetTexture = prevTarget;
                        RenderTexture.active = prevActive;
                    }
                    // Commit frame
                    frameInput.CommitFrame(framebuffer);
                    RenderTexture.ReleaseTemporary(framebuffer);
                }
            }
        }

        private sealed class CameraInputAttachment : MonoBehaviour { }
        #endregion
    }
}