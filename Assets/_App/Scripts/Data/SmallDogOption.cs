using System;
using UnityEngine;

[CreateAssetMenu(fileName = "SmallDogOption", menuName = "CustomOptions/SmallDogOption", order = 5)]
public class SmallDogOption : CustomOption
{
    public CustomizableSmallDog _smallDogPrefab;

    public override void OnSelected()
    {
        HomeCustomizationManager.Instance.LoadNewSmallDog(this);
    }

    public override bool IsActiveSelection
    {
        get
        {
            return GlobalReferences._JSON.SmallDogs == _id;
        }
    }
}
