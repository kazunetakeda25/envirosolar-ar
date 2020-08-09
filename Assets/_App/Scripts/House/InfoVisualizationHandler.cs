using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoVisualizationHandler : MonoBehaviour
{

    public TextMeshProUGUI _panelsText;
    public TextMeshProUGUI _savingsText;
    public TextMeshProUGUI _averageBillText;

    public void SetNumberOfPanels(int panels)
    {
        if (_panelsText)
        {
            _panelsText.text = panels.ToString("0");
        }
    }

    public void SetPercentSavings(float percent)
    {
        if (_savingsText)
        {
            _savingsText.text = percent.ToString("0")+ "%";
        }
    }

    public void SetAverageBill(float bill)
    {
        if (_averageBillText)
        {
            _averageBillText.text = "$"+ bill.ToString("0");
        }
    }
}
