using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    public abstract bool CanReflect();
    public abstract void ApplyEffect();
    public virtual void ActiveCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }
    public virtual void DeactiveCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

}
