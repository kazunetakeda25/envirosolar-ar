using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class PinchVerticalTranslation : MonoBehaviour {
    public float sensitivity = 0.5f;
	public Touch touchZero;
	public Touch touchOne;
	public Touch touchTwo;

	void Update() {
		// If there are three touches on the device...
		if (Input.touchCount == 3) {
			touchZero = Input.GetTouch(0);
			touchOne = Input.GetTouch(1);
			touchTwo = Input.GetTouch(2);
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchTwoPrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchTwo.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;
			
			this.transform.position += new Vector3 (0f,Mathf.Clamp(deltaMagnitudeDiff * sensitivity,1.2f,8f),0f);
		}
	}
}
}