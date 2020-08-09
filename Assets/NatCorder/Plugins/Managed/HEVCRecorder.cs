/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder {

    using UnityEngine;
    using System;
    using System.IO;
    using Internal;

    /// <summary>
    /// EXPERIMENTAL. HEVC video recorder.
    /// </summary>
    [Doc(@"HEVCRecorder")]
    public sealed class HEVCRecorder : IMediaRecorder, IAbstractRecorder { // EXPERIMENTAL

        #region --Client API--
        /// <summary>
        /// Video width
        /// </summary>
        [Doc(@"PixelWidth")]
        public int pixelWidth {
            get { return recorder.pixelWidth; }
        }
        
        /// <summary>
        /// Video height
        /// </summary>
        [Doc(@"PixelHeight")]
        public int pixelHeight {
            get { return recorder.pixelHeight; }
        }

        /// <summary>
        /// Create a HEVC recorder
        /// </summary>
        /// <param name="videoWidth"></param>
        /// <param name="videoHeight"></param>
        /// <param name="videoFramerate"></param>
        /// <param name="audioSampleRate">Audio sample rate. Pass 0 for no audio.</param>
        /// <param name="audioChannelCount">Audio channel count. Pass 0 for no audio.</param>
        /// <param name="recordingCallback">Recording callback</param>
        /// <param name="videoBitrate">Video bitrate in bits per second</param>
        /// <param name="videoKeyframeInterval">Keyframe interval in seconds</param>
        [Doc(@"HEVCRecorderCtor")]
        public HEVCRecorder (int videoWidth, int videoHeight, int videoFramerate, int audioSampleRate, int audioChannelCount, Action<string> recordingCallback, int videoBitrate = (int)(960 * 540 * 11.4f), int videoKeyframeInterval = 3) {
            var recordingDirectory = Application.persistentDataPath;
            var recordingFilename = string.Format("recording_{0}.mp4", DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff"));
            switch (Application.platform) {
                case RuntimePlatform.OSXEditor:
                    recordingDirectory = Directory.GetCurrentDirectory();
                    goto case RuntimePlatform.OSXPlayer;
                case RuntimePlatform.WindowsEditor:
                    recordingDirectory = Directory.GetCurrentDirectory();
                    goto case RuntimePlatform.WindowsPlayer;
                case RuntimePlatform.OSXPlayer:
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.IPhonePlayer: {
                    var recordingPath = Path.Combine(recordingDirectory, recordingFilename);
                    var nativeRecorder = MediaRecorderBridge.CreateHEVCRecorder(videoWidth, videoHeight, videoFramerate, videoBitrate, videoKeyframeInterval, audioSampleRate, audioChannelCount);
                    this.recorder = new MediaRecorderiOS(nativeRecorder, videoWidth, videoHeight, recordingPath, recordingCallback);
                    break;
                }
                case RuntimePlatform.Android: {
                    var recordingPath = Path.Combine(recordingDirectory, recordingFilename);
                    var nativeRecorder = new AndroidJavaObject(@"com.yusufolokoba.natcorder.HEVCRecorder", videoWidth, videoHeight, videoFramerate, videoBitrate, videoKeyframeInterval, audioSampleRate, audioChannelCount);
                    this.recorder = new MediaRecorderAndroid(nativeRecorder, videoWidth, videoHeight, recordingPath, recordingCallback);
                    break;
                }
                default:
                    Debug.LogError("NatCorder Error: HEVCRecorder is not supported on this platform");
                    this.recorder = null; // Self-destruct >:D
                    break;
            }
        }

        /// <summary>
        /// Stop recording and dispose the recorder.
        /// The recording callback is expected to be invoked soon after calling this method.
        /// </summary>
        [Doc(@"Dispose", @"DisposeDiscussion")]
        public void Dispose () {
            recorder.Dispose();
        }

        /// <summary>
        /// Commit a video pixel buffer for encoding.
        /// The pixel buffer MUST in the RGBA32 format.
        /// </summary>
        /// <param name="pixelBuffer">Pixel buffer containing video frame to commit</param>
        /// <param name="timestamp">Sample buffer timestamp in nanoseconds</param>
        [Doc(@"CommitFrame", @"CommitFrameDiscussion"), Code(@"RecordWebCam")]
        public void CommitFrame<T> (T[] pixelBuffer, long timestamp) where T : struct {
            recorder.CommitFrame(pixelBuffer, timestamp);
        }

        /// <summary>
        /// Commit a video pixel buffer for encoding.
        /// The pixel buffer MUST in the RGBA32 format.
        /// </summary>
        /// <param name="nativeBuffer">Pixel buffer in native memory to commit</param>
        /// <param name="timestamp">Sample buffer timestamp in nanoseconds</param>
        [Doc(@"CommitFrame", @"CommitFrameDiscussion")]
        public void CommitFrame (IntPtr nativeBuffer, long timestamp) {
            recorder.CommitFrame(nativeBuffer, timestamp);
        }

        /// <summary>
        /// Commit an audio sample buffer for encoding
        /// </summary>
        /// <param name="sampleBuffer">Raw PCM audio sample buffer, interleaved by channel</param>
        /// <param name="timestamp">Sample buffer timestamp in nanoseconds</param>
        [Doc(@"CommitSamples", @"CommitSamplesDiscussion"), Code(@"RecordPCM")]
        public void CommitSamples (float[] sampleBuffer, long timestamp) {
            recorder.CommitSamples(sampleBuffer, timestamp);
        }
        #endregion

        private readonly IMediaRecorder recorder;
        IMediaRecorder IAbstractRecorder.recorder {
            get { return recorder; }
        }
    }
}