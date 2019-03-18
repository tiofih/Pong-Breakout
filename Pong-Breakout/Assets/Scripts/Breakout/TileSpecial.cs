using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpecial : MonoBehaviour
{

    [SerializeField] private GameObject specialPrefab;

    private void Start()
    {
        Tile.OnTileDestroyed += DestroyEffect;
    }

    public void DestroyEffect()
    {
        Instantiate(specialPrefab, transform.position, transform.rotation);
    }
}
