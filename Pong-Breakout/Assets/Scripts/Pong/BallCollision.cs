using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Ball))]
public class BallCollision : MonoBehaviour
{
    private Ball ball;
    private Vector3 limits = new Vector3(18.5f, 0f, 13.5f);
    private ParticleSystem particles;
    private AudioSource audioSource;


    private void Awake()
    {
        ball = GetComponent<Ball>();
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
        Timer.OnTimerOn += ClampBall;
    }

    private void ClampBall(bool timer)
    {
        if (timer)
        {
            if (transform.position.z > limits.z || transform.position.z < -limits.z)
            {
                ball.direction.z *= -1f;
                ball.fixAngle = UnityEngine.Random.Range(-1f, 1f);
            }

            if (transform.position.x > limits.x)
            {
                Reflect();
            }
            else if (transform.position.x < -limits.x)
            {
                Reflect();
            }
        }
    }

    private void Reflect()
    {
        ball.direction.x *= -1f;
    }

    private void Reflect(Transform obj, float width)
    {
        ball.direction.z = ((transform.position.z - obj.position.z) / width) * 50f;
        ball.direction.x *= -1f;
    }

    private void OnCollisionEnter(Collision other)
    {
        audioSource.Play();
        if (ball.speed >= ball.maxSpeed / 2)
        {
            CameraShake.Shake(.1f, .1f);
            particles.Play();
        }

        if (other.collider.CompareTag("Pad"))
        {
            Reflect(other.transform, other.collider.bounds.size.z);
            ball.AddSpeed(3f);
        }

        if (other.collider.CompareTag("Tile"))
        {
            Reflect(other.transform, other.collider.bounds.size.z);
        }

        if (other.collider.CompareTag("Ball"))
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider);
        }
    }

    private void OnDestroy()
    {
        Timer.OnTimerOn -= ClampBall;
    }
}
