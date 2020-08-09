using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 look = transform.position - Camera.main.transform.position;
        Quaternion lookRot = Quaternion.LookRotation(look);
        transform.rotation = lookRot;
    }
}
