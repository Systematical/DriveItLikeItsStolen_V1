using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Command
{
    Drive,SwitchDirection,LeftBrake,RightBrake,LeftTurn,RightTurn
}
public static class CustomInputScript {

    static Dictionary<Command, KeyCode> keyMapping;

    static CustomInputScript()
    {
        InitializeDictionary();
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<Command, KeyCode>()
        {
            { Command.Drive, KeyCode.UpArrow },
            { Command.SwitchDirection, KeyCode.DownArrow },
            { Command.LeftTurn, KeyCode.LeftArrow },
            { Command.RightTurn, KeyCode.None },
            { Command.RightBrake, KeyCode.None },
            { Command.LeftBrake, KeyCode.RightArrow }
        };
        
    }

    public static void SetKeyMap(Command keyMap, KeyCode key)
    {
        if (!keyMapping.ContainsKey(keyMap))
            throw new ArgumentException("Invalid KeyMap in SetKeyMap: " + keyMap);
        keyMapping[keyMap] = key;
    }

    public static bool GetKeyDown(Command keyMap)
    {
        return Input.GetKey(keyMapping[keyMap]);
    }
}
