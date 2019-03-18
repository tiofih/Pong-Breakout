using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{

    [Range(1, 7)]
    [SerializeField] private int tilePoints;
    [SerializeField] private Color[] colors;
    private Renderer rend;

    public static event Action OnTileDestroyed = delegate { };

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = colors[tilePoints - 1];
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Ball"))
        {
            if (tilePoints <= 1)
            {
                OnTileDestroyed();
                Destroy(gameObject);
            }
            else
            {
                tilePoints--;
            }
            rend.material.color = colors[tilePoints - 1];
        }
    }
}
