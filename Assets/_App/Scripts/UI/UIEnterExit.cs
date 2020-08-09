using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIEnterExit : UIMover
{

    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public RectTransform enterTarget;
    public RectTransform exitTarget;

//    public float _enterSpeed;
//    public float _exitSpeed;

    private bool _show;


    public bool showOnEnable;
    public bool deactivateOnExit = true;

    private void OnEnable()
    {
        _show = showOnEnable;
        if (showOnEnable)
        {
            moveRoot.transform.position = enterTarget.position;
            moveRoot.gameObject.SetActive(true);
        }
        else
        {
            moveRoot.transform.position = exitTarget.position;
            if (deactivateOnExit)
                moveRoot.gameObject.SetActive(false);
        }
    }

    public void Update()
    {
        moveTarget = _show ? enterTarget.position : exitTarget.position;
        MoveToTarget();
    }

    public void Enter()
    {
        moveRoot.gameObject.SetActive(true);
        _show = true;
        OnEnter.Invoke();
    }

    public void Exit()
    {
        _show = false;
        OnExit.Invoke();
    }

    protected override void OnTargetReached()
    {
        if (!_show && deactivateOnExit)
            moveRoot.gameObject.SetActive(false);
    }
}
