using System.Collections;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class resetPosition : MonoBehaviour {
	public Vector3 position;
	public void resetPos() {
		transform.position = position;
	}
}
}