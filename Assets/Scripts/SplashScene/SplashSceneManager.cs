using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashSceneManager : MonoBehaviour
{
    IEnumerator Start()
    {
        yield return new WaitForSeconds(6);
        SceneManager.LoadScene("login");
    }
}
