using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum gamemode { none, history, endless}

public static class UserInformation
{
    public static gamemode gameMode = gamemode.none;

    public static void LoadInformation()
    {

    }
}
