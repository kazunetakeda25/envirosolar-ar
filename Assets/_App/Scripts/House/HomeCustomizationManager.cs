using System;
using System.Reflection;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum MaterialFamily
{
    WALL,
    ROOF
}


public class HomeCustomizationManager : MonoBehaviour
{
    public static HomeCustomizationManager Instance;

    public Transform rootPosition;


    [HideInInspector] public CustomizableHouse _currentHouse;

    public bool animateIn;
    public bool spawnHouseOnStart;

    public CustomOptionSet houseOptionSet;
    public CustomOptionSet roofMaterialOptionSet;
    public CustomOptionSet wallMaterialOptionSet;
    public CustomOptionSet bigDogOptionSet;
    public CustomOptionSet smallDogOptionSet;
    public CustomOptionSet catOptionSet;
    public CustomOptionSet sportsOptionSet;
    public CustomOptionSet colorOptionSet;

    private void Awake()
    {
        if (!Instance) Instance = this;
        else { GameObject.Destroy(this); }
    }

    void Start()
    {
        if (spawnHouseOnStart)
        {
            SpawnHouse();
        }
    }



    public void SpawnHouse()
    {
        Debug.Log("Spawn House");

        if (_currentHouse != null)
            Destroy(_currentHouse.gameObject);

        HouseOption houseOption = (HouseOption)houseOptionSet.GetOptionByID(GlobalReferences._JSON.HouseType);
        if(houseOption == null)
        {
            Debug.LogWarning("invalid house ID " + GlobalReferences._JSON.HouseType);
            houseOption = (HouseOption)houseOptionSet.GetOptionByID(1);
            return;
        }
        _currentHouse = Instantiate(houseOption._housePrefab, rootPosition);
        _currentHouse.transform.localPosition = Vector3.zero;
        _currentHouse.transform.localScale = Vector3.one;
        _currentHouse.transform.localRotation = Quaternion.identity;

        MaterialOption roofMaterialOption = (MaterialOption)roofMaterialOptionSet.GetOptionByID(GlobalReferences._JSON.RoofType);
        SetMaterial(roofMaterialOption);

        MaterialOption wallMaterialOption = (MaterialOption)wallMaterialOptionSet.GetOptionByID(GlobalReferences._JSON.BrickType);
        SetMaterial(wallMaterialOption);

        ColorOption colorOption = (ColorOption)colorOptionSet.GetOptionByID(GlobalReferences._JSON.ColorType);
        SetColor(colorOption);

        if (GlobalReferences._JSON.BigDogs >= 0)
        {
            BigDogOption bigDogOption = (BigDogOption)bigDogOptionSet.GetOptionByID(GlobalReferences._JSON.BigDogs);
            LoadNewBigDog(bigDogOption);
        }

        if (GlobalReferences._JSON.SmallDogs >= 0)
        {
            SmallDogOption smallDogOption = (SmallDogOption)smallDogOptionSet.GetOptionByID(GlobalReferences._JSON.SmallDogs);
            LoadNewSmallDog(smallDogOption);
        }

        if (GlobalReferences._JSON.Cats >= 0)
        {
            CatOption catOption = (CatOption)catOptionSet.GetOptionByID(GlobalReferences._JSON.Cats);
            LoadNewCat(catOption);
        }

        if (GlobalReferences._JSON.SportBalls >= 0)
        {
            SportsOption sportsOption = (SportsOption)sportsOptionSet.GetOptionByID(GlobalReferences._JSON.SportBalls);
            LoadNewSports(sportsOption);
        }


        _currentHouse.LoadInfoVis(GlobalReferences._JSON.PanelCount, GlobalReferences._JSON.PercentSavings, GlobalReferences._JSON.AverageBill);
        _currentHouse.LoadYardSign(GlobalReferences._JSON.YardSign);

        if (animateIn)
        {
            _currentHouse.gameObject.SetActive(false);
            _currentHouse.gameObject.SetActive(true);
            _currentHouse.TurnOnAnimator();
        }
    }



    /// <summary>
    /// Saves HouseOption. Reloads house with new house prefab
    /// </summary>
    /// <param name="opt"></param>
    public void LoadHouseOption(HouseOption opt)
    {
        if (opt == null) return;
        GlobalReferences._JSON.HouseType = opt._id;
        /*
        GlobalReferences._JSON.BigDogs = -1;
        GlobalReferences._JSON.SmallDogs = -1;
        GlobalReferences._JSON.Cats = -1;
        GlobalReferences._JSON.SportBalls = -1;
        */
        SpawnHouse();
    }

    /// <summary>
    /// Saves MaterialOption. Sets materials on current house if there is one
    /// </summary>
    /// <param name="opt"></param>
    public void SetMaterial(MaterialOption opt)
    {
        if (opt == null) return;
        switch (opt._family)
        {
            case MaterialFamily.WALL:
                GlobalReferences._JSON.BrickType = opt._id;
                if (_currentHouse) _currentHouse.SetWallMaterial(opt._material);
                break;

            case MaterialFamily.ROOF:
                GlobalReferences._JSON.RoofType = opt._id;
                if (_currentHouse) _currentHouse.SetRoofMaterial(opt._material);
                break;
        }
    }


    public void SetColor(ColorOption opt)
    {
        if (opt == null) return;
        GlobalReferences._JSON.ColorType = opt._id;

        if (_currentHouse)
        {
            _currentHouse.SetWallColor(opt._color);
        }
    }

    public void LoadNewBigDog(BigDogOption bigDogOpt)
    {
        if (bigDogOpt == null) return;
        GlobalReferences._JSON.BigDogs = bigDogOpt._id;

        if (_currentHouse)
        {
            _currentHouse.LoadNewBigDog(bigDogOpt._bigDogPrefab);
        }

    }

    public void LoadNewSmallDog(SmallDogOption smallDogOpt)
    {
        if (smallDogOpt == null) return;
        GlobalReferences._JSON.SmallDogs = smallDogOpt._id;

        if (_currentHouse)
        {
            _currentHouse.LoadNewSmallDog(smallDogOpt._smallDogPrefab);
        }
    }

    public void LoadNewCat(CatOption catOpt)
    {
        if (catOpt == null) return;
        GlobalReferences._JSON.Cats = catOpt._id;

        if (_currentHouse)
        {
            _currentHouse.LoadNewCat(catOpt._catPrefab);
        }
    }

    public void LoadNewSports(SportsOption sportsOpt)
    {
        if (sportsOpt == null) return;
        GlobalReferences._JSON.SportBalls = sportsOpt._id;

        if (_currentHouse)
        {
            _currentHouse.LoadNewSports(sportsOpt._sportsPrefab);
        }
    }
}
