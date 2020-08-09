using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHouse : MonoBehaviour
{

    public void Rotate(float degrees)
    {
        transform.Rotate(Vector3.up, degrees, Space.Self);
    }
}
