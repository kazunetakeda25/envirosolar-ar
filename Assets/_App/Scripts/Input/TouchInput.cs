using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TouchInput : MonoBehaviour
{
    private float rotVelocity = 0;
    private float dampen = 6;
    public RotateHouse houseRotator;
    private float sensitivity = 90;


    public Camera houseCam;
    float perspectiveZoomSpeed = 0.1f;        // The rate of change of the field of view in perspective mode.
    float orthoZoomSpeed = 0.03f;        // The rate of change of the orthographic size in orthographic mode.

    public float minOrthoSize = 8;
    public float maxOrthSize = 40;

    private Vector2 lastTouchViewport;
    private bool touch1Last;
    private bool touch2Last;

    private Vector3 lastMousePosition;

    // Update is called once per frame
    void Update()
    {
        //ROTATE
        if (houseRotator)
        {
            float rot = rotVelocity;
            rotVelocity = rotVelocity * (1 - dampen * Time.deltaTime);

#if UNITY_EDITOR
            if(Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject(-1))
            {
                Vector2 touchViewport = Camera.main.ScreenToViewportPoint(Input.mousePosition);
#else

            if (Input.touchCount > 0 && !EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId))
            {
                Vector2 touchViewport = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position);
#endif

                if (touch1Last)
                {
                    Vector2 touchDelta = touchViewport - lastTouchViewport;
                    rot = touchDelta.x * sensitivity;
                    rotVelocity = rot;
                }
                lastTouchViewport = touchViewport;
                touch1Last = true;
            }
            else
            {
                touch1Last = false;
            }
            if (rot != 0)
            {
                houseRotator.Rotate(-rot);
            }
        }

            //PINCH ZOOM
            if (houseCam)
        {
            

#if UNITY_EDITOR
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.Space))
            {
                Touch touchZero = new Touch();
                touchZero.position = Input.mousePosition;
                touchZero.deltaPosition = Input.mousePosition - lastMousePosition;
                Touch touchOne = new Touch();
                touchOne.position = Camera.main.ViewportToScreenPoint(Vector3.one * .5f);
                touchOne.deltaPosition = Vector2.zero;

#else
            if (Input.touchCount == 2)
            {
                // Store both touches.
                Touch touchZero = Input.GetTouch(0);
                Touch touchOne = Input.GetTouch(1);
#endif

                // Find the position in the previous frame of each touch.
                Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
                Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;


                // Find the magnitude of the vector (the distance) between the touches in each frame.
                float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
                float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

                // Find the difference in the distances between each frame.
                float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

                // If the camera is orthographic...
                if (houseCam.orthographic)
                {
                    // ... change the orthographic size based on the change in distance between the touches.
                    houseCam.orthographicSize = Mathf.Clamp(houseCam.orthographicSize + deltaMagnitudeDiff * orthoZoomSpeed, minOrthoSize, maxOrthSize);

                    // Make sure the orthographic size never drops below zero.
                }
                else
                {
                    // Otherwise change the field of view based on the change in distance between the touches.
                    houseCam.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                    // Clamp the field of view to make sure it's between 0 and 180.
                    houseCam.fieldOfView = Mathf.Clamp(houseCam.fieldOfView, 0.1f, 179.9f);
                }
            }
            lastMousePosition = Input.mousePosition;
        }

    }
}
