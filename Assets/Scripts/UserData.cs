using System;

[Serializable]
public class UserData
{
    public bool IsStaff;

    public string FirstName;
    public string LastName;
    public string PhoneNumber;
    public string Email;
    public string ZipCode;

    public int PanelCount;
    public int PercentSavings;
    public float AverageBill;

    public CustomizableJSON SLOT1;
    public CustomizableJSON SLOT2;
    public CustomizableJSON SLOT3;
    public CustomizableJSON SLOT4;

    public int SlotSaveIndex;
    
    public bool IsFirstOpenedApp;

    public UserData()
    {
        IsStaff = false;
        FirstName = "";
        LastName = "";
        Email = "";
        PhoneNumber = "";
        ZipCode = "";
        PanelCount = 0;
        PercentSavings = 0;
        AverageBill = 0.00f;
        SLOT1 = SLOT2 = SLOT3 = SLOT4 = null;
        SlotSaveIndex = 0;
        IsFirstOpenedApp = true;
    }
}
