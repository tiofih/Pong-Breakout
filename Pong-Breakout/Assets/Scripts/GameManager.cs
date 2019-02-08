using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : PSingleton<GameManager>
{
    #region Variables
    protected GameManager() { }
    private float _player1Super = 0f;
    private float _player2Super = 0f;
    private float _maxPlayer1Super = 5f;
    private float _maxPlayer2Super = 5f;
    private int _player1Score = 0;
    private int _player2Score = 0;
    [SerializeField]
    private TMP_Text _player1Text;
    [SerializeField]
    private TMP_Text _player2Text;
    [SerializeField]
    private Slider _player1Slider;
    [SerializeField]
    private Slider _player2Slider;

    public int Player1Score { get { return _player1Score; } private set { Player1Score = _player1Score; } }
    public int Player2Score { get { return _player2Score; } private set { Player2Score = _player2Score; } }
    public float Player1Super
    {
        get { return _player1Score; }
        private set
        {
            if (_player1Super > 0) { Player1Super = _player1Super; }
            else { Player1Super = 0; }
        }
    }
    public float Player2Super
    {
        get { return _player1Score; }
        private set
        {
            if (_player2Super > 0) { Player2Super = _player2Super; }
            else { Player2Super = 0; }
        }
    }

    #endregion

    #region Delegates
    public delegate void OnPlayingGameDelegate(object s, EventArgs a);
    public event OnPlayingGameDelegate playingGameDelegate;

    public void OnPlaying()
    {
        if (playingGameDelegate != null)
            playingGameDelegate(this, EventArgs.Empty);
    }

    #endregion

    #region UnityFunctions

    private void Update()
    {
        OnPlaying();
    }

    #endregion

    #region Functions
    public void SumScore(string player)
    {
        if (player == "Player 1")
        {
            _player1Score++;
        }
        else if (player == "Player 2")
        {
            _player2Score++;
        }
        else
        {
            Debug.LogWarning("Invalid player name");
        }
        _player1Text.text = Player1Score.ToString();
        _player2Text.text = Player2Score.ToString();
    }

    public void SumSuper(string player, float amount)
    {
        if (player == "Player 1")
        {
            if (_player1Super < _maxPlayer1Super)
            {
                _player1Super += amount;
            }
            if (_player1Super > _maxPlayer1Super)
            {
                _player1Super = _maxPlayer1Super;
            }
        }
        else if (player == "Player 2")
        {
            if (_player2Super < _maxPlayer2Super)
            {
                _player2Super += amount;
            }

            if (_player2Super > _maxPlayer2Super)
            {
                _player2Super = _maxPlayer2Super;
            }
        }
        else
        {
            Debug.LogWarning("Invalid player name");
        }
        _player1Slider.value = _player1Super;
        _player2Slider.value = _player2Super;
    }
    #endregion


}
