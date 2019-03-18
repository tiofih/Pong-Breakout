using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutPlayerPoints : MonoBehaviour
{
    public static event Action OnBallLose = delegate { };

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Ball ball = other.GetComponent<Ball>();
            if (GameObject.FindGameObjectsWithTag("Ball").Length > 1)
            {
                Destroy(other.gameObject);
            }
            else
            {
                ball.ResetBall();
                OnBallLose();
                StartCoroutine(ball.ResetBallWithTime(1f));
            }
        }
    }
}
