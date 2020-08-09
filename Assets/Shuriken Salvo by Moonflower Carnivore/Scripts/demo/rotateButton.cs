using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class rotateButton : MonoBehaviour {
	//public string button;
	public Transform objectTransform;
	public Vector3 rotation;
	public void onButton () {
		objectTransform.localEulerAngles += rotation;
	}/*
	void Update() {
		if (Input.GetButtonDown(button)) {
			objectTransform.localEulerAngles += rotation;
		}
	}*/
}
}