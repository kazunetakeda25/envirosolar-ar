using UnityEngine;
using UnityEngine.UI;

public class UIMask : MonoBehaviour
{
    [SerializeField] private GameObject m_Top;
    [SerializeField] private GameObject m_Bottom;
    [SerializeField] private GameObject m_ReplayCam;
    
    [SerializeField] private GameObject m_Canvas;

    [SerializeField] private GameObject m_Logo;
    [SerializeField] private GameObject m_BackButton1;
    [SerializeField] private GameObject m_BackButton2;

    [SerializeField] private GameObject m_RecordButton;
    [SerializeField] private GameObject m_ScaleButtonGroup;

    [SerializeField] private GameObject m_PlayButton;
    [SerializeField] private GameObject m_PauseButton;

    [SerializeField] private GameObject m_SavePanel;

    private void Start()
    {
        m_Canvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width, Screen.height);

        m_Top.GetComponent<RectTransform>().sizeDelta = new Vector2(m_Top.GetComponent<RectTransform>().sizeDelta.x, (Screen.height - Screen.width) / 2);

        m_Bottom.GetComponent<RectTransform>().sizeDelta = new Vector2(m_Bottom.GetComponent<RectTransform>().sizeDelta.x, (Screen.height - Screen.width) / 2);

        //m_ReplayCam.GetComponent<RectTransform>().offsetMax = new Vector2(m_ReplayCam.GetComponent<RectTransform>().offsetMax.x, -(Screen.height - Screen.width) / 2);
        //m_ReplayCam.GetComponent<RectTransform>().offsetMin = new Vector2(m_ReplayCam.GetComponent<RectTransform>().offsetMin.x, (Screen.height - Screen.width) / 2);

        m_Logo.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5, Screen.width / 5);
        m_Logo.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, Screen.width / 10);

        m_BackButton1.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        m_BackButton1.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 10, 0);
        m_BackButton1.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);

        m_BackButton2.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        m_BackButton2.GetComponent<RectTransform>().anchoredPosition = new Vector2(Screen.width / 10, 0);
        m_BackButton2.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);

        m_RecordButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5, Screen.width / 5);
        m_RecordButton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -Screen.width / 10);
        m_RecordButton.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 7.5f, Screen.width / 7.5f);
        m_RecordButton.transform.GetChild(2).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 5, Screen.width / 25);
        m_RecordButton.transform.GetChild(3).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 7.5f / 1.4f, Screen.width / 7.5f / 1.4f);

        m_ScaleButtonGroup.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3 - Screen.width / 10, Screen.width / 5);
        m_ScaleButtonGroup.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        m_ScaleButtonGroup.transform.GetChild(0).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);
        m_ScaleButtonGroup.transform.GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 10, Screen.width / 10);
        m_ScaleButtonGroup.transform.GetChild(1).transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 20, Screen.width / 20);

        m_PlayButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.width / 3);
        m_PlayButton.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, Screen.width / 1.5f);
        m_PauseButton.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 3, Screen.width / 3);
        m_PauseButton.transform.GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, Screen.width / 1.5f);

        m_SavePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, (Screen.height - Screen.width) / 2);
        m_SavePanel.transform.GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, (Screen.height - Screen.width) / 10);
        m_SavePanel.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, (Screen.height - Screen.width) / 20);
        m_SavePanel.transform.GetChild(0).GetChild(1).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, (Screen.height - Screen.width) / 10);
        m_SavePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 1.5f, (Screen.height - Screen.width) / 20);
    }
}
