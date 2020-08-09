using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CatOption", menuName = "CustomOptions/CatOption", order = 6)]
public class CatOption : CustomOption
{
    public CustomizableCat _catPrefab;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.LoadNewCat(this);
    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.Cats ==_id;
        }
    }
}
