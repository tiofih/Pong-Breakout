using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    private float maxSpeed = 4f;
    private float fixAngle = 1f;
    private Vector3 direction;
    private Vector3 limits = new Vector3(18.5f, 0f, 13.5f);
    private ParticleSystem particles;
    private AudioSource audioSource;
    public static event Action<string> OnBallGoal = delegate { };

    public void ResetBall()
    {
        transform.position = Vector3.zero;
        speed = 1f;
        direction = new Vector3(UnityEngine.Random.Range(-1f, 1f), 0, UnityEngine.Random.Range(-1f, 1f));
        //direction = new Vector3(1, 0, 0);
    }

    private void Awake()
    {
        Timer.OnReset += ResetBall;
        Timer.OnTimerOn += Move;
        particles = GetComponent<ParticleSystem>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Move(bool timer)
    {
        if (timer)
        {
            var velocity = (direction * fixAngle + direction).normalized;
            transform.position += velocity * speed * Time.deltaTime;
        }
    }

    private void AddSpeed(float amount)
    {
        speed = speed + amount >= maxSpeed ? speed = maxSpeed : speed += amount;
    }

    private void Update()
    {
        ClampHit();
    }

    private void Reflect()
    {
        direction.x *= -1f;
    }

    private void ClampHit()
    {
        if (transform.position.z > limits.z || transform.position.z < -limits.z)
        {
            direction.z *= -1f;
            fixAngle = UnityEngine.Random.Range(-0.1f, 0.1f);
        }

        if (transform.position.x > limits.x)
        {
            OnBallGoal("Player 2");
            // ResetBall();
            StartCoroutine(ResetBallWithTime(1f));
        }
        else if (transform.position.x < -limits.x)
        {
            OnBallGoal("Player 1");
            // ResetBall();
            StartCoroutine(ResetBallWithTime(1f));
        }
    }

    private IEnumerator ResetBallWithTime(float timeAmount)
    {
        ResetBall();
        Timer.OnTimerOn -= Move;
        yield return new WaitForSecondsRealtime(timeAmount);
        Timer.OnTimerOn += Move;
    }

    private void ReflectOnPad(Transform pad)
    {
        direction.z = transform.position.z - pad.position.z < 2f ? transform.position.z - pad.position.z : 0f;
        direction.z = direction.z < -2f ? 0f : direction.z;
        Reflect();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.CompareTag("Pad"))
        {
            ReflectOnPad(other.transform);
            AddSpeed(0.3f);
            audioSource.Play();
            if (speed >= maxSpeed / 2)
            {
                CameraShake.Shake(1f, 1f);
                particles.Play();
            }
        }
    }

}
