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

    static KeyCode[] defaults = new KeyCode[6]
    {
        KeyCode.Q,
        KeyCode.E,
        KeyCode.UpArrow,
        KeyCode.DownArrow,
        KeyCode.LeftArrow,
        KeyCode.RightArrow

    };

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
            { Command.RightTurn, KeyCode.RightArrow }

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
