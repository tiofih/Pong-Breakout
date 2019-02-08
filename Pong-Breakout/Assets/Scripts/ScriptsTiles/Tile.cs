using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tile : MonoBehaviour
{
    protected PGrid grid;
    [SerializeField] protected Vector3 _collSize;
    protected bool moveSide = false;
    protected bool isPlaced = false;

    private void Awake()
    {
        GetTileComponents();
    }

    private void Update()
    {
        Clamp();
        Movement();
    }

    public abstract bool CanReflect();
    public abstract void ApplyEffect();
    public virtual void Movement()
    {
        if (!isPlaced)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += grid.GetNearestPointOnGrid(Vector3.down);
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position += grid.GetNearestPointOnGrid(Vector3.up);
            }
            if (moveSide)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    transform.position += grid.GetNearestPointOnGrid(Vector3.left);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    transform.position += grid.GetNearestPointOnGrid(Vector3.right);
                }
            }
        }
    }
    public virtual void Clamp()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, PUnits.MinX, PUnits.MaxX),
            Mathf.Clamp(transform.position.y, PUnits.MinY, PUnits.MaxY),
            0f
        );
    }
    public virtual void ActiveCollider()
    {
        GetComponent<Collider2D>().enabled = true;
    }

    public virtual void DeactiveCollider()
    {
        GetComponent<Collider2D>().enabled = false;
    }

    public virtual void GetTileComponents()
    {
        GetColliderSize();
        grid = FindObjectOfType<PGrid>();
    }

    protected void GetColliderSize()
    {
        if (transform.childCount > 1)
        {
            Debug.Log(transform.name + " have " + transform.childCount + " children");
            float value = -0.5f;
            foreach (BoxCollider coll in GetComponentsInChildren<BoxCollider>())
            {
                value += 0.5f;
            }
            _collSize.x = value;
            _collSize.y = value;
            _collSize.z = 0f;
            Debug.Log(_collSize);
        }
        else if (GetComponent<BoxCollider>())
        {
            _collSize = GetComponent<BoxCollider>().size;
        }
        else if (GetComponentInChildren<BoxCollider>())
        {
            _collSize = GetComponentInChildren<BoxCollider>().size;
        }
        else
        {
            Debug.LogWarning("Collider not found");
        }
    }

}
