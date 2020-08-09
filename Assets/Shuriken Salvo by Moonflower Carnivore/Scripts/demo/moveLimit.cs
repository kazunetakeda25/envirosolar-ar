using System.Collections;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class moveLimit : MonoBehaviour {
	public Vector3 MinRange=new Vector3(-6f,-6f,-6f);
	public Vector3 MaxRange=new Vector3(6f,6f,6f);
	void Update () {
		transform.localPosition = new Vector3(Mathf.Clamp(transform.localPosition.x,MinRange.x,MaxRange.x),Mathf.Clamp(transform.localPosition.y,MinRange.y,MaxRange.y),Mathf.Clamp(transform.localPosition.z,MinRange.z,MaxRange.z));
	}
}
}