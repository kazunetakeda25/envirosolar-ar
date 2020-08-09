using UnityEngine;

public abstract class CustomOption : ScriptableObject
{
    public int _id;
    public Sprite _icon;
    public string _name;
    public abstract void OnSelected();

    public bool solidColorIcon;

    public virtual bool IsActiveSelection => false;
}
