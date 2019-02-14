using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float maxSpeed = 5f;
    private float fixAngle = 1f;
    private Vector3 velocity = new Vector3(1, 0, 0);
    private Vector3 limits = new Vector3(18.5f, 0f, 13.5f);
    public static event Action<string> OnBallGoal = delegate { };

    private void Start()
    {
        //velocity.z = Random.Range(-1f, 1f);
    }

    private void Move()
    {
        velocity = (velocity * fixAngle + velocity).normalized;
        transform.position += velocity * speed * Time.deltaTime;
    }

    private void AddSpeed(float amount)
    {
        speed = speed + amount >= maxSpeed ? speed = maxSpeed : speed += amount;
    }

    private void Update()
    {
        Move();
        ClampHit();
    }

    private void Reflect()
    {
        velocity.x *= -1f;
    }

    private void ClampHit()
    {
        if (transform.position.z > limits.z || transform.position.z < -limits.z)
        {
            velocity.z *= -1f;
            fixAngle = UnityEngine.Random.Range(-0.1f, 0.1f);
        }

        if (transform.position.x > limits.x)
        {
            Reflect();
            OnBallGoal("Player 2");
        }
        else if (transform.position.x < -limits.x)
        {
            Reflect();
            OnBallGoal("Player 1");
        }
    }

    private void ReflectOnPad(Transform pad)
    {
        velocity.z = transform.position.z - pad.position.z < 2f ? transform.position.z - pad.position.z : 0f;
        velocity.z = velocity.z < -2f ? 0f : velocity.z;
        Reflect();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pad"))
        {
            ReflectOnPad(other.transform);
            AddSpeed(0.3f);
        }
    }

}
