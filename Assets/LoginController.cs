using System.Collections;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoginController : MonoBehaviour
{
    [SerializeField] private Toggle m_Checkbox;
    [Header("Staff InputFields")]
    [SerializeField] private TMP_InputField m_St_FirstName;
    [SerializeField] private TMP_InputField m_St_LastName;
    [SerializeField] private TMP_InputField m_St_PhoneNumber;
    [SerializeField] private TMP_InputField m_St_Password;
    [Header("Customer InputFields")]
    [SerializeField] private TMP_InputField m_Cu_FirstName;
    [SerializeField] private TMP_InputField m_Cu_LastName;
    [SerializeField] private TMP_InputField m_Cu_Email;
    [SerializeField] private TMP_InputField m_Cu_PhoneNumber;
    [SerializeField] private TMP_InputField m_Cu_ZipCode;

    [SerializeField] private Button m_GetStartedButton;
    [SerializeField] private TMP_Text m_Status;

    [SerializeField] private GameObject m_Canvas;
    [SerializeField] private GameObject m_Loading;

    private void Start()
    {
        m_Canvas.SetActive(false);
        m_Loading.SetActive(true);

        m_Checkbox.onValueChanged.AddListener(delegate
        {
            if (m_Checkbox.isOn == true)
            {
                SwitchUIForCustomers();
            }
            else
            {
                SwitchUIForStaffs();
            }
        });

        GlobalReferences.LoadOrCreateUserData();

        if (GlobalReferences._UserData.IsStaff == true)
        {
            if (CheckLoginStateForStaffs() == true)
            {
                GlobalReferences._JSON.YardSign = GlobalReferences._UserData.LastName;
                SceneManager.LoadSceneAsync("editor");
            }
        }
        else
        {
            if (CheckLoginStateForCustomers() == true)
            {
                GlobalReferences._JSON.YardSign = GlobalReferences._UserData.LastName;
                SceneManager.LoadSceneAsync("customerMenu");
            }
        }

        m_Loading.SetActive(false);
        m_Canvas.SetActive(true);
    }

    private bool CheckLoginStateForCustomers()
    {
        if (GlobalReferences._UserData.FirstName == "" 
            || GlobalReferences._UserData.LastName == ""
            || GlobalReferences._UserData.Email == ""
            || GlobalReferences._UserData.PhoneNumber == ""
            || GlobalReferences._UserData.ZipCode == "")
            return false;

        return true;
    }

    private bool CheckLoginStateForStaffs()
    {
        if (GlobalReferences._UserData.FirstName == ""
            || GlobalReferences._UserData.LastName == ""
            || GlobalReferences._UserData.PhoneNumber == "")
            return false;

        return true;
    }

    public void Login()
    {
        if (m_Checkbox.isOn == true)
        {
            if (RemoveNonNumeric(m_Cu_PhoneNumber.text).Length == 10)
            {
                StartCoroutine(CustomerLoginAttempt());
            }
            else
            {
                ThrowPhoneNumberInvalid();
            }
        }
        else
        {
            if (RemoveNonNumeric(m_St_PhoneNumber.text).Length == 10)
            {
                StartCoroutine(StaffLoginAttempt());
            }
            else
            {
                ThrowPhoneNumberInvalid();
            }
        }
    }

    private IEnumerator CustomerLoginAttempt()
    {
        WWWForm formData = new WWWForm();
        formData.AddField("firstname", m_Cu_FirstName.text);
        formData.AddField("lastname", m_Cu_LastName.text);
        formData.AddField("email", m_Cu_Email.text);
        formData.AddField("phone_number", m_Cu_PhoneNumber.text);
        formData.AddField("zipcode", m_Cu_ZipCode.text);
        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "registerUser", formData);
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
            m_Status.SetText("Login Failed.");
            yield break;
        }
        else
        {
            GlobalReferences._UserData.IsStaff = false;
            GlobalReferences._UserData.FirstName = m_Cu_FirstName.text;
            GlobalReferences._UserData.LastName = m_Cu_LastName.text;
            GlobalReferences._UserData.Email = m_Cu_Email.text;
            GlobalReferences._UserData.PhoneNumber = m_Cu_PhoneNumber.text;
            GlobalReferences._UserData.ZipCode = m_Cu_ZipCode.text;
            GlobalReferences.SaveUserData();

            GlobalReferences._JSON.ID = 1;
            GlobalReferences._JSON.YardSign = GlobalReferences._UserData.LastName;

            m_Canvas.SetActive(false);
            m_Loading.SetActive(true);

            SceneManager.LoadSceneAsync("customerMenu");
        }
    }

    private IEnumerator StaffLoginAttempt()
    {
        Debug.Log(m_St_Password.text.ToLower());
        if (m_St_Password.text.ToLower() != "solar2020")
        {
            ThrowInvalidPassword();
            yield break;
        }

        GlobalReferences._UserData.IsStaff = true;
        GlobalReferences._UserData.FirstName = m_St_FirstName.text;
        GlobalReferences._UserData.LastName = m_St_LastName.text;
        GlobalReferences._UserData.PhoneNumber = m_St_PhoneNumber.text;
        GlobalReferences.SaveUserData();

        GlobalReferences._JSON.ID = 1;
        GlobalReferences._JSON.YardSign = GlobalReferences._UserData.LastName;

        m_Canvas.SetActive(false);
        m_Loading.SetActive(true);

        SceneManager.LoadSceneAsync("editor");
    }

    private void ThrowPhoneNumberInvalid()
    {
        m_Status.SetText("Input a valid phone number.");
    }

    private void ThrowInvalidPassword()
    {
        m_Status.SetText("Password is incorrect.");
    }

    public string RemoveNonNumeric(string phone)
    {
        return Regex.Replace(phone, @"[^0-9]+", "");
    }
    
    public void ValidateCustomerForm()
    {
        if (m_Cu_FirstName.text.Length > 0 
            && m_Cu_LastName.text.Length > 0 
            && m_Cu_Email.text.Length > 0
            && m_Cu_PhoneNumber.text.Length == 10
            && m_Cu_ZipCode.text.Length > 0)
            m_GetStartedButton.interactable = true;
        else
            m_GetStartedButton.interactable = false;

        m_Status.text = "";
    }

    public void ValidateStaffForm()
    {
        if (m_St_FirstName.text.Length > 0
            && m_St_LastName.text.Length > 0
            && m_St_PhoneNumber.text.Length == 10
            && m_St_Password.text.Length > 0)
            m_GetStartedButton.interactable = true;
        else
            m_GetStartedButton.interactable = false;

        m_Status.text = "";
    }

    private void SwitchUIForCustomers()
    {
        m_Cu_FirstName.gameObject.SetActive(true);
        m_Cu_LastName.gameObject.SetActive(true);
        m_Cu_Email.gameObject.SetActive(true);
        m_Cu_PhoneNumber.gameObject.SetActive(true);
        m_Cu_ZipCode.gameObject.SetActive(true);

        m_St_FirstName.gameObject.SetActive(false);
        m_St_LastName.gameObject.SetActive(false);
        m_St_PhoneNumber.gameObject.SetActive(false);
        m_St_Password.gameObject.SetActive(false);
    }

    private void SwitchUIForStaffs()
    {
        m_Cu_FirstName.gameObject.SetActive(false);
        m_Cu_LastName.gameObject.SetActive(false);
        m_Cu_Email.gameObject.SetActive(false);
        m_Cu_PhoneNumber.gameObject.SetActive(false);
        m_Cu_ZipCode.gameObject.SetActive(false);

        m_St_FirstName.gameObject.SetActive(true);
        m_St_LastName.gameObject.SetActive(true);
        m_St_PhoneNumber.gameObject.SetActive(true);
        m_St_Password.gameObject.SetActive(true);
    }
}
