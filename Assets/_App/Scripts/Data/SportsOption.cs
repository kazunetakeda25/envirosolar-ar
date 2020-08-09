using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SportsOption", menuName = "CustomOptions/SportsOption", order = 7)]
public class SportsOption : CustomOption
{
    public CustomizableSports _sportsPrefab;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.LoadNewSports(this);

    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.SportBalls == _id;
        }
    }
}
