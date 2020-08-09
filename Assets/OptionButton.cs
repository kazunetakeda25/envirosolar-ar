using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionButton : MonoBehaviour
{
    public Sprite background_unselected;
    public Sprite background_selected;

    public Color iconColor_unselected = Color.white;
    public Color iconColor_selected = Color.white;

    public Image background;
    public Image icon;

    public TextMeshProUGUI label;
    public Button button;

    private CustomOption option;

    void Update()
    {
        if (option)
        {
            SetSelected(option.IsActiveSelection);
        }
    }

    public void SetOption(CustomOption opt)
    {
        option = opt;
    }

    public void SetSelected(bool selected)
    {
        background.sprite = selected ? background_selected : background_unselected;
        if (option.solidColorIcon)
        {
            icon.color = selected ? iconColor_selected : iconColor_unselected;
        }
    }

    public void SetText(string text)
    {
        label.text = text;
    }

    public void SetIcon(Sprite image)
    {
        icon.sprite = image;
    }

    public void SetIconColor(Color col)
    {
        icon.color = col;
    }
}
