using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class LiveCableAnimation : MonoBehaviour
{
    public float stretch = 1;
    private bool finishedEnter = false;
    private new MeshRenderer renderer;

    private Material _liveCableMat => Resources.Load<Material>("Materials/ACDC/LiveCableMat");
    private Material _enterCableMat => Resources.Load<Material>("Materials/ACDC/EnterCableMat");


    void Awake()
    {
        renderer = GetComponent<MeshRenderer>();
    }

    void OnEnable()
    {
        StartCoroutine(CableEnterRoutine());
    }


    IEnumerator CableEnterRoutine()
    {
        // tile y from 1 to 0
        renderer.material = _enterCableMat;
        renderer.material.mainTextureOffset = Vector2.up;

        float y = 1;
        while (y > 0)
        {
            renderer.material.mainTextureOffset = Vector2.up * y;
            y -= Time.deltaTime;
            yield return null;
        }
        renderer.material.mainTextureOffset = Vector2.zero;
        yield return null;
        renderer.material = _liveCableMat;
        renderer.material.mainTextureScale = new Vector2(1, stretch);

        while (true)
        {
            renderer.material.mainTextureOffset = renderer.material.mainTextureOffset - Vector2.up * Time.deltaTime;
            yield return null;
        }


    }
}
