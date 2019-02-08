using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleTile : Tile
{

    private void OnEnable()
    {
        base.moveSide = true;
    }
    private void PlaceCubeNear(Vector3 point)
    {
        var finalPosition = grid.GetNearestPointOnGrid(point);
        if (CanPlace(finalPosition))
        {
            //tile = Instantiate(tilePrefab, finalPosition + Vector3.up, Quaternion.identity);
            //tileRenderer = tile.GetComponentInChildren<Renderer>();
        }
        else
        {
            Debug.Log("Cant Place");
        }
    }

    private void MovePlacedCube()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += grid.GetNearestPointOnGrid(Vector3.left);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += grid.GetNearestPointOnGrid(Vector3.right);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position += grid.GetNearestPointOnGrid(Vector3.down);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.position += grid.GetNearestPointOnGrid(Vector3.up);
        }

        if (CanPlace(transform.position + Vector3.down))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                grid.positionList.Add(transform.position);
            }
        }
    }

    private bool CanPlace(Vector3 position)
    {
        foreach (Vector3 pos in grid.positionList)
        {
            if (position == pos)
            {
                return false;
            }
        }
        return true;
    }

    public override void ApplyEffect()
    {
        Destroy(gameObject);
    }

    public override bool CanReflect()
    {
        return true;
    }
}
