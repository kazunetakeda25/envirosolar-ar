using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CustomerSlotPanel : MonoBehaviour
{
    [SerializeField] private Sprite[] m_Houses;

    [SerializeField] private Button m_LoadButton;
    [SerializeField] private Button m_ClearButton;
    [SerializeField] private ToggleGroup m_SlotToggleGroup;

    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private CustomerMenuUI m_CustomerMenuController;


    private void Start()
    {
        Toggle[] toggles = m_SlotToggleGroup.GetComponentsInChildren<Toggle>();
        for (int i = 0; i < toggles.Length; i ++)
        {
            toggles[i].onValueChanged.RemoveAllListeners();
            int index = i;
            toggles[i].onValueChanged.AddListener(delegate { 
                if (toggles[index].isOn == true)
                {
                    if (toggles[index].transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled == false)
                    {
                        m_LoadButton.interactable = true;
                        m_ClearButton.interactable = true;
                    }
                    else
                    {
                        if (m_SlotToggleGroup.AnyTogglesOn() == false)
                        {
                            m_LoadButton.interactable = false;
                            m_ClearButton.interactable = false;
                        }
                    }
                }
                else
                {
                    if (m_SlotToggleGroup.AnyTogglesOn() == false)
                    {
                        m_LoadButton.interactable = false;
                        m_ClearButton.interactable = false;
                    }
                }
            });
        }
    }

    private void OnEnable()
    {
        UpdateSlots();
    }

    public void ClosePanel()
    {
        transform.localScale = Vector3.zero;
    }

    public void ClearSlot()
    {
        Toggle[] toggles = m_SlotToggleGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn == true)
            {
                switch (toggles[i].name)
                {
                    case "0":
                        GlobalReferences._UserData.SLOT1 = new CustomizableJSON();
                        GlobalReferences.SaveUserData();
                        break;
                    case "1":
                        GlobalReferences._UserData.SLOT2 = new CustomizableJSON();
                        GlobalReferences.SaveUserData();
                        break;
                    case "2":
                        GlobalReferences._UserData.SLOT3 = new CustomizableJSON();
                        GlobalReferences.SaveUserData();
                        break;
                    case "3":
                        GlobalReferences._UserData.SLOT4 = new CustomizableJSON();
                        GlobalReferences.SaveUserData();
                        break;
                    default:
                        break;
                }
            }
        }

        UpdateSlots();
    }

    public void LoadHouse() 
    {
        Toggle[] toggles = m_SlotToggleGroup.GetComponentsInChildren<Toggle>();

        for (int i = 0; i < toggles.Length; i++)
        {
            if (toggles[i].isOn == true)
            {
                switch (toggles[i].name)
                {
                    case "0":
                        GlobalReferences.LoadUserData();
                        GlobalReferences._JSON = GlobalReferences._UserData.SLOT1;
                        break;
                    case "1":
                        GlobalReferences.LoadUserData();
                        GlobalReferences._JSON = GlobalReferences._UserData.SLOT2;
                        break;
                    case "2":
                        GlobalReferences.LoadUserData();
                        GlobalReferences._JSON = GlobalReferences._UserData.SLOT3;
                        break;
                    case "3":
                        GlobalReferences.LoadUserData();
                        GlobalReferences._JSON = GlobalReferences._UserData.SLOT4;
                        break;
                    default:
                        break;
                }
            }
        }

        UpdateSlots();
        ClosePanel();

        m_Canvas.SetActive(false);
        m_CustomerMenuController.LoadScene("presentation");
    }

    public void UpdateSlots()
    {
        GlobalReferences.LoadUserData();
        if (GlobalReferences._UserData.SLOT1.ID == 0)
        {
            m_SlotToggleGroup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            m_SlotToggleGroup.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            m_SlotToggleGroup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().sprite = m_Houses[GlobalReferences._UserData.SLOT1.HouseType - 1];
            m_SlotToggleGroup.transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            m_SlotToggleGroup.transform.GetChild(0).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        }

        if (GlobalReferences._UserData.SLOT2.ID == 0)
        {
            m_SlotToggleGroup.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            m_SlotToggleGroup.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            m_SlotToggleGroup.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().sprite = m_Houses[GlobalReferences._UserData.SLOT2.HouseType - 1];
            m_SlotToggleGroup.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            m_SlotToggleGroup.transform.GetChild(1).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        }

        if (GlobalReferences._UserData.SLOT3.ID == 0)
        {
            m_SlotToggleGroup.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            m_SlotToggleGroup.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            m_SlotToggleGroup.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().sprite = m_Houses[GlobalReferences._UserData.SLOT3.HouseType - 1];
            m_SlotToggleGroup.transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            m_SlotToggleGroup.transform.GetChild(2).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        }

        if (GlobalReferences._UserData.SLOT4.ID == 0)
        {
            m_SlotToggleGroup.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().enabled = false;
            m_SlotToggleGroup.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else
        {
            m_SlotToggleGroup.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().sprite = m_Houses[GlobalReferences._UserData.SLOT4.HouseType - 1];
            m_SlotToggleGroup.transform.GetChild(3).GetChild(0).GetChild(0).GetComponent<Image>().enabled = true;
            m_SlotToggleGroup.transform.GetChild(3).GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().enabled = false;
        }
    }
}
