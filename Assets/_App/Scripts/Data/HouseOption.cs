using System;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseOption", menuName = "CustomOptions/HouseOption", order = 1)]
public class HouseOption : CustomOption
{
    public CustomizableHouse _housePrefab;
    public Texture2D _dropTarget;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.LoadHouseOption(this);

    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.HouseType == _id;
        }
    }
}
