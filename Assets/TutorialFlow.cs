using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialFlow : MonoBehaviour
{
    public Sprite[] tutorialScreens;
    private int screenIndex = 0;
    private bool tutorialComplete = false;
    public bool forceTutorial = false;

    public Image[] unfilledDots;
    public RectTransform panelRoot;
    private Vector3 panelRootTarget;

    public PlaceTransformOnPlane placer;

    private const string prefsKey = "AR_TutorialCompleted";

    void Awake()
    {
        tutorialComplete = PlayerPrefs.GetInt(prefsKey) == 1;

        if (forceTutorial) tutorialComplete = false;
        if(tutorialScreens.Length == 0) TutorialComplete();

        if (!tutorialComplete)
        {
            placer.enabled = false;
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        swipe = new SwipeGestureRecognizer();
        swipe.StateUpdated += Swipe_Updated;
        swipe.DirectionThreshold = 0;
        swipe.MinimumNumberOfTouchesToTrack = swipe.MaximumNumberOfTouchesToTrack = SwipeTouchCount;
        swipe.PlatformSpecificView = Image;
        FingersScript.Instance.AddGesture(swipe);
        TapGestureRecognizer tap = new TapGestureRecognizer();
        tap.StateUpdated += Tap_Updated;
        FingersScript.Instance.AddGesture(tap);

        ShowScreen(0);
    }

    // Update is called once per frame
    void Update()
    {
        swipe.MinimumNumberOfTouchesToTrack = swipe.MaximumNumberOfTouchesToTrack = SwipeTouchCount;
        swipe.EndMode = SwipeMode;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ShowScreen(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ShowScreen(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ShowScreen(2);
        }
        panelRoot.localPosition = Vector3.Lerp(panelRoot.localPosition, panelRootTarget, Time.deltaTime * 10f);

    }

    private void TutorialComplete()
    {
        PlayerPrefs.SetInt(prefsKey, 1);
        tutorialComplete = true;
        placer.enabled = true;
    }

    private void ShowScreen(int index)
    {
        foreach(Image i in unfilledDots)
        {
            i.enabled = false;
        }
        unfilledDots[index].enabled = true;

        panelRootTarget = Vector3.right * (panelRoot.rect.width/3f) + Vector3.left * panelRoot.rect.width/3f *index;// + panelRoot.sizeDelta.x *2f / 3f * index);

    }


    [Tooltip("Set the required touches for the swipe.")]
    [Range(1, 10)]
    public int SwipeTouchCount = 1;

    [Tooltip("Controls how the swipe gesture ends. See SwipeGestureRecognizerSwipeMode enum for more details.")]
    public SwipeGestureRecognizerEndMode SwipeMode = SwipeGestureRecognizerEndMode.EndImmediately;

    public GameObject Image;

    private SwipeGestureRecognizer swipe;


    private void Tap_Updated(DigitalRubyShared.GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Ended)
        {
            Debug.Log("Tap");
        }
    }

    private void Swipe_Updated(DigitalRubyShared.GestureRecognizer gesture)
    {
        SwipeGestureRecognizer swipe = gesture as SwipeGestureRecognizer;
        if (swipe.State == GestureRecognizerState.Ended)
        {
            switch (swipe.EndDirection)
            {
                case SwipeGestureRecognizerDirection.Left:
                    SwipeLeft();
                    break;
                case SwipeGestureRecognizerDirection.Right:
                    SwipeRight();
                    break;
            }
        }
    }

    private void SwipeLeft()
    {
        screenIndex++;
        if (screenIndex > 2)
        {
            TutorialComplete();
            GameObject.Destroy(this.gameObject);
            return;
        }
        screenIndex = Mathf.Clamp(screenIndex, 0, 2);
        ShowScreen(screenIndex);
    }
    
    private void SwipeRight()
    {
        screenIndex--;
        screenIndex = Mathf.Clamp(screenIndex, 0, 2);
        ShowScreen(screenIndex);
    }
}