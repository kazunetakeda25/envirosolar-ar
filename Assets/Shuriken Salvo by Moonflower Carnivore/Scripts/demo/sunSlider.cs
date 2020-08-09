using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class sunSlider : MonoBehaviour {
	public Slider mainSlider;
	public void sun () {
		float value2 = (mainSlider.value) * 2F;
		if (mainSlider.value>0.5) {//when slider value is greater than 1, value 2 pingpong backward instead of growing greater.
			value2 = (1F-value2)*2F+value2;
		}
		Light light = GetComponent<Light> ();
		light.intensity = Mathf.Clamp(value2,0.001f,3f);
		transform.eulerAngles = new Vector3 (mainSlider.value*360f-90f,-29f,0f);
	}
}
}