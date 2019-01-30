using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraBallTile : Tile
{
    public GameObject ball;

    public override void ApplyEffect()
    {
        Instantiate(ball, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public override bool CanReflect()
    {
        return false;
    }
}
