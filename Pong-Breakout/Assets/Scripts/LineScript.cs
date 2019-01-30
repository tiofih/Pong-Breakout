using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineScript : MonoBehaviour
{
    public bool canStay;
    public BoxCollider2D col;
    public Rigidbody2D rb;

    private void OnEnable()
    {
        col = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (transform.childCount < 1)
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveLine(int x, int y)
    {
        if (x > 0)
        {
            transform.position += Vector3.right * Time.deltaTime * GameManager.Instance.PadSpeed;
        }
        if (x < 0)
        {
            transform.position += Vector3.left * Time.deltaTime * GameManager.Instance.PadSpeed;
        }
        if (y > 0)
        {
            transform.position += Vector3.up * Time.deltaTime * GameManager.Instance.PadSpeed;
        }
        if (y < 0)
        {
            transform.position += Vector3.down * Time.deltaTime * GameManager.Instance.PadSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            canStay = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Line"))
        {
            canStay = true;
        }

    }
}
