using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadTile : Tile
{
    public PInputModel inputModel;
    public override void ApplyEffect()
    {
        return;
    }

    public override bool CanReflect()
    {
        return true;
    }

    public override void Movement()
    {
        if (Input.GetKey(inputModel._upKey))
        {
            transform.Translate(Vector3.up * PUnits.PadSpeed * Time.deltaTime);
        }
        if (Input.GetKey(inputModel._downKey))
        {
            transform.Translate(Vector3.down * PUnits.PadSpeed * Time.deltaTime);
        }
    }

    public override void Clamp()
    {
        var clampY = Mathf.Clamp(transform.position.y, PUnits.MinY + _collSize.y, PUnits.MaxY - _collSize.y);
        var clampVector = new Vector3(transform.position.x,
                                     clampY,
                                     transform.position.z);
        transform.position = clampVector;
    }
}
