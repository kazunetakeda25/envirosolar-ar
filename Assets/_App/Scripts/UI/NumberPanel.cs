using UnityEngine;
using UnityEngine.UI;

public class NumberPanel : MonoBehaviour
{
    [SerializeField] private Button m_OKButton;
    [SerializeField] private TMPro.TMP_InputField m_PanelCountInputField;
    [SerializeField] private TMPro.TMP_InputField m_PercentSavingsInputField;
    [SerializeField] private TMPro.TMP_InputField m_AverageBillInputField;


    void Awake()
    {
        m_PanelCountInputField.text = GlobalReferences._JSON.PanelCount.ToString();
        m_PercentSavingsInputField.text = GlobalReferences._JSON.PercentSavings.ToString();
        m_AverageBillInputField.text = GlobalReferences._JSON.AverageBill.ToString();
    }

    public void ValidateInputs()
    {
        if (m_PanelCountInputField.text.Length > 0
            && m_PercentSavingsInputField.text.Length > 0
            && m_AverageBillInputField.text.Length > 0)
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

    public void SaveNumbers()
    {
        int panelCount;
        int percentSaving;
        float bill;

        int.TryParse(m_PanelCountInputField.text, out panelCount);
        int.TryParse(m_PercentSavingsInputField.text, out percentSaving);
        float.TryParse(m_AverageBillInputField.text, out bill);

        GlobalReferences._JSON.PanelCount = panelCount;
        GlobalReferences._JSON.PercentSavings = percentSaving;
        GlobalReferences._JSON.AverageBill = bill;

        transform.localScale = Vector3.zero;
    }

    public void ClosePanel()
    {
        m_PanelCountInputField.text = GlobalReferences._JSON.PanelCount.ToString();
        m_PercentSavingsInputField.text = GlobalReferences._JSON.PercentSavings.ToString();
        m_AverageBillInputField.text = GlobalReferences._JSON.AverageBill.ToString();

        transform.localScale = Vector3.zero;
    }
}
