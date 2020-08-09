using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ARManager : MonoBehaviour
{
    [Tooltip("Menu Parent Object")]
    public GameObject Menu;
    [Tooltip("Items to toggle on/off with menu button")]
    public GameObject[] ToggleItems;
    public GameObject[] ToggleOn;
    public GameObject[] ToggleOff;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void OpenEditor()
    {
        SceneManager.LoadScene("editor");
    }

    public void OpenCustomerMain()
    {
        SceneManager.LoadScene("customerMenu");
    }

    public void GoBack()
    {
        GlobalReferences.LoadUserData();
        if (GlobalReferences._UserData.IsStaff == true)
            OpenEditor();
        else
            OpenCustomerMain();
    }

    public void ToggleMenuItems()
    {
        if(ToggleItems != null)
        {
            foreach(GameObject o in ToggleItems)
                o.SetActive(!o.activeSelf);
        }

        if (ToggleOn != null)
        {
            foreach (GameObject o in ToggleOn)
                o.SetActive(true);
        }

        if (ToggleOff != null)
        {
            foreach (GameObject o in ToggleOff)
                o.SetActive(false);
        }

    }

    public void ToggleMenu()
    {
        if (Menu != null)
            Menu.SetActive(!Menu.activeSelf);
    }
}
