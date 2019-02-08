using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PadManager : MonoBehaviour
{

    [SerializeField]
    private KeyCode _upKey;
    [SerializeField]
    private KeyCode _downKey;
    [SerializeField]
    private KeyCode _rightKey;
    [SerializeField]
    private KeyCode _leftKey;
    [SerializeField]
    private KeyCode _confirmKey;
    [SerializeField]
    private GameObject _linePrefab;
    private GameObject _line;
    private LineScript _lineScript;
    private float _speed;
    private float _maxY;
    private float _minY;
    private float _maxX;
    private float _minX;
    private Vector2 _collSize;
    private Vector2 _lineCollSize;

    private void OnEnable()
    {
        GameManager.Instance.playingGameDelegate += MovePad;
        GameManager.Instance.playingGameDelegate += ClampPad;
        GameManager.Instance.playingGameDelegate += MoveAndPlaceLine;
    }

    private void Awake()
    {
        _speed = PUnits.PadSpeed;
        _maxY = PUnits.MaxY;
        _minY = PUnits.MinY;
        _maxX = PUnits.MaxX;
        _minX = PUnits.MinX;
        _collSize = GetComponent<BoxCollider2D>().size;
    }

    private void MovePad(object e, EventArgs a)
    {
        if (Input.GetKey(_upKey))
        {
            transform.Translate(Vector3.up * _speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(_downKey))
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime, Space.World);
        }
    }

    private void ClampPad(object e, EventArgs a)
    {

        var clampY = Mathf.Clamp(transform.position.y, _minY + (_collSize.y / 2), _maxY - (_collSize.y / 2));
        var clampVector = new Vector3(transform.position.x,
                                     clampY,
                                     transform.position.z);
        transform.position = clampVector;
    }

    private void MoveAndPlaceLine(object e, EventArgs a)
    {
        if (_line == null)
        {
            if (Input.GetKeyDown(_confirmKey))
            {
                _line = Instantiate(_linePrefab);
                _lineScript = _line.GetComponent<LineScript>();

                _line.transform.position = Vector3.zero;
                _lineCollSize = _line.GetComponent<BoxCollider2D>().size;
            }
        }
        else if (_line != null)
        {
            int x = 0;
            int y = 0;
            if (Input.GetKey(_upKey))
            {
                y = 1;
            }
            if (Input.GetKey(_downKey))
            {
                y = -1;
            }
            if (Input.GetKey(_rightKey))
            {
                x = 1;
            }
            if (Input.GetKey(_leftKey))
            {
                x = -1;
            }
            _lineScript.MoveLine(x, y);

            if (Input.GetKeyDown(_confirmKey))
            {
                if (_lineScript.canStay)
                {
                    Tile[] tiles = _lineScript.GetComponentsInChildren<Tile>();
                    foreach (Tile tile in tiles)
                    {
                        tile.ActiveCollider();
                    }
                    Destroy(_lineScript.rb);
                    _lineScript = null;
                    _line = null;
                }
            }
            if (_line != null)
            {
                var clampLineX = 0f;
                if (this.CompareTag("Player 1"))
                {
                    clampLineX = Mathf.Clamp(_line.transform.position.x, _minX, 0);

                }
                else if (this.CompareTag("Player 2"))
                {
                    clampLineX = Mathf.Clamp(_line.transform.position.x, 0, _maxX - (_collSize.x / 2));
                }
                var clampLineY = Mathf.Clamp(_line.transform.position.y, _minY + (_lineCollSize.y / 2), _maxY - (_lineCollSize.y / 2));
                var clampLineVector = new Vector3(clampLineX, clampLineY, 0f);
                _line.transform.position = clampLineVector;
            }
        }
    }
}
