/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/

namespace NatCorder.Examples {

#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityEngine;
    using Clocks;
    using Inputs;
    using UnityEngine.UI;
    using System.IO;
    using RenderHeads.Media.AVProVideo;
    using TMPro;
    using System.Collections;

    public class ReplayCam : MonoBehaviour {

        /**
        * ReplayCam Example
        * -----------------
        * This example records the screen using a `CameraRecorder`.
        * When we want mic audio, we play the mic to an AudioSource and record the audio source using an `AudioRecorder`
        * -----------------
        * Note that UI canvases in Overlay mode cannot be recorded, so we use a different mode (this is a Unity issue)
        */

        private int videoWidth = 720;
        private int videoHeight = 1280;

        [Header("Microphone")]
        public bool recordMicrophone;
        public AudioListener audioListener;
        public AudioSource microphoneAudioSource;
        private AudioClip[] audioClips;

        private MP4Recorder videoRecorder;
        private IClock recordingClock;
        private CameraInput cameraInput;
        private AudioInput audioInput;

        public HomeCustomizationManager customizationManager;
        public Animator savePanel;
        public Button m_DiscardButton;
        public Button m_ShareButton;

        public MediaPlayer mediaPlayer;
        public GameObject mediaPlayerBackground;
        public GameObject mediaPlayerFrame;
        public Button m_MediaPlayButton;
        public Button m_MediaPauseButton;
        public Button m_BackToARButton;

        public GameObject RecordingImage;
        public GameObject StopRecordingButton;

        public GameObject m_SavingAnimation;

        private bool IsSaved = false;

        public RecordButton recordButton;

        public void StartRecording() {

            mediaPlayerBackground.SetActive(false);
            mediaPlayerFrame.SetActive(false);

            customizationManager.SpawnHouse();

            recordingClock = new RealtimeClock();
            videoRecorder = new MP4Recorder(
                videoWidth,
                videoHeight,
                30,
                recordMicrophone ? AudioSettings.outputSampleRate : 0,
                recordMicrophone ? (int)AudioSettings.speakerMode : 0,
                //2,
                OpenSaveOption
            );
            Debug.Log("Recording Started");
            // Create recording inputs
            cameraInput = new CameraInput(videoRecorder, recordingClock, Camera.main);
            if (recordMicrophone) {
                StartMicrophone();
                audioInput = new AudioInput(videoRecorder, recordingClock, audioListener);
            }

            RecordingImage.SetActive(false);
            StopRecordingButton.SetActive(true);
        }

        private void StartMicrophone()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            microphoneAudioSource.clip = Microphone.Start(null, true, 10, 44100);
            microphoneAudioSource.loop = true;
            while (!(Microphone.GetPosition(null) > 0)) { }
            microphoneAudioSource.Play();
#endif
        }

        private void StopMicrophone()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            Microphone.End(null);
            microphoneAudioSource.Stop();
#endif
        }

        public void DiscardRecording()
        {
            recordButton.pressed = false;
        }

        public void StopRecording()
        {
            if (recordMicrophone)
            {
                StopMicrophone();

                if (audioInput != null)
                    audioInput.Dispose();
            }
            if (cameraInput != null)
                cameraInput.Dispose();

            Debug.Log("Recording Stopped");
            if (videoRecorder != null)
                videoRecorder.Dispose();
        }

        public void OnReplay(string path) {
            Debug.Log("Saved recording to: " + path);
        }

        private void Update()
        {
            if (mediaPlayer.Control.IsFinished() == true)
            {
                m_MediaPlayButton.gameObject.SetActive(true);
                m_MediaPauseButton.gameObject.SetActive(false);
            }

        }

        public void Rewind()
        {
            mediaPlayer.Control.Rewind();
        }

        public void ContinueVideo()
        {
            mediaPlayer.Control.Play();
        }

        public void PauseVideo()
        {
            Debug.Log("Paused");
            mediaPlayer.Control.Pause();
        }

        private void DiscardVideo(string path)
        {
            savePanel.ResetTrigger("IsOpened");
            savePanel.SetTrigger("IsClosed");

            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("File deleted");
            }

            mediaPlayer.CloseVideo();
            mediaPlayerBackground.SetActive(false);
            mediaPlayerFrame.SetActive(false);

            m_BackToARButton.gameObject.SetActive(false);
        }

        private void BackToARMode(string path)
        {
            savePanel.ResetTrigger("IsOpened");
            savePanel.SetTrigger("IsClosed");

            if (IsSaved == false && File.Exists(path))
            {
                File.Delete(path);
                Debug.Log("File deleted");
            }

            m_BackToARButton.gameObject.SetActive(false);

            mediaPlayer.CloseVideo();
            mediaPlayerBackground.SetActive(false);
            mediaPlayerFrame.SetActive(false);

            IsSaved = false;
        }

        private void OpenSaveOption(string path)
        {
            savePanel.SetTrigger("IsOpened");
            savePanel.ResetTrigger("IsClosed");

            RecordingImage.SetActive(true);
            StopRecordingButton.SetActive(false);

            mediaPlayerBackground.SetActive(true);
            mediaPlayerFrame.SetActive(true);
            mediaPlayer.OpenVideoFromFile(MediaPlayer.FileLocation.AbsolutePathOrURL, path, false);
            m_BackToARButton.gameObject.SetActive(true);

            m_DiscardButton.onClick.RemoveAllListeners();
            m_ShareButton.onClick.RemoveAllListeners();
            m_BackToARButton.onClick.RemoveAllListeners();

            m_DiscardButton.onClick.AddListener(delegate
            {
                DiscardVideo(path);
            });

            m_ShareButton.onClick.AddListener(delegate
            {
#if UNITY_ANDROID
                bool canShare = NatShareU.NatShare.Share(path, "See How Much Money You Can Save With Envirosolar!  www.envirosolarvisualizer.com");
                if (canShare == false)
                {
                    ShowNotify("The video can't be shared.");
                }
#elif UNITY_IOS
                NativeShare share = new NativeShare();
                share.AddFile(path, "video/mp4");
                share.SetTitle("Recorded Video");
                share.SetText("See How Much Money You Can Save With Envirosolar!  www.envirosolarvisualizer.com");
                share.Share();
#endif
            });

            m_BackToARButton.onClick.AddListener(delegate
            {
                BackToARMode(path);
            });
        }

        public void ShowNotify(string notify)
        {
            GameObject notifyObject = Instantiate(Resources.Load("Prefabs/Notify") as GameObject);
            notifyObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
            notifyObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = notify;
            Destroy(notifyObject, 6);
        }
    }
}
