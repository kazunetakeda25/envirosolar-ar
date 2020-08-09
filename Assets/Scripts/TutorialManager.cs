using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject m_CanvasForTutorial;
    [SerializeField] private PlaceTransformOnPlane m_Placer;

    void Start()
    {
        m_Placer.enabled = false;
        GlobalReferences.LoadUserData();
        if (GlobalReferences._UserData.IsFirstOpenedApp == true)
        {
            m_CanvasForTutorial.SetActive(true);
            GlobalReferences._UserData.IsFirstOpenedApp = false;
            GlobalReferences.SaveUserData();
        }
        else
        {
            m_CanvasForTutorial.SetActive(false);
            m_Placer.enabled = true;
        }
    }

    public void HideTutorial()
    {
        m_CanvasForTutorial.SetActive(false);
        m_Placer.enabled = true;
    }
}
