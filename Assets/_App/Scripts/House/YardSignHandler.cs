using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class YardSignHandler : MonoBehaviour
{
    public TMPro.TextMeshPro nameText;
    public GameObject root;

    public void SetName(string name)
    {
        if (nameText)
        nameText.text = name;
    }

    public void RefreshText()
    {
        StartCoroutine(RefreshRoutine());
    }

    IEnumerator RefreshRoutine()
    {
        root.SetActive(false);
        yield return new WaitForEndOfFrame();
        root.SetActive(true);
    }
}
