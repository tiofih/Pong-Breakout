using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PKeyBinding
{

    public KeyCode PrimaryKey { get; set; }
    public KeyCode AlternativeKey { get; set; }
    public string AlternativeAxis { get; set; }
    public PInputKeys TriggeredKeys { get; set; }

    public bool IsAxis => this.AlternativeAxis != "";

    public PKeyBinding(KeyCode primaryKey, KeyCode alternativeKey, PInputKeys triggeredKeys)
    {
        this.PrimaryKey = primaryKey;
        this.AlternativeKey = alternativeKey;
        this.AlternativeAxis = "";
        this.TriggeredKeys = triggeredKeys;
    }

    public PKeyBinding(KeyCode primaryKey, string alternativeAxis, PInputKeys triggeredKeys)
    {
        this.PrimaryKey = primaryKey;
        this.AlternativeKey = KeyCode.None;
        this.AlternativeAxis = alternativeAxis;
        this.TriggeredKeys = triggeredKeys;
    }

    public PKeyBinding() { }

    public void Copy(PKeyBinding other)
    {
        this.PrimaryKey = other.PrimaryKey;
        this.AlternativeKey = other.AlternativeKey;
        this.AlternativeAxis = other.AlternativeAxis;
        this.TriggeredKeys = other.TriggeredKeys;
    }
}
