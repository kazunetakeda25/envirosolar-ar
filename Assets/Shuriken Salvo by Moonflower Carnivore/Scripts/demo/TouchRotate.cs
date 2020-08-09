using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class TouchRotate : MonoBehaviour {
    public float sensitivityX = 0f;
    public float sensitivityY = 0f;
    public float sensitivityZ = 0f;
	public Touch touchZero;

	void Update() {
		// If there are two touches on the device...
		if (Input.touchCount == 1 && Input.touchCount<2) {
			touchZero = Input.GetTouch(0);
			// Find the position in the previous frame of each touch.
			float deltaMagnitudeDiff = (touchZero.position - touchZero.deltaPosition).magnitude;

			this.transform.localEulerAngles += new Vector3 (deltaMagnitudeDiff * sensitivityX,deltaMagnitudeDiff * sensitivityY,deltaMagnitudeDiff * sensitivityZ);
		}
	}
}
}