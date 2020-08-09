using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOptionsPanel : MonoBehaviour
{
    public OptionButton optionOptionButtonPrefab;
    public GridLayoutGroup contentRoot;
    private List<OptionButton> activeOptionButtons = new List<OptionButton>();

    public void LoadOptionSet(CustomOptionSet set)
    {
        ClearOldOptionButtons();
        contentRoot.cellSize = set.buttonSize;
        foreach (CustomOption option in set.options)
        {
            LoadOption(option);
        }
    }

    void LoadOption(CustomOption option)
    {
        OptionButton button = GetOptionButton();
        button.SetOption(option);
        button.button.onClick.AddListener(option.OnSelected);

        button.SetIcon(option._icon);
        button.icon.rectTransform.ForceUpdateRectTransforms();

        if (option is ColorOption)
        {
            button.SetIconColor((option as ColorOption)._color);
        }
        else
        {
            button.SetIconColor(Color.white);
        }

        button.SetText(option._name);
    }

    void ClearOldOptionButtons()
    {
        foreach (OptionButton activeOptionButton in activeOptionButtons.ToArray())
        {
            GameObject.Destroy(activeOptionButton.gameObject);
        }
        activeOptionButtons.Clear();
    }

    OptionButton GetOptionButton()
    {
            OptionButton newOptionButton = Instantiate(optionOptionButtonPrefab, contentRoot.transform) as OptionButton;
            newOptionButton.transform.localScale = Vector3.one;

            activeOptionButtons.Add(newOptionButton);
            return newOptionButton;
        
    }



}
