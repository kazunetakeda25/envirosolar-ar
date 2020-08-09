using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BigDogOption", menuName = "CustomOptions/BigDogOption", order = 4)]
public class BigDogOption : CustomOption
{
    public CustomizableBigDog _bigDogPrefab;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.LoadNewBigDog(this);
    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.BigDogs == _id;
        }
    }
}
