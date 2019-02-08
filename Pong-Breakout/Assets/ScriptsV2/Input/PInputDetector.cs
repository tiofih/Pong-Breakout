using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PInputDetector
{
    private readonly string[] knowAxes;

    public PInputDetector()
    {
        this.knowAxes = new[]{
            "Joy1_Up",
            "Joy1_Down",
            "Joy1_Left",
            "Joy1_Right",

            "Joy2_Up",
            "Joy2_Down",
            "Joy2_Left",
            "Joy2_Right"
        };
    }

    public KeyCode DetectKeyPresses()
    {
        foreach (KeyCode code in Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(code))
            {
                return code;
            }
        }

        return KeyCode.None;
    }

    public string DetectAxisMovement()
    {
        foreach (string axis in this.knowAxes)
        {
            if (Input.GetAxis(axis) > 0.5f)
            {
                return axis;
            }
        }

        return "";
    }


}
