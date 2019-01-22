using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour
{
    private Vector3 _angle;
    private Vector3 _direction;
    private float _fixAngle;
    private float _speed = 1f;
    private float _maxY;
    private float _minY;
    private float _maxX;
    private float _minX;
    private int _side = 1;

    private void OnEnable()
    {
        GameManager.Instance.playingGameDelegate += CallMov;
        GameManager.Instance.playingGameDelegate += WallHit;
    }

    private void Start()
    {
        _fixAngle = UnityEngine.Random.Range(0, 1f);
        _maxX = GameManager.Instance.MaxX;
        _minX = GameManager.Instance.MinX;
        _maxY = GameManager.Instance.MaxY;
        _minY = GameManager.Instance.MinY;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        _side *= -1;

        if (other.gameObject.CompareTag("Player 1") || other.gameObject.CompareTag("Player 2"))
        {
            ReflectBall(other.gameObject);
        }

        if (other.gameObject.CompareTag("Line"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
        if (other.gameObject.CompareTag("Tile"))
        {
            Destroy(other.gameObject);
        }
    }

    private void MoveBall(Vector3 horizontalDir, Vector3 verticalDir, float ballSpeed, float difAngle)
    {
        _angle = (horizontalDir * difAngle + verticalDir).normalized;
        transform.Translate(_angle * ballSpeed * Time.deltaTime);
    }

    private void CallMov(object e, EventArgs a)
    {
        if (_side == 1)
        {
            MoveBall(_direction, Vector3.right, _speed, _fixAngle);
        }
        if (_side == -1)
        {
            MoveBall(_direction, Vector3.left, _speed, _fixAngle);
        }
    }



    private void ReflectBall(GameObject c)
    {
        if (transform.position.y == c.transform.position.y)
        {
            _fixAngle = UnityEngine.Random.Range(0, 10f);
            _speed++;
        }
        else if (transform.position.y >= c.transform.position.y + .5f &&
                 transform.position.y <= c.transform.position.y + .8f ||
                 transform.position.y <= c.transform.position.y - .5f &&
                 transform.position.y >= c.transform.position.y + .8f)
        {
            _fixAngle = .5f;
            _speed += 2f;
        }
        else if (transform.position.y >= c.transform.position.y + .8f &&
                 transform.position.y <= c.transform.position.y - .8f)
        {
            _fixAngle = 1f;
            _speed += 5f;
        }

        if (transform.position.y > c.transform.position.y)
        {
            _direction = Vector3.up;
        }
        else if (transform.position.y < c.transform.position.y)
        {
            _direction = Vector3.down;
        }
    }

    private void WallHit(object e, EventArgs a)
    {
        if (transform.position.y > _maxY)
        {
            _direction = Vector3.down;
            _fixAngle = 1f;
        }
        else if (transform.position.y < _minY)
        {
            _direction = Vector3.up;
            _fixAngle = 1f;
        }

        if (transform.position.x >= _maxX)
        {
            _speed = 1f;
            transform.position = Vector3.zero;
            GameManager.Instance.SumScore("Player 1");

        }
        else if (transform.position.x <= _minX)
        {
            _speed = 1f;
            transform.position = Vector3.zero;
            GameManager.Instance.SumScore("Player 2");
        }


    }
}
