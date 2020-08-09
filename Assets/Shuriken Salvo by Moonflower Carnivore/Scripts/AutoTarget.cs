using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonflowerCarnivore.ShurikenSalvo;

namespace MoonflowerCarnivore.ShurikenSalvo {
	public class AutoTarget : MonoBehaviour {
		public float speed;
		float step;
		public Transform[] targets;
		int count=0;
		int next=0;
		void Start () {
			transform.position = targets[0].position;
		}
		void Update () {
			step = speed * Time.deltaTime;
			if (count == targets.Length-1) {
				next = 0;
			} else {
				next = count+1;
			}
			transform.position = Vector3.MoveTowards(transform.position,targets[next].position,step);
			if (transform.position == targets[next].position) {
				if (count == targets.Length-1) {
					count = 0;
				} else {
					count++;
				}
			}
		}
		
	}
}