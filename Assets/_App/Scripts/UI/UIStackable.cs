using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIStackable : UIMover
{
    public UnityEvent OnStack;
    public UnityEvent OnUnstack;
    public RectTransform stackBehindRoot;

    private UIStackable stackFront;
    private RectTransform stackTarget;

    public void StackBehind(UIStackable panel)
    {
        stackFront = panel;
        stackTarget = panel.stackBehindRoot;
        
        OnStack.Invoke();

        foreach(Selectable select in GetComponentsInChildren<Selectable>())
        {
            select.interactable = false;
        }
    }

    public void Unstack()
    {
        
        stackFront = null;
        stackTarget = null;
        OnUnstack.Invoke();

        foreach (Selectable select in GetComponentsInChildren<Selectable>())
        {
            select.interactable = true;
        }
    }

    private void Update()
    {
        if (stackTarget)
        {
            moveTarget = stackTarget.position + Vector3.forward * .05f + Vector3.up * .1f;
        }
        else
        {
            moveTarget = moveRoot.parent.position;
        }
        MoveToTarget();
    }
}
