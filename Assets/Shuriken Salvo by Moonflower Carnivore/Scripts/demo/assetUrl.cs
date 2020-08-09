using UnityEngine;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class assetUrl : MonoBehaviour {
	public string link;
	public void url () {
		Application.OpenURL(link);
	}
}
}