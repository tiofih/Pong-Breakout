using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PadManager : MonoBehaviour
{

    public float minDist = 1f;
    public KeyCode upKey;
    public KeyCode downKey;
    public KeyCode rightKey;
    public KeyCode leftKey;
    public KeyCode confirmKey;
    public GameObject linePrefab;
    private GameObject line;
    private LineScript lineScript;
    private float speed;
    private float maxY = 2f;
    private float minY = -2f;
    private float maxX;
    private float minX;
    private Vector2 collSize;
    private Vector2 lineCollSize;

    private void OnEnable()
    {
        GameManager.Instance.playingGameDelegate += MovePad;
        GameManager.Instance.playingGameDelegate += ClampPad;
        GameManager.Instance.playingGameDelegate += MoveAndPlaceLine;
    }

    private void Awake()
    {
        speed = GameManager.Instance.padSpeed;
        maxY = GameManager.Instance.maxY;
        minY = GameManager.Instance.minY;
        maxX = GameManager.Instance.maxX;
        minX = GameManager.Instance.minX;
        collSize = GetComponent<BoxCollider2D>().size;
    }

    private void MovePad(object e, EventArgs a)
    {
        if (Input.GetKey(upKey))
        {
            transform.Translate(Vector3.up * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(downKey))
        {
            transform.Translate(Vector3.down * speed * Time.deltaTime, Space.World);
        }
    }

    private void ClampPad(object e, EventArgs a)
    {

        var clampY = Mathf.Clamp(transform.position.y, minY + (collSize.y / 2), maxY - (collSize.y / 2));
        var clampVector = new Vector3(transform.position.x,
                                     clampY,
                                     transform.position.z);
        transform.position = clampVector;
    }

    private void MoveAndPlaceLine(object e, EventArgs a)
    {
        if (line == null)
        {
            if (Input.GetKeyDown(confirmKey))
            {
                line = Instantiate(linePrefab);
                lineScript = line.GetComponent<LineScript>();
                line.transform.position = Vector3.zero;
                lineCollSize = line.GetComponent<BoxCollider2D>().size;
            }
        }
        else if (line != null)
        {
            int x = 0;
            int y = 0;
            if (Input.GetKey(upKey))
            {
                y = 1;
            }
            if (Input.GetKey(downKey))
            {
                y = -1;
            }
            if (Input.GetKey(rightKey))
            {
                x = 1;
            }
            if (Input.GetKey(leftKey))
            {
                x = -1;
            }
            lineScript.MoveLine(x, y);

            if (Input.GetKeyDown(confirmKey))
            {
                if (lineScript.canStay)
                {
                    Destroy(lineScript.rb);
                    Destroy(lineScript.col);
                    lineScript = null;
                    line = null;
                }
            }
            if (line != null)
            {
                var clampLineX = 0f;
                if (this.CompareTag("Player 1"))
                {
                    clampLineX = Mathf.Clamp(line.transform.position.x, minX, 0);

                }
                else if (this.CompareTag("Player 2"))
                {
                    clampLineX = Mathf.Clamp(line.transform.position.x, 0, maxX - (collSize.x / 2));
                }
                var clampLineY = Mathf.Clamp(line.transform.position.y, minY + (lineCollSize.y / 2), maxY - (lineCollSize.y / 2));
                var clampLineVector = new Vector3(clampLineX, clampLineY, 0f);
                line.transform.position = clampLineVector;
            }
        }
    }
}
