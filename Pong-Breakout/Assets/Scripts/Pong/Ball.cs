using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed = 10f;
    public float maxSpeed = 30f;
    public float fixAngle = 1f;
    public Vector3 direction;
    private Rigidbody rb;

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        speed = 20f;
        //int randSide = UnityEngine.Random.Range(1, 10);
        //float x = randSide > 5 ? 1 : -1;
        //direction = new Vector3(x, 0, UnityEngine.Random.Range(-0.5f, 0.5f));
        direction = new Vector3(speed, 0, 0);
    }

    private void Awake()
    {
        Timer.OnReset += ResetBall;
        Timer.OnTimerOn += Move;
    }

    private void Move(bool timer)
    {
        if (timer)
        {
            var velocity = (direction * fixAngle + direction).normalized;
            transform.position += velocity * speed * Time.deltaTime;
        }
    }

    public void AddSpeed(float amount)
    {
        speed = speed + amount >= maxSpeed ? speed = maxSpeed : speed += amount;
    }

    public IEnumerator ResetBallWithTime(float timeAmount)
    {
        ResetBall();
        Timer.OnTimerOn -= Move;
        yield return new WaitForSecondsRealtime(timeAmount);
        Timer.OnTimerOn += Move;
    }

    private void OnDestroy()
    {
        Timer.OnReset -= ResetBall;
        Timer.OnTimerOn -= Move;
    }
}
