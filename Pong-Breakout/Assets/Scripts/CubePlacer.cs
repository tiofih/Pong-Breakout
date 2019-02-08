using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubePlacer : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private GameObject tile;
    private Renderer tileRenderer;
    private PGrid grid;
    [SerializeField]
    private List<Vector3> positionList;

    private void Awake()
    {
        grid = FindObjectOfType<PGrid>();
    }

    private void Update()
    {
        if (tile == null)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {

                PlaceCubeNear(Vector3.zero);

            }
        }
        else
        {
            MovePlacedCube();
        }
    }

    private void PlaceCubeNear(Vector3 point)
    {
        if (tile == null)
        {
            var finalPosition = grid.GetNearestPointOnGrid(point);
            if (CanPlace(finalPosition))
            {
                tile = Instantiate(tilePrefab, finalPosition + Vector3.up, Quaternion.identity);
                tileRenderer = tile.GetComponentInChildren<Renderer>();
            }
            else
            {
                Debug.Log("Cant Place");
            }
        }
    }

    private void MovePlacedCube()
    {
        if (tile != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                tile.transform.position += grid.GetNearestPointOnGrid(Vector3.left / 5f);
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                tile.transform.position += grid.GetNearestPointOnGrid(Vector3.right / 5f);
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                tile.transform.position += grid.GetNearestPointOnGrid(Vector3.forward / 5f);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                tile.transform.position += grid.GetNearestPointOnGrid(Vector3.back / 5f);
            }

            if (CanPlace(tile.transform.position + Vector3.down))
            {
                tileRenderer.material.color = Color.blue;
                if (Input.GetKeyDown(KeyCode.Space) && tile != null)
                {
                    tile.transform.position += Vector3.down;
                    positionList.Add(tile.transform.position);
                    tile = null;
                }
            }
            else
            {
                tileRenderer.material.color = Color.red;
            }
        }
    }

    private bool CanPlace(Vector3 position)
    {
        foreach (Vector3 pos in positionList)
        {
            if (position == pos)
            {
                return false;

            }
        }
        return true;
    }
}
