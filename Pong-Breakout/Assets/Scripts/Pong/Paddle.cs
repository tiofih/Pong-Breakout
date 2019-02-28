using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    private float speed = 2f;
    private Vector3 limits = new Vector3(18.5f, 0f, 13.5f);
    private Vector3 initialPosition;

    private void Awake()
    {
        initialPosition = transform.position;
        Timer.OnReset += ResetPaddle;
    }

    public void ResetPaddle()
    {
        transform.position = initialPosition;
    }

    public void MoveInZ(float amount)
    {
        transform.position += new Vector3(0, 0, amount) * speed * Time.deltaTime;
        var limitsWithScaleSize = limits.z - (transform.localScale.z / 2) + .5f;
        var clampedZ = Mathf.Clamp(transform.position.z, -limitsWithScaleSize, limitsWithScaleSize);
        transform.position = new Vector3(transform.position.x, transform.position.y, clampedZ);
    }

    public void MoveInX(float amount)
    {
        transform.position += new Vector3(amount, 0, 0) * speed * Time.deltaTime;
        var limitsWithScaleSize = limits.x - (transform.localScale.x / 2) + .5f;
        var clampedX = Mathf.Clamp(transform.position.x, -limitsWithScaleSize, limitsWithScaleSize);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
}
