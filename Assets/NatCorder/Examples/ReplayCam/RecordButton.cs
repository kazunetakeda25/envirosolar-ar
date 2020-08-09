/* 
*   NatCorder
*   Copyright (c) 2019 Yusuf Olokoba
*/


using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;

namespace NatCorder.Examples
{
    [RequireComponent(typeof(EventTrigger))]
	public class RecordButton : MonoBehaviour, IPointerDownHandler {

		public Image button, countdown;
		public UnityEvent onTouchDown, onTouchUp;
		[HideInInspector] public bool pressed;
		private const float MaxRecordingTime = 23f; // seconds

        public ReplayCam replayCam;

        private void Awake()
        {
            SetTimerText(MaxRecordingTime);

        }

        private void Reset () {
			// Reset fill amounts
			if (button) button.fillAmount = 1.0f;
			if (countdown) countdown.fillAmount = 0.0f;
            SetTimerText(MaxRecordingTime);

            transform.GetChild(2).GetComponent<TMP_Text>().text = "25:00";
            replayCam.StopRecording();
            GetComponent<Button>().interactable = true;
            pressed = false;
		}

		void IPointerDownHandler.OnPointerDown (PointerEventData eventData) {
			// Start counting
            if (pressed == false)
			    StartCoroutine (Countdown());
		}

		//void IPointerUpHandler.OnPointerUp (PointerEventData eventData) {
		//	// Reset pressed
		//	pressed = false;
		//}

		private IEnumerator Countdown () {
			pressed = true;
            GetComponent<Button>().interactable = false;
			// First wait a short time to make sure it's not a tap
			yield return new WaitForSeconds(0.2f);
			if (!pressed) yield break;
			// Start recording
			if (onTouchDown != null) onTouchDown.Invoke();
			// Animate the countdown
			float startTime = Time.time, ratio = 0f;
			while (pressed && (ratio = (Time.time - startTime) / MaxRecordingTime) < 1.0f) {
				countdown.fillAmount = ratio;
				button.fillAmount = 1f - ratio;
                SetTimerText(button.fillAmount * MaxRecordingTime);
                yield return null;
			}
			// Reset
			Reset();
			// Stop recording
			if (onTouchUp != null) onTouchUp.Invoke();
		}

        void SetTimerText(float time)
        {
            transform.GetChild(2).GetComponent<TMP_Text>().text = string.Format("{0:00.00}", time);

        }
    }
}
