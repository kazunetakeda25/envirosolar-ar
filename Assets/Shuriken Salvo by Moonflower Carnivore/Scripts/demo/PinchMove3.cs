using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class PinchMove3 : MonoBehaviour {
    public float sensitivityX = 0f;
    public float sensitivityY = 0f;
    public float sensitivityZ = 0f;
	public Touch touchZero;
	public Touch touchOne;

	void Update() {
		// If there are two touches on the device...
		if (Input.touchCount == 3 && Input.touchCount < 4) {
			touchZero = Input.GetTouch(0);
			touchOne = Input.GetTouch(2);
			// Find the position in the previous frame of each touch.
			Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

			// Find the magnitude of the vector (the distance) between the touches in each frame.
			float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
			float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

			// Find the difference in the distances between each frame.
			float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

			this.transform.localPosition += new Vector3 (deltaMagnitudeDiff * sensitivityX,deltaMagnitudeDiff * sensitivityY,deltaMagnitudeDiff * sensitivityZ);
		}
	}
}
}