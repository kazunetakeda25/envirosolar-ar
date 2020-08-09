using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorOption", menuName = "CustomOptions/ColorOption", order = 6)]
public class ColorOption : CustomOption
{
    public Color _color = Color.white;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.SetColor(this);

    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.ColorType == _id;
        }
    }
}
