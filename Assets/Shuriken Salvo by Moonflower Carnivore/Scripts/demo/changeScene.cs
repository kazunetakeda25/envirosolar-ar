using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MoonflowerCarnivore.ShurikenSalvo {
public class changeScene : MonoBehaviour {
	public string sceneName;
	//public Button button;
	public void button() {
		SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
	}
}
}