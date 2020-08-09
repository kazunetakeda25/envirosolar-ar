using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class toggleControlSingle : MonoBehaviour {
	public GameObject[] effects;
	public ParticleSystem[] ps;
	public Toggle toggleButton;
	public Text toggleText;
	public Button prevButton;
	public Button nextButton;
	int count=0;
	
	void Start(){
		toggleText.text = effects[count].ToString();
		for (count=0 ; count < effects.Length ; count++) {
			ps[count] = effects[count].GetComponent<ParticleSystem>();
		}
		count=0;
		effects[0].SetActive(true);
		ps[0].Play(true);
	}
	
	public void onOff(){
		if (toggleButton.isOn) {
			effects[count].SetActive(true);
			ps[count].Play(true);
		} else {
			effects[count].SetActive(false);
			ps[count].Stop(true);
		}
	}
	
	public void next(){
		effects[count].SetActive(false);
		ps[count].Stop(true);
		if (count == effects.Length-1) {
			count = 0;
		} else {
			count++;
		}
		toggleText.text = effects[count].ToString();
		effects[count].SetActive(true);
		ps[count].Play(true);
		toggleButton.isOn = true;
	}
	
	public void prev(){
		effects[count].SetActive(false);
		ps[count].Stop(true);
		if (count == 0) {
			count = effects.Length-1;
		} else {
			count--;
		}
		toggleText.text = effects[count].ToString();
		effects[count].SetActive(true);
		ps[count].Play(true);
		toggleButton.isOn = true;
	}
}
}