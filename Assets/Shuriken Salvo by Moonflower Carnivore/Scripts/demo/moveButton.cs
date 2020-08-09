using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class moveButton : MonoBehaviour {
	public Transform objectTransform;
	public Vector3 translate;
	public void onButton () {
		objectTransform.localPosition += translate;
	}
}
}