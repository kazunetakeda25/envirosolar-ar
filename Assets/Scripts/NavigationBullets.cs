using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;

public class NavigationBullets : MonoBehaviour
{
    [SerializeField] public HorizontalScrollSnap hScrollSnapScript;
    
    public void Navigate(bool isOn)
    {
        if (isOn == true)
        {
            hScrollSnapScript.GoToScreen(GetComponent<ToggleGroup>().ActiveToggles().First().transform.GetSiblingIndex());
        }
    }

    public void NavigatePage()
    {
        transform.GetChild(hScrollSnapScript._currentPage).GetComponent<Toggle>().isOn = true;
    }
}
