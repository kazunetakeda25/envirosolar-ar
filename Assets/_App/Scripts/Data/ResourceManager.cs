using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField PhoneNumberInputField;

    [SerializeField] private GameObject m_SharePanel;
    [SerializeField] private Button m_ShareButton;

    public static ResourceManager _Instance;

    public void OpenSharePanel()
    {
        m_SharePanel.transform.localScale = Vector3.one;
    }

    public void Share()
    {
        StartCoroutine(CheckShare());
    }

    private IEnumerator CheckShare()
    {
        m_ShareButton.interactable = false;
        WWWForm formData = new WWWForm();
        formData.AddField("phone_number", PhoneNumberInputField.text);

        UnityWebRequest www = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "getUserIdNameFromPhoneNumber", formData);
        yield return www.SendWebRequest();


        if (www.isNetworkError || www.isHttpError)
        {
            ShowNotify("Share Failed.");
            m_ShareButton.interactable = true;
            yield break;
        }

        string respData = www.downloadHandler.text;
        string idText = null;
        string lastName = null;

        if (respData.Split(',').Length == 2)
        {
            idText = respData.Split(',')[0];
            lastName = respData.Split(',')[1];
        }
        else
        {
            ShowNotify("Share Failed.");
            m_ShareButton.interactable = true;
            yield break;
        }

        if (int.TryParse(idText, out int recordN) == false)
        {
            ShowNotify("Share Failed.");
            m_ShareButton.interactable = true;
            yield break;
        }

        if (recordN <= 0)
        {
            ShowNotify("The customer with the phone number does not have an account.");
            m_ShareButton.interactable = true;
            yield break;
        }

        WWWForm formData1 = new WWWForm();

        formData1.AddField("housetype", GlobalReferences._JSON.HouseType);
        formData1.AddField("rooftype", GlobalReferences._JSON.RoofType);
        formData1.AddField("bricktype", GlobalReferences._JSON.BrickType);
        formData1.AddField("colortype", GlobalReferences._JSON.ColorType);
        formData1.AddField("bigdogs", GlobalReferences._JSON.BigDogs);
        formData1.AddField("smalldogs", GlobalReferences._JSON.SmallDogs);
        formData1.AddField("cats", GlobalReferences._JSON.Cats);
        formData1.AddField("sportballs", GlobalReferences._JSON.SportBalls);
        formData1.AddField("yardsign", lastName);
        formData1.AddField("panelcount", GlobalReferences._JSON.PanelCount);
        formData1.AddField("percentsavings", GlobalReferences._JSON.PercentSavings);
        formData1.AddField("averagebill", GlobalReferences._JSON.AverageBill.ToString());
        formData1.AddField("sharedby", GlobalReferences._UserData.PhoneNumber);
        formData1.AddField("sharedto", PhoneNumberInputField.text);
        formData1.AddField("userid", recordN.ToString());

        UnityWebRequest www1 = UnityWebRequest.Post(GlobalReferences.SERVER_API_URL + "shareJsonToCustomer", formData1);
        yield return www1.SendWebRequest();

        if (www1.isNetworkError || www1.isHttpError)
        {
            Debug.Log(www1.error);
            ShowNotify("Share Failed.");
            m_ShareButton.interactable = true;
            yield break;
        }

        Debug.Log(www1.downloadHandler.text);

        ShowNotify("AR HOUSE succesfully shared.");

        //RandomAudioSelector audio = GetComponent<RandomAudioSelector>();
        //if (audio)
        //{
        //    audio.PlayRandom();
        //}

        m_ShareButton.interactable = true;
    }

    public void ShowNotify(string notify)
    {
        GameObject notifyObject = Instantiate(Resources.Load("Prefabs/Notify") as GameObject);
        notifyObject.transform.SetParent(GameObject.Find("Canvas").transform, false);
        notifyObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = notify;
        Destroy(notifyObject, 6);
    }
}
