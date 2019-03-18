using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGoalEffects : MonoBehaviour
{

    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private ParticleSystem[] particles;

    private void Awake()
    {
        PlayerPoints.OnBallGoal += PlayAllParticles;
        PlayerPoints.OnBallGoal += PlayAudio;
        PlayerPoints.OnBallGoal += ShakeCameraWheGoal;
    }

    private void PlayAllParticles(string something = null)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }

    private void PlayAudio(string something = null)
    {
        for (int i = 0; i < audioSource.Length; i++)
        {
            audioSource[i].Play();
        }
    }

    private void ShakeCameraWheGoal(string something = null)
    {
        CameraShake.Shake(1f, 2f);
    }

    private void OnDestroy()
    {
        PlayerPoints.OnBallGoal -= PlayAllParticles;
        PlayerPoints.OnBallGoal -= PlayAudio;
        PlayerPoints.OnBallGoal -= ShakeCameraWheGoal;
    }


}
