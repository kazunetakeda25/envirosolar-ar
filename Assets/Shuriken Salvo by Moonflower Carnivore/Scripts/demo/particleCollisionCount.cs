using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class particleCollisionCount : MonoBehaviour {
	public ParticleSystem ps;
	public GameObject target;
	public List<ParticleCollisionEvent> collisionEvents;
	public Text display;
	int hit = 0;
		
	void Update(){
		collisionEvents = new List<ParticleCollisionEvent>();
		hit += ps.GetCollisionEvents(target, collisionEvents);
		//GetComponent<Text>().text = (System.String.Format("{0:F2}",hit));
		//string format = System.String.Format(hit.ToString()," Hit!");
		display.text = (hit.ToString()+" HITS!");
		//Debug.Log(hit);
	}
}
}