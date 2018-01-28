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
    static PairScript[] pairs;
    static CustomInputScript()
    {
        InitializeDictionary();
        exceptions = new List<Command>();
        pairs = new PairScript[4];
    }

    private static void InitializeDictionary()
    {
        keyMapping = new Dictionary<Command, KeyCode>()
        {
            { Command.Drive, KeyCode.None },
            { Command.SwitchDirection, KeyCode.None },
            { Command.LeftTurn, KeyCode.None },
            { Command.RightTurn, KeyCode.None },
            { Command.RightBrake, KeyCode.None },
            { Command.LeftBrake, KeyCode.None },
            { Command.Radio, KeyCode.None },
            { Command.Alarm, KeyCode.None },
            { Command.None, KeyCode.None }
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

    public static void updatePair(PairScript p2, int pairID)
    {
        InitializeDictionary();
        exceptions = new List<Command>();
        pairs[pairID] = p2;
        foreach(PairScript p in pairs)
        {
            if(p.objectConnections[0].myKey != KeyCode.None)
            {
                if(p.objectConnections[1].myKey != KeyCode.None)
                {
                    continue;
                }
                else
                {
                    SetKeyMap(p.objectConnections[1].myCommand, p.objectConnections[0].myKey);
                }
            }

            else if (p.objectConnections[1].myKey != KeyCode.None)
            {
                if (p.objectConnections[0].myKey != KeyCode.None)
                {
                    continue;
                }
                else
                {
                    SetKeyMap(p.objectConnections[0].myCommand, p.objectConnections[1].myKey);
                }
            }

            else if (p.objectConnections[0].myCommand != Command.None && p.objectConnections[1].myCommand != Command.None)
            {
                exceptions.Add(p.objectConnections[0].myCommand);
                exceptions.Add(p.objectConnections[1].myCommand);
            }
        }
    }

    //public static void setPair(SocketScript o1, SocketScript o2)
    //{
    //    Debug.Log(o1.myCommand);
    //    Debug.Log(o2.myCommand);
    //    if (!(o1.myKey == KeyCode.None))
    //    {
    //        SetKeyMap(o2.myCommand, o1.myKey);
    //    }
    //    else if (!(o2.myKey == KeyCode.None))
    //    {
    //        SetKeyMap(o1.myCommand, o2.myKey);
    //    }
    //    else
    //    {
    //        if (o1.myCommand != Command.None && o2.myCommand != Command.None)
    //        {
    //            exceptions.Add(o1.myCommand);
    //            exceptions.Add(o2.myCommand);
    //        }
    //        else
    //        {
    //            if(o1.myCommand != Command.None)
    //            {
    //                SetKeyMap(o1.myCommand, KeyCode.None);
    //            }
    //            else if(o2.myCommand != Command.None)
    //            {
    //                SetKeyMap(o2.myCommand, KeyCode.None);
    //            }
    //        }
    //    }
    //}
}
