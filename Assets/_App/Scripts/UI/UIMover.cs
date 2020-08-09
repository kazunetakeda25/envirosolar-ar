using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMover : MonoBehaviour
{
    public RectTransform moveRoot; //transform that will move
    protected Vector3 moveTarget
    {
        get { return _target; }
        set {
            if (_target != value) _moving = true;
            _target = value;
        }
    }
    private Vector3 _target;
    private bool _moving;
    private Vector3 _moveVelocity;
    private float _speed = .1f;

    // Update is called once per frame
    protected void MoveToTarget()
    {
        if (!_moving) return;
        moveRoot.transform.position = Vector3.SmoothDamp(moveRoot.transform.position, moveTarget, ref _moveVelocity, _speed);
        if (Vector3.Distance(moveRoot.transform.position, moveTarget) < .001f)
        {
            moveRoot.transform.position = moveTarget;
            _moving = false;
            OnTargetReached();
        }
    }

    protected virtual void OnTargetReached()
    {

    }
}
