using System.Collections;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class locateAttacker : MonoBehaviour {
	public GameObject attacker;
	ParticleSystem ps;
	ParticleSystem.MainModule main;
	ParticleSystem.ShapeModule shape;
	Transform master;
	Transform attackerpos;
	float dist;
	void OnEnable() {
		master = this.gameObject.transform.GetChild(0).GetChild(0);
		master.localEulerAngles = new Vector3 (0f,-90f,0f);
		ps = master.GetComponent<ParticleSystem>();
		main = ps.main;
		shape = ps.shape;
		attackerpos = attacker.GetComponent<Transform>();
	}
	void Update() {
		this.transform.LookAt(attackerpos);
		dist = Vector3.Distance(attackerpos.position, master.position);
		shape.radius = dist;
		main.startSpeedMultiplier = -dist / main.startLifetimeMultiplier - (dist * 0.05f); //variable start speed
		/* variable start lifetime requires updating the "duration" of all "particle sub birth..." sub emitters which isn't worth the trouble.
		main.startLifetimeMultiplier = dist / Mathf.Abs(main.startSpeedMultiplier); 
		*/
	}
}
}