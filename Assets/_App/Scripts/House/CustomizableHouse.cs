using UnityEngine;
using UnityEngine.Playables;

[RequireComponent(typeof(PlayableDirector))]
public class CustomizableHouse : MonoBehaviour
{
    private MeshRenderer[] wallRenderers;
    private MeshRenderer[] roofRenderers;

    public Transform wallRoot;
    public Transform roofRoot;
    public Transform acdcRoot;

    public Transform signPosition;
    public Transform catPosition;
    public Transform dogPosition;
    public Transform smallDogPosition;
    public Transform sportPosition;



    private Color _wallColor = Color.white;
    private CustomizableBigDog _currentBigDog;
    private CustomizableSmallDog _currentSmallDog;
    private CustomizableCat _currentCat;
    private CustomizableSports _currentSports;
    private string _signName;

    private int _numPanels = 6;
    private float _percentSavings = 25;
    private float _averageBill = 100;

    [SerializeField] private YardSignHandler _currentYardSign;
    [SerializeField] private InfoVisualizationHandler _currentInfoVis;

    [SerializeField] private Animator animator;
    private PlayableDirector director;

    private AudioSource constructionSound;
    

    private bool HouseAnimationComplete => animator && animator.enabled && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1;

    private void Awake()
    {
        if (wallRoot)
        {
            wallRenderers = wallRoot.GetComponentsInChildren<MeshRenderer>();
        }
        if (roofRoot)
        {
            roofRenderers = roofRoot.GetComponentsInChildren<MeshRenderer>();
        }
        if (acdcRoot)
        {
            acdcRoot.gameObject.SetActive(false);
        }

        if (animator)
        {
            animator.enabled = false;
        }

        director = GetComponent<PlayableDirector>();
        constructionSound = GetComponent<AudioSource>();
    }

    public void TurnOnAnimator()
    {
        if (animator) animator.enabled = true;
        if (constructionSound) constructionSound.Play();
        dogPosition.gameObject.SetActive(false);
        smallDogPosition.gameObject.SetActive(false);
        catPosition.gameObject.SetActive(false);
        sportPosition.gameObject.SetActive(false);
        director.Play();

    }

    void Update()
    {
        
    }

    public void SetWallMaterial(Material mat)
    {
        SwapMaterials(wallRenderers, mat);
        SwapColors(wallRenderers, _wallColor);
    }

    public void SetRoofMaterial(Material mat)
    {
        SwapMaterials(roofRenderers, mat);
    }

    public void SetWallColor(Color color)
    {
        _wallColor = color;
        SwapColors(wallRenderers, color);
    }


    private void SwapMaterials(MeshRenderer[] renderers, Material mat)
    {
        if (renderers == null) return;
        foreach (MeshRenderer rend in renderers)
        {
            rend.material = mat;
        }
    }

    private void SwapColors(MeshRenderer[] renderers, Color color)
    {
        if (renderers == null) return;
        foreach (MeshRenderer rend in renderers)
        {
            rend.material.color = color;
        }
    }

    public void LoadInfoVis(int panels, float savings, float bill)
    {
        _numPanels = panels;
        _percentSavings = savings;
        _averageBill = bill;

        _currentInfoVis.SetNumberOfPanels(_numPanels);
        _currentInfoVis.SetPercentSavings(_percentSavings);
        _currentInfoVis.SetAverageBill(_averageBill);
    }

    public void LoadYardSign(string name)
    {
        if (name == "")
        {
            GlobalReferences._JSON.YardSign = GlobalReferences._UserData.LastName;
            _signName = GlobalReferences._UserData.LastName;
        }
        else
            _signName = name;

        _currentYardSign.SetName(_signName);
    }


    public void LoadNewBigDog(CustomizableBigDog bigDogPrefab)
    {
        if (_currentBigDog != null) Destroy(_currentBigDog.gameObject);
        _currentBigDog = Instantiate(bigDogPrefab, dogPosition.transform);
        _currentBigDog.transform.localPosition = Vector3.zero;

    }

    public void LoadNewSmallDog(CustomizableSmallDog smallDogPrefab)
    {
        if (_currentSmallDog != null) Destroy(_currentSmallDog.gameObject);
        _currentSmallDog = Instantiate(smallDogPrefab, smallDogPosition.transform);
        _currentSmallDog.transform.localPosition = Vector3.zero;

    }

    public void LoadNewCat(CustomizableCat catPrefab)
    {
        if (_currentCat != null) Destroy(_currentCat.gameObject);
        _currentCat = Instantiate(catPrefab, catPosition.transform);
        _currentCat.transform.localPosition = Vector3.zero;

    }

    public void LoadNewSports(CustomizableSports sportsPrefab)
    {
        if (_currentSports != null) Destroy(_currentSports.gameObject);
        _currentSports = Instantiate(sportsPrefab, sportPosition.transform);
        _currentSports.transform.localPosition = Vector3.zero;

    }
}