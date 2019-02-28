using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGoalEffects : MonoBehaviour
{

    [SerializeField] private AudioSource[] audioSource;
    [SerializeField] private ParticleSystem[] particles;

    private void Awake()
    {
        Ball.OnBallGoal += PlayAllParticles;
        Ball.OnBallGoal += ChangePitchAndPlayAudio;
        Ball.OnBallGoal += ShakeCameraWheGoal;
    }

    private void PlayAllParticles(string something = null)
    {
        for (int i = 0; i < particles.Length; i++)
        {
            particles[i].Play();
        }
    }

    private void ChangePitchAndPlayAudio(string something = null)
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


}
