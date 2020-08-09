using System;



[Serializable]
public class CustomizableJSON
{
    public int ID;
    public int HouseType;
    public int RoofType;
    public int BrickType;
    public int ColorType;
    public int BigDogs;
    public int SmallDogs;
    public int Cats;
    public int SportBalls;
    public string YardSign;
    public int PanelCount;
    public int PercentSavings;
    public float AverageBill;
    public string SharedBy;
    public string SharedTo;

    

    public CustomizableJSON()
    {
        ID = 1;
        HouseType = 6;
        RoofType = 1;
        BrickType = 1;
        ColorType = 0;
        BigDogs = 0;
        SmallDogs = 0;
        Cats = 0;
        SportBalls = 0;
        YardSign = "";
        PanelCount = 32;
        PercentSavings = 75;
        AverageBill = 40;
        SharedBy = "";
        SharedTo = "";
    }
}