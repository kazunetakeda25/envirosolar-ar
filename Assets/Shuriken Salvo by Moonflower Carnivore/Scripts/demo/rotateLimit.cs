using System.Collections;
using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class rotateLimit : MonoBehaviour {
	public Vector3 MinRange=new Vector3(0f,0f,0f);
	public Vector3 MaxRange=new Vector3(360f,360f,360f);
	void Update () {
		transform.localEulerAngles = new Vector3(Mathf.Clamp(transform.localEulerAngles.x,MinRange.x,MaxRange.x),Mathf.Clamp(transform.localEulerAngles.y,MinRange.y,MaxRange.y),Mathf.Clamp(transform.localEulerAngles.z,MinRange.z,MaxRange.z));
	}
}
}