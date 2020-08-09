using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

[Serializable]
public class CustomerInfo
{
    public string FirstName;
    public string LastName;
    public string PhoneNumber;
    public string Email;
    public string ZipCode;

    public CustomerInfo()
    {
        FirstName = LastName = PhoneNumber = Email = ZipCode = "";
    }
}
public class AccountSettingsPanel : MonoBehaviour
{
    [SerializeField] private GameObject m_NameBlock;
    [SerializeField] private GameObject m_PhoneNumberBlock;
    [SerializeField] private GameObject m_EmailBlock;
    [SerializeField] private GameObject m_ZipCodeBlock;
    //[SerializeField] private GameObject m_PasswordBlock;

    [SerializeField] private TMP_InputField m_Cu_FirstName;
    [SerializeField] private TMP_InputField m_Cu_LastName;
    [SerializeField] private TMP_InputField m_Cu_PhoneNumber;
    [SerializeField] private TMP_InputField m_Cu_Email;
    [SerializeField] private TMP_InputField m_Cu_ZipCode;
    //[SerializeField] private TMP_InputField m_St_FirstName;
    //[SerializeField] private TMP_InputField m_St_LastName;
    //[SerializeField] private TMP_InputField m_St_PhoneNumber;
    //[SerializeField] private TMP_InputField m_St_Password;
    
    [SerializeField] private TextMeshProUGUI m_Status;

    [SerializeField] private Button m_ConfirmButton;
    [SerializeField] private Button m_CancelButton;

    [SerializeField] private Button m_GoBackButton;

    private void UpdateUI()
    {
        GlobalReferences.LoadUserData();

        if (GlobalReferences._UserData.IsStaff == true)
        {
            //m_NameBlock.SetActive(true);
            //m_PhoneNumberBlock.SetActive(true);
            //m_EmailBlock.SetActive(false);
            //m_ZipCodeBlock.SetActive(false);
            //m_PasswordBlock.SetActive(true);

            //m_NameBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
            //    = GlobalReferences._UserData.FirstName + " " + GlobalReferences._UserData.LastName;
            //m_PhoneNumberBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
            //    = GlobalReferences._UserData.PhoneNumber;

            //m_Cu_FirstName.text = "";
            //m_Cu_LastName.text = "";
            //m_Cu_PhoneNumber.text = "";
            //m_Cu_Email.text = "";
            //m_Cu_ZipCode.text = "";
            //m_St_FirstName.text = GlobalReferences._UserData.FirstName;
            //m_St_LastName.text = GlobalReferences._UserData.LastName;
            //m_St_PhoneNumber.text = GlobalReferences._UserData.PhoneNumber;

            //m_Status.text = "";
        }
        else
        {
            m_NameBlock.SetActive(true);
            m_PhoneNumberBlock.SetActive(true);
            m_EmailBlock.SetActive(true);
            m_ZipCodeBlock.SetActive(true);
            //m_PasswordBlock.SetActive(false);

            m_NameBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = GlobalReferences._UserData.FirstName + " " + GlobalReferences._UserData.LastName;
            m_PhoneNumberBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = GlobalReferences._UserData.PhoneNumber;
            m_EmailBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = GlobalReferences._UserData.Email;
            m_ZipCodeBlock.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text
                = GlobalReferences._UserData.ZipCode;

            m_Cu_FirstName.text = GlobalReferences._UserData.FirstName;
            m_Cu_LastName.text = GlobalReferences._UserData.LastName;
            m_Cu_PhoneNumber.text = GlobalReferences._UserData.PhoneNumber;
            m_Cu_Email.text = GlobalReferences._UserData.Email;
            m_Cu_ZipCode.text = GlobalReferences._UserData.ZipCode;
            //m_St_FirstName.text = "";
            //m_St_LastName.text = "";
            //m_St_PhoneNumber.text = "";

            m_Status.text = "";
        }

        m_Cu_FirstName.gameObject.SetActive(false);
        m_Cu_LastName.gameObject.SetActive(false);
        m_Cu_PhoneNumber.gameObject.SetActive(false);
        m_Cu_Email.gameObject.SetActive(false);
        m_Cu_ZipCode.gameObject.SetActive(false);
        //m_St_FirstName.gameObject.SetActive(false);
        //m_St_LastName.gameObject.SetActive(false);
        //m_St_PhoneNumber.gameObject.SetActive(false);
        //m_St_Password.gameObject.SetActive(false);

        m_ConfirmButton.gameObject.SetActive(false);
        m_CancelButton.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        UpdateUI();
    }
/// <summary>
/// //////////////////////////////////////////////////////////////////////////////
/// </summary>
    public void OnEdit_Cu_Name()
    {
        UpdateUI();

        m_Cu_FirstName.gameObject.SetActive(true);
        m_Cu_LastName.gameObject.SetActive(true);
        
        m_ConfirmButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.AddListener(UpdateCuName);
        m_ConfirmButton.interactable = true;
        m_ConfirmButton.gameObject.SetActive(true);
        m_CancelButton.gameObject.SetActive(true);
    }

    public void ValidateCuNameInputs()
    {
        if (m_Cu_FirstName.text.Length > 0
            && m_Cu_LastName.text.Length > 0)
            m_ConfirmButton.interactable = true;
        else
            m_ConfirmButton.interactable = false;
    }

    public void UpdateCuName()
    {
        CustomerInfo info = new CustomerInfo
        {
            FirstName = m_Cu_FirstName.text,
            LastName = m_Cu_LastName.text
        };

        StartCoroutine(UpdateUserInfo(info));
    }
    /// <summary>
    /// //////////////////////////////////////////////////////////////////////////////
    /// </summary>
    public void OnEdit_Cu_PhoneNumber()
    {
        UpdateUI();

        m_Cu_PhoneNumber.gameObject.SetActive(true);

        m_ConfirmButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.AddListener(UpdateCuPhoneNumber);
        m_ConfirmButton.interactable = true;
        m_ConfirmButton.gameObject.SetActive(true);
        m_CancelButton.gameObject.SetActive(true);
    }

    public void ValidateCuPhoneNumberInputs()
    {
        if (m_Cu_PhoneNumber.text.Length == 10)
            m_ConfirmButton.interactable = true;
        else
            m_ConfirmButton.interactable = false;
    }

    private void UpdateCuPhoneNumber()
    {
        CustomerInfo info = new CustomerInfo
        {
            PhoneNumber = m_Cu_PhoneNumber.text
        };

        StartCoroutine(UpdateUserInfo(info));
    }

    public void OnEdit_Cu_Email()
    {
        UpdateUI();

        m_Cu_Email.gameObject.SetActive(true);

        m_ConfirmButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.AddListener(UpdateCuEmail);
        m_ConfirmButton.interactable = true;
        m_ConfirmButton.gameObject.SetActive(true);
        m_CancelButton.gameObject.SetActive(true);
    }

    public void ValidateCuEmailInputs()
    {
        if (m_Cu_Email.text.Length > 0)
            m_ConfirmButton.interactable = true;
        else
            m_ConfirmButton.interactable = false;
    }

    private void UpdateCuEmail()
    {
        CustomerInfo info = new CustomerInfo
        {
            Email = m_Cu_Email.text
        };

        StartCoroutine(UpdateUserInfo(info));
    }

    public void OnEdit_Cu_ZipCode()
    {
        UpdateUI();

        m_Cu_ZipCode.gameObject.SetActive(true);

        m_ConfirmButton.onClick.RemoveAllListeners();
        m_ConfirmButton.onClick.AddListener(UpdateCuZipCode);
        m_ConfirmButton.interactable = true;
        m_ConfirmButton.gameObject.SetActive(true);
        m_CancelButton.gameObject.SetActive(true);
    }

    public void ValidateCuZipCodeInputs()
    {
        if (m_Cu_ZipCode.text.Length > 0)
            m_ConfirmButton.interactable = true;
        else
            m_ConfirmButton.interactable = false;
    }

    private void UpdateCuZipCode()
    {
        CustomerInfo info = new CustomerInfo
        {
            ZipCode = m_Cu_ZipCode.text
        };

        StartCoroutine(UpdateUserInfo(info));
    }

    private IEnumerator UpdateUserInfo(CustomerInfo info)
    {
        m_Status.text = "Updating...";

        GlobalReferences.LoadUserData();
        
        WWWForm formData = new WWWForm();
        formData.AddField("key", GlobalReferences._UserData.PhoneNumber);
        formData.AddField("firstname", info.FirstName);
        formData.AddField("lastname", info.LastName);
        formData.AddField("email", info.Email);
        formData.AddField("phone_number", info.PhoneNumber);
        formData.AddField("zipcode", info.ZipCode);

        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "updateUser", formData);
        yield return www.SendWebRequest();

        Debug.Log(www.downloadHandler.text);

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
            Debug.Log(www.downloadHandler.text);
            m_Status.SetText(www.error);
            yield break;
        }
        else if (int.TryParse(www.downloadHandler.text, out int recordN) == false)
        {
            m_Status.SetText("Failed to update your account information.");
            yield break;
        }
        else
        {
            if (info.FirstName.Length > 0)
                GlobalReferences._UserData.FirstName = info.FirstName;
            if (info.LastName.Length > 0)
                GlobalReferences._UserData.LastName = info.LastName;
            if (info.PhoneNumber.Length > 0)
                GlobalReferences._UserData.PhoneNumber = info.PhoneNumber;
            if (info.Email.Length > 0)
                GlobalReferences._UserData.Email = info.Email;
            if (info.ZipCode.Length > 0)
                GlobalReferences._UserData.ZipCode = info.ZipCode;

            GlobalReferences.SaveUserData();
            UpdateUI();
        }
    }

    public void CancelEdit()
    {
        UpdateUI();
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
