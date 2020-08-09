using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PushJSONPanel : MonoBehaviour
{
    [SerializeField] private Button m_PushButton;
    [SerializeField] private TMP_InputField m_FirstNameInputField;
    [SerializeField] private TMP_InputField m_LastNameInputField;
    [SerializeField] private TMP_InputField m_PhoneNumberInputField;
    [SerializeField] private TMP_InputField m_PanelCountInputField;
    [SerializeField] private TMP_InputField m_PercentSavingsInputField;
    [SerializeField] private TMP_InputField m_AverageBillInputField;
    [SerializeField] private GameObject m_Pushing;

    public TMP_InputField PercentSavingsInputField { get => m_PercentSavingsInputField; set => m_PercentSavingsInputField = value; }

    void Awake()
    {
        m_PhoneNumberInputField.text = "";
        m_PanelCountInputField.text = GlobalReferences._JSON.PanelCount.ToString();
        m_PercentSavingsInputField.text = GlobalReferences._JSON.PercentSavings.ToString();
        m_AverageBillInputField.text = GlobalReferences._JSON.AverageBill.ToString();
        m_Pushing.SetActive(false);
    }

    public void ValidateInputs()
    {
        if (m_FirstNameInputField.text.Length > 0
            && m_LastNameInputField.text.Length > 0
            && m_PhoneNumberInputField.text.Length == 10
            && m_PanelCountInputField.text.Length > 0
            && m_PercentSavingsInputField.text.Length > 0
            && m_AverageBillInputField.text.Length > 0)
            EnablePushButton();
        else
            DisablePushButton();
    }

    private void EnablePushButton()
    {
        m_PushButton.interactable = true;
    }

    private void DisablePushButton()
    {
        m_PushButton.interactable = false;
    }

    public void PushJSON()
    {
        int.TryParse(m_PanelCountInputField.text, out int panelCount);
        int.TryParse(m_PercentSavingsInputField.text, out int percentSaving);
        float.TryParse(m_AverageBillInputField.text, out float bill);

        GlobalReferences._JSON.PanelCount = panelCount;
        GlobalReferences._JSON.PercentSavings = percentSaving;
        GlobalReferences._JSON.AverageBill = bill;

        StartCoroutine(PushJson());
    }

    private IEnumerator PushJson()
    {
        m_Pushing.GetComponent<TextMeshProUGUI>().text = "Uploading To Server...";
        m_Pushing.SetActive(true);
        m_PushButton.interactable = false;
        WWWForm formData = new WWWForm();
        formData.AddField("housetype", GlobalReferences._JSON.HouseType);
        formData.AddField("rooftype", GlobalReferences._JSON.RoofType);
        formData.AddField("bricktype", GlobalReferences._JSON.BrickType);
        formData.AddField("colortype", GlobalReferences._JSON.ColorType);
        formData.AddField("bigdogs", GlobalReferences._JSON.BigDogs);
        formData.AddField("smalldogs", GlobalReferences._JSON.SmallDogs);
        formData.AddField("cats", GlobalReferences._JSON.Cats);
        formData.AddField("sportballs", GlobalReferences._JSON.SportBalls);
        formData.AddField("yardsign", m_LastNameInputField.text);
        formData.AddField("panelcount", GlobalReferences._JSON.PanelCount);
        formData.AddField("percentsavings", GlobalReferences._JSON.PercentSavings);
        formData.AddField("averagebill", GlobalReferences._JSON.AverageBill.ToString());
        formData.AddField("phone", m_PhoneNumberInputField.text);
        formData.AddField("firstname", m_FirstNameInputField.text);
        formData.AddField("lastname", m_LastNameInputField.text);
        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "pushJson", formData);
        yield return www.SendWebRequest();

        m_PushButton.interactable = true;

        if (www.isNetworkError || www.isHttpError)
        {
            m_Pushing.GetComponent<TextMeshProUGUI>().text = www.error;
        }
        else if (www.downloadHandler.text == "\"\"")
        {
            m_Pushing.GetComponent<TextMeshProUGUI>().text = "User not exists. Please input a correct phone number.";
        }
        else
        {
            m_Pushing.SetActive(false);
            ClosePanel();
        }
    }

    public void ClosePanel()
    {
        m_FirstNameInputField.text = "";
        m_LastNameInputField.text = "";
        m_PhoneNumberInputField.text = "";
        m_PanelCountInputField.text = GlobalReferences._JSON.PanelCount.ToString();
        m_PercentSavingsInputField.text = GlobalReferences._JSON.PercentSavings.ToString();
        m_AverageBillInputField.text = GlobalReferences._JSON.AverageBill.ToString();
        m_Pushing.SetActive(false);

        gameObject.SetActive(false);
    }
}
