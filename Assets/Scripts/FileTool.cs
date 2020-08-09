using System;
using System.IO;
using UnityEngine;

public class FileTool
{
    public static string RootPath
    {
        get
        {
            if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.Android)
            {
                string tempPath = Application.persistentDataPath, dataPath;
                if (!string.IsNullOrEmpty(tempPath))
                {

                    dataPath = PlayerPrefs.GetString("DataPath", "");
                    if (string.IsNullOrEmpty(dataPath))
                    {
                        PlayerPrefs.SetString("DataPath", tempPath);
                    }

                    return tempPath + "/";
                }
                else
                {
                    Debug.Log("Application.persistentDataPath Is Null.");

                    dataPath = PlayerPrefs.GetString("DataPath", "");

                    return dataPath + "/";
                }
            }
            else if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.OSXEditor)
            {
                return Application.dataPath.Replace("Assets", "");
            }
            else
            {
                return Application.dataPath + "/";
            }
        }
    }

    public static void CreateOrWriteFile(string fileName, string info)
    {
        FileStream fs = new FileStream(RootPath + fileName, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fs);
        fs.SetLength(0);
        sw.WriteLine(info);
        sw.Close();
        sw.Dispose();
    }

    public static string ReadFile(string fileName, bool onlyreadline = true)
    {
        string fileContent = "";
        StreamReader sr = null;
        try
        {
            sr = File.OpenText(RootPath + fileName);
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return null;
        }
        if (onlyreadline)
        {
            while ((fileContent = sr.ReadLine()) != null)
            {
                break;
            }
        }
        else
        {
            fileContent = sr.ReadToEnd();
        }
        sr.Close();
        sr.Dispose();
        return fileContent;
    }

    public static bool IsFileExists(string fileName)
    {
        return File.Exists(RootPath + fileName);
    }

    public static void CopyFile(string from, string to, bool overWrite)
    {
        File.Copy(from, to, overWrite);
    }
}