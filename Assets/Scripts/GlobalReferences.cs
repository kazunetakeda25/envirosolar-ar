using System;
using UnityEngine;

public static class GlobalReferences
{
    public static string SERVER_URL = "https://hmi.pwa.mybluehost.me/";
    public static string SERVER_API_URL = "https://hmi.pwa.mybluehost.me/index.php/API/";
    public static UserData _UserData = new UserData();
    private static CustomizableJSON m_JSON;
    public static CustomizableJSON _JSON
    {
        get
        {
            if (m_JSON == null) m_JSON = new CustomizableJSON();
            return m_JSON;
        }
        set
        {
            m_JSON = value;
        }
    }

    public static void LoadOrCreateUserData(string fileName = "UserData")
    {
        if (FileTool.IsFileExists(fileName) == false)
        {
            Debug.Log("Created UserData");
            string dataAsJson = JsonUtility.ToJson(_UserData);
            FileTool.CreateOrWriteFile(fileName, dataAsJson);
        }
        else
        {
            string dataAsJson = FileTool.ReadFile(fileName, false);
            _UserData = JsonUtility.FromJson<UserData>(dataAsJson);
        }
    }

    public static void SaveUserData(string fileName = "UserData")
    {
        string dataAsJson = JsonUtility.ToJson(_UserData);
        FileTool.CreateOrWriteFile(fileName, dataAsJson);
    }

    public static void LoadUserData(string fileName = "UserData")
    {
        string dataAsJson = FileTool.ReadFile(fileName, false);
        _UserData = JsonUtility.FromJson<UserData>(dataAsJson);
    }
}

[Serializable]
public class JSONItem
{
    public int id;
    public int housetype;
    public int rooftype;
    public int bricktype;
    public int colortype;
    public int bigdogs;
    public int smalldogs;
    public int cats;
    public int sportballs;
    public string yardsign;
    public int panelcount;
    public int percentsavings;
    public float averagebill;
    public int created_by;
    public DateTime created_at;
    public string firstname;
    public string lastname;
    public string phone;

    JSONItem()
    {
        id = 0;
        housetype = 0;
        rooftype = 0;
        bricktype = 0;
        colortype = 0;
        bigdogs = -1;
        smalldogs = -1;
        cats = -1;
        sportballs = -1;
        yardsign = "";
        panelcount = 0;
        percentsavings = 0;
        averagebill = 0;
        created_by = 0;
        created_at = new DateTime();
        firstname = "";
        lastname = "";
        phone = "";
    }
}

[Serializable]
public class JSONItemsArray
{
    public JSONItem[] jsonItems;
}