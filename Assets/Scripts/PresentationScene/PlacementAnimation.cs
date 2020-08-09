using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementAnimation : MonoBehaviour
{
    public void StartAnimation()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
