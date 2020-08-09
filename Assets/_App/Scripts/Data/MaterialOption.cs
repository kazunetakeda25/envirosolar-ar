using System;
using UnityEngine;

[CreateAssetMenu(fileName = "MaterialOption", menuName = "CustomOptions/MaterialOption", order = 1)]
public class MaterialOption : CustomOption
{
    public MaterialFamily _family;
    public Material _material;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.SetMaterial(this);
    }

    public override bool IsActiveSelection
    {
        get
        {
            switch (_family)
            {
                case MaterialFamily.WALL:
                    return GlobalReferences._JSON.BrickType == _id;
                case MaterialFamily.ROOF:
                    return GlobalReferences._JSON.RoofType == _id;
            }
            return false;
        }
    }
}
