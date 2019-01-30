using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTile : Tile
{
    public override void ApplyEffect()
    {
        Destroy(gameObject);
    }

    public override bool CanReflect()
    {
        return true;
    }
}
