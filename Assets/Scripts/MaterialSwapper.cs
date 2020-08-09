using Michsky.UI.ModernUIPack;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MaterialSwapper : MonoBehaviour
{
    public HorizontalSelector horizontalSelector;
    public GameObject selectedObj;
    public bool loadResourceMats = false;
    //public string prefabName = "House 9A 05 (v2)";

    public Color[] colors;

    public GameObject[] wallMeshes, roofMeshes;
    public Material[] roofMats;
    public Material[] wallMats;

    public UnityEngine.Object[] allMats;

    public enum typeMat { Roof, Wall };
    public typeMat materialType = typeMat.Roof;

    public int typeIndex = 0;
    public int index = 0;

    protected int wallIndex = 0;
    protected int roofIndex = 0;
    public int matSize = 0;

    public UnityEvent OnMatChanged = new UnityEvent();
    public UnityAction MatChangeAction;

    // Start is called before the first frame update
    void Start()
    {
        if (selectedObj == null)
            selectedObj = this.gameObject;

        //OnMatChanged.AddListener(ToggleMat);

        if(loadResourceMats)
            LoadMaterials();

        //if (horizontalSelector != null)
        //    index = horizontalSelector.defaultIndex;
    }

    public void LoadMaterials()
    {
        allMats = Resources.LoadAll("Materials", typeof(Material));
        foreach (var t in allMats)
        {
            HorizontalSelector.Item newitem = new HorizontalSelector.Item();
            newitem.itemTitle = t.name;
            horizontalSelector.itemList.Add(newitem);
            try
            {
                //MatChangeAction = ToggleMat;
                //newitem.onValueChanged.AddListener(MatChangeAction);
            }
            catch (Exception e) { Debug.Log("Error: " + e); }
        }

        matSize = horizontalSelector.itemList.Count;
        Debug.Log("Materials: [" + matSize + "]");
        Debug.Log("Selector Capacity: " + horizontalSelector.itemList.Capacity);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIndex(int newindex)
    {
        /**
        if (newindex >= 0)
            typeIndex = newindex;
        else
            Debug.Log("Couldn't update index (" + newindex + ")");*/
    }

    public void ToggleIndex()
    {
        if (typeIndex == 0)
            typeIndex = 1;
        else
            typeIndex = 0;

        Debug.Log("Mesh Type Index: [" + typeIndex + "]");
    }

    public void UpdateSelector()
    {
        
    }

    public void SwapRoofMat(int nextValue)
    {
        if (index + nextValue < 0)
            return;

        if (index + nextValue > matSize)
            return;

        if (roofIndex + nextValue < matSize)
            roofIndex += nextValue;
        else
            return;

        foreach (GameObject o in roofMeshes)
        {
            o.GetComponent<MeshRenderer>().material = (Material) allMats[roofIndex];
            //o.GetComponent<MeshRenderer>().material = roofMats[roofIndex];
        }

        if (index + nextValue <= matSize)
            index += nextValue;
    }

    public void SwapWallMat(int nextValue)
    {
        if (index + nextValue < 0)
            return;

        if (index + nextValue > matSize)
            return;

        if (wallIndex + nextValue < matSize)
            wallIndex += nextValue;
        else
            return;

        foreach (GameObject o in wallMeshes)
        {
            o.GetComponent<MeshRenderer>().material = (Material) allMats[wallIndex];
            //o.GetComponent<MeshRenderer>().material = wallMats[wallIndex];
        }

        if (index + nextValue <= matSize)
            index += nextValue;

        Debug.Log("Swapping Wall Mat");
    }

    public void ToggleMat()
    {
        Debug.Log("Material Swap");
        switch (typeIndex)
        {
            case 0:
                SwapRoofMat(1);
                break;
            case 1:
                SwapWallMat(1);
                break;
            default:
                break;
        }
    }

    public void UpdateMat(int value)
    {
        int nextValue = value;
        Debug.Log("Updating Material: [" + value + "]");
        switch (typeIndex)
        {
            case 0:
                SwapRoofMat(value);
                break;
            case 1:
                SwapWallMat(value);
                break;
            default:
                break;
        }
    }

    public void DebugToggle()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleIndex();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            ToggleMat();
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
            UpdateMat(1);

        if (Input.GetKeyDown(KeyCode.RightArrow))
            UpdateMat(-1);
    }
}
