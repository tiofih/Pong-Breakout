using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class BreakoutUIEvents : MonoBehaviour
{
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text ballsText;

    [SerializeField] private Animator pauseText;
    [SerializeField] private Animator pausePanel;
    [SerializeField] private Animator pauseButton;
    [SerializeField] private Animator transitionPanel;

    [SerializeField] private AudioSource clickAudioSource;

    [SerializeField] private Timer timer;

    private int points;
    private int balls = 3;

    private void Start()
    {
        Timer.OnReset += ResetUI;
        Timer.OnTimerEnd += PauseGame;
        Timer.OnTimerEnd += EndGame;
        BreakoutPlayerPoints.OnBallLose += SubtractBall;
        Tile.OnTileDestroyed += AddPoints;
        UpdateHud();
    }

    private void UpdateHud()
    {
        pointsText.text = points.ToString();
        ballsText.text = balls.ToString();
    }

    private void AddPoints()
    {
        points++;
        UpdateHud();
    }

    private void AddBall()
    {
        balls++;
        UpdateHud();
    }

    private void SubtractBall()
    {
        if (balls > 0)
        {
            balls--;
            UpdateHud();
        }
        else
        {
            EndGame();
            PauseGame();
        }
    }

    private void ResetUI()
    {
        points = 0;
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

    private void OnDestroy()
    {
        Timer.OnReset -= ResetUI;
        Timer.OnTimerEnd -= PauseGame;
        Timer.OnTimerEnd -= EndGame;
    }
}
