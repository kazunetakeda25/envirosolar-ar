using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerMenuUI : MonoBehaviour
{
    [Header("Warning Panel")]
    [SerializeField] private GameObject m_WarningPanel;
    [Header("Slot Panel")]
    [SerializeField] private GameObject m_SlotPanel;
    [Header("Account Settings Panel")]
    [SerializeField] private GameObject m_SettingsPanel;
    [Header("UI")]
    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private GameObject m_Loading;

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

    public void VisitEnvirosolarWebsite()
    {
        Application.OpenURL("https://www.envirosolarpower.com/contact");
    }

    public void OpenSlotPanel()
    {
        m_SlotPanel.transform.localScale = Vector3.one;
    }

    public void CloseSlotPanel()
    {
        m_SlotPanel.transform.localScale = Vector3.zero;
    }

    public void BackToLogin()
    {
        OpenWarningDialog();
    }

    public void ResetUserData()
    {
        CloseWarningDialog();
        GlobalReferences.LoadUserData();
        GlobalReferences._UserData = new UserData();
        GlobalReferences.SaveUserData();
        LoadScene("login");
    }

    public void OpenWarningDialog()
    {
        m_WarningPanel.transform.localScale = Vector3.one;
    }

    public void CloseWarningDialog()
    {
        m_WarningPanel.transform.localScale = Vector3.zero;
    }

    public void OpenAccountSettingsPanel()
    {
        m_SettingsPanel.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating("UpdateUserJsons", 0, 60);
    }

    public void UpdateUserJsons()
    {
        StartCoroutine(FetchUserJsons());
    }

    private IEnumerator FetchUserJsons()
    {
        m_Loading.SetActive(true);
        WWWForm formData = new WWWForm();
        GlobalReferences.LoadUserData();
        formData.AddField("phone", GlobalReferences._UserData.PhoneNumber);
        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "getUserJsons", formData);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            JSONItemsArray jsonItemsArray = JsonUtility.FromJson<JSONItemsArray>("{\"jsonItems\":" + www.downloadHandler.text + "}");
            if (jsonItemsArray != null && jsonItemsArray.jsonItems.Length > 0)
            {
                CustomizableJSON json = new CustomizableJSON
                {
                    ID = jsonItemsArray.jsonItems[0].id,
                    HouseType = jsonItemsArray.jsonItems[0].housetype,
                    RoofType = jsonItemsArray.jsonItems[0].rooftype,
                    BrickType = jsonItemsArray.jsonItems[0].bricktype,
                    ColorType = jsonItemsArray.jsonItems[0].colortype,
                    BigDogs = jsonItemsArray.jsonItems[0].bigdogs,
                    SmallDogs = jsonItemsArray.jsonItems[0].smalldogs,
                    Cats = jsonItemsArray.jsonItems[0].cats,
                    SportBalls = jsonItemsArray.jsonItems[0].sportballs,
                    YardSign = jsonItemsArray.jsonItems[0].yardsign,
                    PanelCount = jsonItemsArray.jsonItems[0].panelcount,
                    PercentSavings = jsonItemsArray.jsonItems[0].percentsavings,
                    AverageBill = jsonItemsArray.jsonItems[0].averagebill,
                    SharedBy = string.Empty,
                    SharedTo = jsonItemsArray.jsonItems[0].phone
                };

                UpdateJSON(json);

                StartCoroutine(ReceivedJson(jsonItemsArray.jsonItems[0].id));
            }
        }

        m_Loading.SetActive(false);
    }

    private void UpdateJSON(CustomizableJSON cJson)
    {
        if (cJson.SharedTo == GlobalReferences._UserData.PhoneNumber)
        {
            string yardSign = GlobalReferences._JSON.YardSign;
            GlobalReferences._JSON = cJson;
            GlobalReferences._JSON.YardSign = yardSign;
            GlobalReferences.LoadUserData();

            if (GlobalReferences._UserData.IsStaff == false)
            {
                Debug.Log("GlobalReferences._UserData.SlotSaveIndex: " + GlobalReferences._UserData.SlotSaveIndex);
                switch (GlobalReferences._UserData.SlotSaveIndex)
                {
                    case 0:
                        GlobalReferences._UserData.SLOT1 = GlobalReferences._JSON;
                        GlobalReferences._UserData.SLOT1.ID = 1;
                        GlobalReferences._UserData.SlotSaveIndex = 1;
                        break;
                    case 1:
                        GlobalReferences._UserData.SLOT2 = GlobalReferences._JSON;
                        GlobalReferences._UserData.SLOT2.ID = 1;
                        GlobalReferences._UserData.SlotSaveIndex = 2;
                        break;
                    case 2:
                        GlobalReferences._UserData.SLOT3 = GlobalReferences._JSON;
                        GlobalReferences._UserData.SLOT3.ID = 1;
                        GlobalReferences._UserData.SlotSaveIndex = 3;
                        break;
                    case 3:
                        GlobalReferences._UserData.SLOT4 = GlobalReferences._JSON;
                        GlobalReferences._UserData.SLOT4.ID = 1;
                        GlobalReferences._UserData.SlotSaveIndex = 0;
                        break;
                    default:
                        break;
                }
            }

            GlobalReferences.SaveUserData();
            
            ShowNotifyWithPreview("You received your Envirosolar AR Home.");
        }
    }

    public void ShowNotifyWithPreview(string notify)
    {
        GameObject notifyObject = Instantiate(Resources.Load("Prefabs/NotifyWithPreview") as GameObject);
        notifyObject.transform.SetParent(m_Canvas.transform, false);
        notifyObject.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = notify;
        notifyObject.transform.GetChild(0).GetChild(0).GetChild(2).GetComponent<Button>().onClick.AddListener(OpenARScene);
        notifyObject.transform.GetChild(0).GetChild(0).GetChild(0).GetComponentInChildren<Button>().onClick.AddListener(delegate { CloseNotify(notifyObject); });
    }

    private void OpenARScene()
    {
        StartCoroutine(LoadPresentationScene());
        m_Canvas.SetActive(false);
    }

    private IEnumerator ReceivedJson(int jsonID)
    {
        WWWForm formData = new WWWForm();
        GlobalReferences.LoadUserData();
        formData.AddField("jsonid", jsonID.ToString());
        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "receivedJson", formData);
        yield return www.SendWebRequest();
    }

    private IEnumerator LoadPresentationScene()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("presentation");
    }

    private void CloseNotify(GameObject notifyObject)
    {
        Destroy(notifyObject);
    }
}
