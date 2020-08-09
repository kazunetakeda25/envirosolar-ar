using UnityEngine;

public class ARViewFingerGestureListener : MonoBehaviour
{
    private float scale;

    private void Start()
    {
        scale = transform.localScale.x;
    }
    public void ScaleGestureExecuted(DigitalRubyShared.GestureRecognizer gesture)
    {
        Debug.Log("Action Triggered");
        scale *= (gesture as DigitalRubyShared.ScaleGestureRecognizer).ScaleMultiplier;

        if (scale < 0.6f)
            scale = 0.6f;

        if (scale > 2)
            scale = 2;

        Debug.Log("scale: " + scale);

        if (gesture.State == DigitalRubyShared.GestureRecognizerState.Executing)
        {
            Debug.LogFormat("Scale gesture executing, state: {0}, scale: {1} pos: {2},{3}", gesture.State, scale, gesture.FocusX, gesture.FocusY);
            transform.localScale = new Vector3(scale, scale, 1);
        }
    }
}
