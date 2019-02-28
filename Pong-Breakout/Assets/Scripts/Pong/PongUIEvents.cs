using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PongUIEvents : MonoBehaviour
{
    [SerializeField] private TMP_Text p1;
    [SerializeField] private TMP_Text p2;
    [SerializeField] private Animator pauseText;
    [SerializeField] private Animator pausePanel;
    [SerializeField] private Animator pauseButton;
    [SerializeField] private Animator transitionPanel;
    [SerializeField] private AudioSource crowdAudioSource;
    [SerializeField] private AudioSource clickAudioSource;

    [SerializeField] private Timer timer;

    private int p1Points;
    private int p2Points;

    private void Start()
    {
        Ball.OnBallGoal += UpdateHud;
        Timer.OnReset += ResetUI;
        Timer.OnTimerEnd += PauseGame;
        Timer.OnTimerEnd += EndGame;
    }

    private void UpdateHud(string player)
    {
        if (player == "Player 1")
        {
            p1Points++;
            p1.SetText(p1Points.ToString());
        }

        if (player == "Player 2")
        {
            p2Points++;
            p2.SetText(p2Points.ToString());
        }
    }

    private void ResetUI()
    {
        p1Points = 0;
        p1.SetText(p1Points.ToString());
        p2Points = 0;
        p2.SetText(p2Points.ToString());
        var newText = pauseText.GetComponent<TMP_Text>();
        newText.text = "Paused";
    }

    private void EndGame()
    {
        var newText = pauseText.GetComponent<TMP_Text>();
        newText.text = "Game Over";
    }

    private void UIOff()
    {
        pauseText.SetBool("IsInScene", false);
        pausePanel.SetBool("IsInScene", false);
        pauseButton.SetBool("IsInScene", true);
        crowdAudioSource.Play();
        clickAudioSource.Play();
    }

    public void PauseGame()
    {
        timer.StopTimer();
        if (!pauseText.enabled || !pausePanel.enabled)
        {
            pauseText.enabled = true;
            pausePanel.enabled = true;
            pauseButton.enabled = true;
        }
        pauseText.SetBool("IsInScene", true);
        pausePanel.SetBool("IsInScene", true);
        pauseButton.SetBool("IsInScene", false);
        crowdAudioSource.Pause();
        clickAudioSource.Play();
    }

    public void RestartGame()
    {
        timer.ResetTimer();
        UIOff();
    }

    public void QuitGame()
    {
        transitionPanel.SetBool("IsInScene", true);
        SceneManager.LoadScene("StartScene");
        clickAudioSource.Play();
    }

    public void ResumeGame()
    {
        timer.StartTimer();
        UIOff();
    }
}
