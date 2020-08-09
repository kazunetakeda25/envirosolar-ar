using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

[CreateAssetMenu(fileName = "OptionSet", menuName = "CustomOptions/OptionSet", order = 1)]
public class CustomOptionSet : CustomOption
{
    public Vector2 buttonSize = new Vector2(200, 200);

    [SerializeField]
    public List<CustomOption> options;

    public CustomOption GetOptionByID(int id)
    {
        return options.First(x => x._id == id);
    }
    
    public override void OnSelected()
    {
        if (options.Count > 0)
            HomeEditorUIManager.Instance.OpenEditorPanelWithOptions(this);

        if (options.Count == 0 && name == "NumberSet")
            GameObject.Find("NumbersPanel").transform.localScale = Vector3.one;

        /*if (options.Count == 0 && name == "ShareSet")
            GameObject.Find("SharePanel").transform.localScale = Vector3.one;

        if (options.Count == 0 && name == "SlotSet")
        {
            GameObject.Find("SavingSlotPanel").transform.localScale = Vector3.one;
            GameObject.Find("SavingSlotPanel").GetComponentInChildren<ToggleGroup>().SetAllTogglesOff();
            GameObject.Find("SavingSlotPanel").GetComponent<SaveSlotPanel>().UpdateSlots();
        }*/
    }

}
