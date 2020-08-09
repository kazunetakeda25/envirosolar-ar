using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class moveRandom : MonoBehaviour {
	public float speed = 1f;
	public float timeUpdate = 2f;
	Rigidbody rb;
	float step = 0f;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		step += Time.deltaTime;
		if (step > timeUpdate * Time.deltaTime) {
			rb.velocity = new Vector3(Random.Range(-speed,speed), Random.Range(-speed,speed), Random.Range(-speed,speed));
			step = 0f;
		}
	}
	
	public void OnDisable () {
		rb.velocity = Vector3.zero;
	}
}
}