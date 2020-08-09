using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject m_Canvas;

    public void LoadScene(string toLoad)
    {
        m_Canvas.GetComponent<Canvas>().enabled = false;
        StartCoroutine(LoadSceneWithDelay(toLoad));
    }

    private IEnumerator LoadSceneWithDelay(string sceneName)
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }
}
