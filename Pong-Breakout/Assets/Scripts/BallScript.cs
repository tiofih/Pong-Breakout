using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BallScript : MonoBehaviour
{
    private Vector3 angle;
    private Vector3 direction;
    private float fixAngle;
    private float speed = 1f;
    private float maxY;
    private float minY;
    private float maxX;
    private float minX;
    private GameManager instance;

    private void OnEnable()
    {
        instance = GameManager.Instance;
        instance.playingGameDelegate += CallMov;
        instance.playingGameDelegate += WallHit;

    }

    private void Start()
    {
        fixAngle = UnityEngine.Random.Range(0, 1f);
        maxX = instance.maxX;
        minX = instance.minX;
        maxY = instance.maxY;
        minY = instance.minY;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player 1"))
        {
            instance.lp = lastPlayer.p1;
            this.tag = "Player 1";
            ReflectBall(other.gameObject);
        }
        if (other.gameObject.CompareTag("Player 2"))
        {
            instance.lp = lastPlayer.p2;
            this.tag = "Player 2";
            ReflectBall(other.gameObject);
        }
        if (other.gameObject.CompareTag("Tile"))
        {
            if (instance.lp == lastPlayer.p1)
            {
                instance.lp = lastPlayer.p2;
            }
            else
            {
                instance.lp = lastPlayer.p1;
            }

            ReflectBall(other.gameObject);
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Line"))
        {
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), other.collider);
        }
    }

    private void MoveBall(Vector3 horizontalDir, Vector3 verticalDir, float ballSpeed, float difAngle)
    {
        angle = (horizontalDir * difAngle + verticalDir).normalized;
        transform.Translate(angle * ballSpeed * Time.deltaTime);
    }

    private void CallMov(object e, EventArgs a)
    {
        if (this.CompareTag("Player 1"))
        {
            MoveBall(direction, Vector3.right, speed, fixAngle);
        }
        if (this.CompareTag("Player 2"))
        {
            MoveBall(direction, Vector3.left, speed, fixAngle);
        }
    }

    

    private void ReflectBall(GameObject c)
    {
        if (transform.position.y == c.transform.position.y)
        {
            fixAngle = UnityEngine.Random.Range(0, 10f);
            speed++;
        }
        else if (transform.position.y >= c.transform.position.y + .5f &&
                 transform.position.y <= c.transform.position.y + .8f ||
                 transform.position.y <= c.transform.position.y - .5f &&
                 transform.position.y >= c.transform.position.y + .8f)
        {
            fixAngle = .5f;
            speed += 2f;
        }
        else if (transform.position.y >= c.transform.position.y + .8f &&
                 transform.position.y <= c.transform.position.y - .8f)
        {
            fixAngle = 1f;
            speed += 5f;
        }

        if (transform.position.y > c.transform.position.y)
        {
            direction = Vector3.up;
        }
        else if (transform.position.y < c.transform.position.y)
        {
            direction = Vector3.down;
        }
    }

    private void WallHit(object e, EventArgs a)
    {
        if (transform.position.y > maxY)
        {
            direction = Vector3.down;
            fixAngle = 1f;
        }
        else if (transform.position.y < minY)
        {
            direction = Vector3.up;
            fixAngle = 1f;
        }

        if (transform.position.x > maxX || transform.position.x < minX)
        {
            speed = 1f;
            transform.position = Vector3.zero;
        }
    }
}
