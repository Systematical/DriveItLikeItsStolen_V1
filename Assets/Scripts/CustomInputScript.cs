using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum Command
{
    Drive,SwitchDirection,LeftBrake,RightBrake,LeftTurn,RightTurn,Radio,Alarm,None
}


public static class CustomInputScript {

    static Dictionary<Command, KeyCode> keyMapping;
    static List<Command> exceptions;
    static CustomInputScript()
    {
        InitializeDictionary();
        exceptions = new List<Command>();
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<Command, KeyCode>()
        {
            { Command.Drive, KeyCode.None },
            { Command.SwitchDirection, KeyCode.DownArrow },
            { Command.LeftTurn, KeyCode.LeftArrow },
            { Command.RightTurn, KeyCode.None },
            { Command.RightBrake, KeyCode.None },
            { Command.LeftBrake, KeyCode.RightArrow },
            { Command.Radio, KeyCode.None },
            { Command.Alarm, KeyCode.None }
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
        if(exceptions.Contains(keyMap))
        {
            return (true);
        }
        else if (keyMapping[keyMap] == KeyCode.None)
        {
            return (false);
        }
        return Input.GetKey(keyMapping[keyMap]);
    }

    public static void setPair(SocketScript o1, SocketScript o2)
    {
        Debug.Log(o1.myCommand);
        Debug.Log(o2.myCommand);
        if (!(o1.myKey == KeyCode.None))
        {
            SetKeyMap(o2.myCommand, o1.myKey);
        }
        else if (!(o2.myKey == KeyCode.None))
        {
            SetKeyMap(o1.myCommand, o2.myKey);
        }
        else
        {
            if (o1.myCommand != Command.None && o2.myCommand != Command.None)
            {
                exceptions.Add(o1.myCommand);
                exceptions.Add(o2.myCommand);
            }
        }
        Debug.Log(keyMapping[Command.Drive]);
    }
}
