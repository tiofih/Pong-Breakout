using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleOrganize : MonoBehaviour
{
    [SerializeField] private GameObject PaddleTilePrefab;
    [SerializeField] private ColorModel colorModel;
    [SerializeField] private bool groupInZ = true;
    [SerializeField] private List<Renderer> rendList;
    private BoxCollider coll;

    private void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            rendList.Add(transform.GetChild(i).GetComponent<Renderer>());
        }
        coll = GetComponent<BoxCollider>();
        Organize();
        PaintTiles();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AddTile();
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            DeleteTile();
        }
    }

    public void AddTile()
    {
        GameObject tile = Instantiate(PaddleTilePrefab);
        tile.transform.position = transform.position;
        tile.transform.parent = transform;
        Organize();
        rendList.Add(tile.GetComponent<Renderer>());
        PaintTiles();
    }

    public void DeleteTile()
    {
        StartCoroutine(DeleteTileCoroutine());
    }

    private IEnumerator DeleteTileCoroutine()
    {
        if (transform.childCount > 2)
        {
            Destroy(transform.GetChild(0).gameObject);
            yield return null;
            rendList.RemoveAt(0);
            Organize();
            PaintTiles();
        }
    }

    private void Organize()
    {
        float count = transform.childCount;
        coll.size = new Vector3(1, 1, count);
        float step = 0.5f;
        float firstPos = -(count / 2) + step;
        float lastPos = (count / 2) - step;
        count--;
        for (float i = firstPos; i <= lastPos; i++)
        {
            Vector3 organizedVector = groupInZ ? new Vector3(0, 0, i) : new Vector3(i, 0, 0);
            transform.GetChild((int)count--).localPosition = organizedVector;
        }
    }

    private void PaintTiles()
    {
        for (int i = 0; i < rendList.Count; i++)
        {
            rendList[i].material.color = colorModel.middleColor;
        }
        rendList.First().material.color = colorModel.firstColor;
        rendList.Last().material.color = colorModel.firstColor;
    }
}
