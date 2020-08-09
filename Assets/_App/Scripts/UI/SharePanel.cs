using UnityEngine;
using UnityEngine.UI;

public class SharePanel : MonoBehaviour
{
    [SerializeField] private Button m_OKButton;
    [SerializeField] private TMPro.TMP_InputField m_PhoneNumberInputField;

    public void ValidateShare()
    {
        if (m_PhoneNumberInputField.text.Length == 10)
            EnableOKButton();
        else
            DisableOKButton();
    }

    private void EnableOKButton()
    {
        m_OKButton.interactable = true;
    }

    private void DisableOKButton()
    {
        m_OKButton.interactable = false;
    }

    public void ClosePanel()
    {
        m_PhoneNumberInputField.text = "";
        
        transform.localScale = Vector3.zero;
    }
}
