using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialDecoy : MonoBehaviour
{

    public GameObject[] toDuplicate;
    private List<GameObject> decoys = new List<GameObject>();
    public GameObject decoration;

    void OnEnable()
    {
        foreach(GameObject obj in toDuplicate)
        {
            GameObject decoy = GameObject.Instantiate(obj, this.transform);
            decoy.transform.position = obj.transform.position;
            decoys.Add(decoy);
            foreach (Selectable select in decoy.GetComponentsInChildren<Selectable>())
            {
                select.targetGraphic.raycastTarget = false;
            }
            if (decoration)
            {
                GameObject decor = GameObject.Instantiate(decoration, decoy.transform);
            }
        }
    }

    void OnDisable()
    {
        foreach(GameObject obj in decoys)
        {
            Destroy(obj);
        }
        decoys.Clear();
    }
}
