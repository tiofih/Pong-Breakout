using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum PInputKeys
{
    None = 0,
    Invalid = -1,

    Up = 1 << 0,
    Down = 1 << 1,
    Left = 1 << 2,
    Right = 1 << 3,

    Action = 1 << 4

}

public static class PInputKeysExtensions
{
    public static PInputKeys SetFlag(this PInputKeys self, PInputKeys value)
    {
        return self | value;
    }

    public static PInputKeys ResetFlag(this PInputKeys self, PInputKeys value)
    {
        return self & ~value;
    }

    public static PInputKeys AssignFlag(this PInputKeys self, PInputKeys value, bool state)
    {
        return state ? self.SetFlag(value) : self.ResetFlag(value);
    }
}
