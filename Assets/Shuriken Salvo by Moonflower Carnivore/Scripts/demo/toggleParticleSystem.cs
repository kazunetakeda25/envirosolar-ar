using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class toggleParticleSystem : MonoBehaviour {
	ParticleSystem ps;
	public Toggle toggleButton;
	void OnEnable() {
		ps = GetComponent<ParticleSystem>();
	}
	public void onOff() {
		if (toggleButton.isOn) {
			ps.Play(true);
		} else {
			ps.Stop(true);
		}
	}
}
}