using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialCollision : MonoBehaviour
{
    [SerializeField] private GameObject ballPrefab;
    [SerializeField] private Effects effect;

    private void Start()
    {
        var effectsArray = Enum.GetValues(typeof(Effects));
        effect = (Effects)UnityEngine.Random.Range(0, effectsArray.Length);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pad"))
        {
            if (effect == Effects.AddTile)
            {
                other.collider.GetComponent<PaddleOrganize>().AddTile();
            }

            if (effect == Effects.SubtractTile)
            {
                other.collider.GetComponent<PaddleOrganize>().DeleteTile();
            }

            if (effect == Effects.AddBall)
            {
                GameObject ball = Instantiate(ballPrefab, Vector3.zero, Quaternion.identity);
                ball.GetComponent<Ball>().ResetBall();
            }
        }
        Destroy(gameObject);
    }
}

public enum Effects
{
    AddTile,
    SubtractTile,
    AddBall
}
