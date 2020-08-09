using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class EffectButtonLoop : MonoBehaviour {
	public string button;
	public Toggle myToggle;
	public ParticleSystem ps;
	void Start () {
		ps = ps.GetComponent<ParticleSystem> ();
		var em = ps.emission;
		em.enabled = false;
	}
	public void offOnButton () {
		var em = ps.emission;
		if (myToggle.isOn) {
			em.enabled = true;
		} else {
			em.enabled = false;
		}
	}
	void Update () {
		if (button == "") {
		} else if (Input.GetKeyDown(button)) {
			myToggle.isOn = !myToggle.isOn;
		}
	}
}
}