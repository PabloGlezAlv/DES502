using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public enum gamemode { none, history, endless}

public static class UserInformation
{
    public static gamemode gameMode = gamemode.none;

    static readonly string path = Application.dataPath + "data.json";

    public static PlayerInfo data = new PlayerInfo();

    public static void LoadInformation()
    {
        if(File.Exists(path))
        {
            string content = File.ReadAllText(path);
            data =  JsonUtility.FromJson<PlayerInfo>(content);
            Debug.Log("Loaded: " + content);
        }
        else
        {
            SaveInformation();
            Debug.Log("File no exist");
        }
    }

    public static void SaveInformation()
    {
        string JSON = JsonUtility.ToJson(data);

        File.WriteAllText(path, JSON);
    }
}
