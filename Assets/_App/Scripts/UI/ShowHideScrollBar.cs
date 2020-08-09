using Michsky.UI.ModernUIPack;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(UIManagerScrollbar))]
public class ShowHideScrollBar : MonoBehaviour
{
    public Image background;
    public Image shadow;
    public Image scroll;
    private bool show = false;

    public void Refresh(Vector2 val)
    {
        if(val.y>.01f)
            show = true;
    }

    private void LateUpdate() { 

            background.enabled = show;
            scroll.enabled = show;
            shadow.enabled = show;


        show = false;
    }
}
