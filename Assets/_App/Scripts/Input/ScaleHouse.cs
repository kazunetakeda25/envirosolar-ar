using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScaleHouse : MonoBehaviour
{
    public float increment = .005f;
    public Transform _scaleRoot;

    public float _maxScale = .1f;
    public float _minScale = .01f;
    private float _currentScale = 1;

    public Button _scaleUpButton;
    public Button _scaleDownButton;

    private void Awake()
    {
        _currentScale = _scaleRoot.transform.localScale.x;
    }

    public void ScaleUp()
    {
        Scale(increment);
    }

    public void ScaleDown()
    {
        Scale(-increment);
    }

    private void Scale(float delta)
    {
        _currentScale += delta;
        _currentScale = Mathf.Clamp(_currentScale, _minScale, _maxScale);

        _scaleUpButton.interactable = _currentScale < _maxScale;
        _scaleDownButton.interactable = _currentScale > _minScale;

        _scaleRoot.transform.localScale = Vector3.one * _currentScale;
    }

    public void ShowHideScaleButtons(bool show)
    {
        _scaleUpButton.gameObject.SetActive(show);
        _scaleDownButton.gameObject.SetActive(show);
    }
}
