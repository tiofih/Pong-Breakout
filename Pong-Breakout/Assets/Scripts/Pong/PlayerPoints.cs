using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPoints : MonoBehaviour
{
    [SerializeField] private string player;

    public static event Action<string> OnBallGoal = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            OnBallGoal(player);
            ball.ResetBall();
            StartCoroutine(ball.ResetBallWithTime(1f));
        }
    }
}
