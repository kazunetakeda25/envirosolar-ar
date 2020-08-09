using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PullJSONPanel : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Houses;
    [SerializeField] private Sprite[] m_Roofs;
    [SerializeField] private Sprite[] m_Walls;
    [SerializeField] private Color[] m_Colors;
    [SerializeField] private Sprite[] m_BigDogs;
    [SerializeField] private Sprite[] m_SmallDogs;
    [SerializeField] private Sprite[] m_Cats;
    [SerializeField] private Sprite[] m_SportsBalls;
    [SerializeField] private GameObject m_Item;
    [SerializeField] private GameObject m_NoDataAvaliable;
    [SerializeField] private Transform m_ServerJsonRoot;
    [SerializeField] private GameObject m_JsonContent;
    [SerializeField] private GameObject m_Updating;
    [SerializeField] private TMP_InputField m_SearchInputField;

    private void OnEnable()
    {
        UpdateJsons();
    }

    public void UpdateJsons()
    {
        StartCoroutine(FetchJsons());
    }

    private IEnumerator FetchJsons()
    {
        m_Updating.SetActive(true);
        WWWForm formData = new WWWForm();
        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "getJsons", "");
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            JSONItemsArray jsonItemsArray = JsonUtility.FromJson<JSONItemsArray>("{\"jsonItems\":" + www.downloadHandler.text + "}");
            UpdateItems(jsonItemsArray);
        }
    }

    private void UpdateItems(JSONItemsArray jsonItemsArray)
    {
        m_SearchInputField.text = "";

        foreach (Transform t in m_JsonContent.transform)
        {
            t.gameObject.SetActive(false);
            Destroy(t.gameObject);
        }

        m_JsonContent.transform.DetachChildren();

        GameObject[] noDatas = GameObject.FindGameObjectsWithTag("NoData");
        foreach (GameObject nd in noDatas)
        {
            Destroy(nd);
        }

        if (jsonItemsArray.jsonItems.Length == 0)
        {
            GameObject noDataAvaliable = Instantiate(m_NoDataAvaliable);
            noDataAvaliable.transform.SetParent(m_ServerJsonRoot, false);
            m_Updating.SetActive(false);
            return;
        }

        for (int i = 0; i < jsonItemsArray.jsonItems.Length; i ++)
        {
            GameObject item = Instantiate(m_Item);
            item.transform.SetParent(m_JsonContent.transform, false);
            item.name = jsonItemsArray.jsonItems[i].phone;
            item.transform.GetChild(0).GetComponent<Image>().sprite = m_Houses[jsonItemsArray.jsonItems[i].housetype - 1];
            item.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite 
                = m_Roofs[jsonItemsArray.jsonItems[i].rooftype - 1];
            if (jsonItemsArray.jsonItems[i].bricktype > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(1).GetComponent<Image>().sprite
                = m_Walls[jsonItemsArray.jsonItems[i].bricktype - 1];
            }
            
            if (jsonItemsArray.jsonItems[i].colortype > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().sprite = null;
                item.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(2).GetComponent<Image>().color 
                    = m_Colors[jsonItemsArray.jsonItems[i].colortype - 1];
            }

            if (jsonItemsArray.jsonItems[i].bigdogs > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(0).GetComponent<Image>().sprite 
                    = m_BigDogs[jsonItemsArray.jsonItems[i].bigdogs - 1];
            }

            if (jsonItemsArray.jsonItems[i].smalldogs > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite
                    = m_SmallDogs[jsonItemsArray.jsonItems[i].smalldogs - 1];
            }

            if (jsonItemsArray.jsonItems[i].cats > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite
                    = m_Cats[jsonItemsArray.jsonItems[i].cats - 1];
            }

            if (jsonItemsArray.jsonItems[i].sportballs > 0)
            {
                item.transform.GetChild(1).GetChild(0).GetChild(1).GetChild(3).GetComponent<Image>().sprite
                    = m_SportsBalls[jsonItemsArray.jsonItems[i].sportballs - 1];
            }

            item.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = jsonItemsArray.jsonItems[i].firstname;
            item.transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = jsonItemsArray.jsonItems[i].lastname;
            item.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = jsonItemsArray.jsonItems[i].lastname;
            item.transform.GetChild(1).GetChild(2).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = jsonItemsArray.jsonItems[i].panelcount.ToString();
            item.transform.GetChild(1).GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = jsonItemsArray.jsonItems[i].percentsavings.ToString() + "%";
            item.transform.GetChild(1).GetChild(3).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = "$" + jsonItemsArray.jsonItems[i].averagebill.ToString();

            int index = i;
            item.GetComponent<Button>().onClick.AddListener(delegate
            {
                GlobalReferences._JSON.ID = 1;
                GlobalReferences._JSON.HouseType = jsonItemsArray.jsonItems[index].housetype;
                GlobalReferences._JSON.RoofType = jsonItemsArray.jsonItems[index].rooftype;
                GlobalReferences._JSON.BrickType = jsonItemsArray.jsonItems[index].bricktype;
                GlobalReferences._JSON.ColorType = jsonItemsArray.jsonItems[index].colortype;
                GlobalReferences._JSON.BigDogs = jsonItemsArray.jsonItems[index].bigdogs;
                GlobalReferences._JSON.SmallDogs = jsonItemsArray.jsonItems[index].smalldogs;
                GlobalReferences._JSON.Cats = jsonItemsArray.jsonItems[index].cats;
                GlobalReferences._JSON.SportBalls = jsonItemsArray.jsonItems[index].sportballs;
                GlobalReferences._JSON.YardSign = jsonItemsArray.jsonItems[index].lastname;
                GlobalReferences._JSON.PanelCount = jsonItemsArray.jsonItems[index].panelcount;
                GlobalReferences._JSON.PercentSavings = jsonItemsArray.jsonItems[index].percentsavings;
                GlobalReferences._JSON.AverageBill = jsonItemsArray.jsonItems[index].averagebill;
                GlobalReferences._JSON.SharedBy = "";
                GlobalReferences._JSON.SharedTo = "";

                HomeCustomizationManager.Instance.SpawnHouse();
                ClosePanel();
            });

            if (m_SearchInputField.text.Length > 0)
            {
                if (item.name.Contains(m_SearchInputField.text))
                {
                    item.SetActive(true);
                }
                else
                {
                    item.SetActive(false);
                }
            }
        }

        m_Updating.SetActive(false);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    public void UpdateSearchFilter()
    {
        if (m_SearchInputField.text.Length == 0)
        {
            for (int i = 0; i < m_JsonContent.transform.childCount; i++)
            {
                m_JsonContent.transform.GetChild(i).gameObject.SetActive(true);
            }
        }

        for (int i = 0; i < m_JsonContent.transform.childCount; i ++)
        {
            if (m_JsonContent.transform.GetChild(i).name.Contains(m_SearchInputField.text) 
                || m_JsonContent.transform.GetChild(i).transform.GetChild(1).GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text.Contains(m_SearchInputField.text)
                || m_JsonContent.transform.GetChild(i).transform.GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text.Contains(m_SearchInputField.text) )
            {
                m_JsonContent.transform.GetChild(i).gameObject.SetActive(true);
            } 
            else
            {
                m_JsonContent.transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}