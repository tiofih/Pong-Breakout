using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PInputState
{
    public PInputKeys Current { get; set; }
    public PInputKeys Previous { get; set; }
    public bool IsLocal { get; }

    public PInputState(bool isLocal)
    {
        this.IsLocal = isLocal;
        this.Current = PInputKeys.None;
        this.Previous = PInputKeys.None;
    }

    public bool IsPressed(PInputKeys input)
    {
        return this.Current.HasFlag(input) && !this.Previous.HasFlag(input);
    }

    public bool IsHeld(PInputKeys input)
    {
        return this.Current.HasFlag(input);
    }

    public bool WasReleased(PInputKeys input)
    {
        return !this.Current.HasFlag(input) && this.Previous.HasFlag(input);
    }


}
