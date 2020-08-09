using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeEditorUIManager : MonoBehaviour
{
    public static HomeEditorUIManager Instance;


    public UIEnterExit _mainMenu;
    public UIOptionsPanel _optionPanelPrefab;

    public CustomOptionSet baseOptionSet;

    private Stack<UIStackable> panelStack = new Stack<UIStackable>();
    private List<UIOptionsPanel> panelCache = new List<UIOptionsPanel>();

    [SerializeField] private GameObject m_Canvas;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            GameObject.Destroy(this);
        }
    }

    public void OpenARScene()
    {
        m_Canvas.SetActive(false);
        StartCoroutine(LoadPresentationScene());
    }

    private IEnumerator LoadPresentationScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("presentation");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("mainMenu");
    }

    public void OpenBaseEditorPanel()
    {
        OpenEditorPanelWithOptions(baseOptionSet);
    }

    public void OpenEditorPanelWithOptions(CustomOptionSet options)
    {
        UIOptionsPanel panel = GetEditorPanel(m_Canvas.transform);
        panel.LoadOptionSet(options);

        UIStackable stackable = panel.GetComponent<UIStackable>();
        if (panelStack.Count > 0)
        {
            panelStack.Peek().StackBehind(stackable);
        }
        panelStack.Push(stackable);

        UIEnterExit mover = panel.GetComponent<UIEnterExit>();
        mover.Enter();
    }

    public void CloseTopEditorPanel()
    {
        UIStackable top = panelStack.Pop();
        UIOptionsPanel optionsPanel = top.GetComponent<UIOptionsPanel>();
        if (optionsPanel)
        {
            panelCache.Add(optionsPanel);
        }
        if (panelStack.Count > 0)
        {
            panelStack.Peek().Unstack();
        }
    }


    private UIOptionsPanel GetEditorPanel(Transform trans)
    {
        if (panelCache.Count > 0)
        {
            UIOptionsPanel panelOut = panelCache[panelCache.Count-1];
            panelCache.RemoveAt(panelCache.Count-1);
            return panelOut;
        }
        else
        {
            UIOptionsPanel newPanel = GameObject.Instantiate(_optionPanelPrefab, trans);
            UIEnterExit mover = newPanel.GetComponent<UIEnterExit>();
            if (mover)
            {
                mover.OnExit.AddListener(CloseTopEditorPanel);
            }
            return newPanel;
        }
        //pull from cache or create new
    }

}
