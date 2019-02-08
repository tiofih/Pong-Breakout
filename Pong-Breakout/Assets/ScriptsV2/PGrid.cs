using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PGrid : MonoBehaviour
{
    public List<Vector3> positionList;

    public Vector3 GetNearestPointOnGrid(Vector3 position)
    {

        position -= transform.position;

        int xCount = Mathf.RoundToInt(position.x / PUnits.GridCellSize);
        int yCount = Mathf.RoundToInt(position.y / PUnits.GridCellSize);
        int zCount = Mathf.RoundToInt(position.z / PUnits.GridCellSize);

        Vector3 result = new Vector3(
            (float)xCount * PUnits.GridCellSize,
            (float)yCount * PUnits.GridCellSize,
            (float)zCount * PUnits.GridCellSize
        );

        result += transform.position;

        return result;
    }
}
