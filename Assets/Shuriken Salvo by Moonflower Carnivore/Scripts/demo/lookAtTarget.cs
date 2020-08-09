using System.Collections;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class lookAtTarget : MonoBehaviour {
	public Transform attacker;
	void Update() {
		this.transform.LookAt(attacker);
	}
}
}