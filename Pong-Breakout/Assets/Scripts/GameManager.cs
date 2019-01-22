using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    protected GameManager() { }
    private float _padSpeed = 5f;
    private float _maxX = 3.2f;
    private float _minX = -3.2f;
    private float _maxY = 2f;
    private float _minY = -2f;
    private int _player1Score = 0;
    private int _player2Score = 0;
    [SerializeField]
    private TMP_Text _player1Text;
    [SerializeField]
    private TMP_Text _player2Text;

    public float PadSpeed { get { return _padSpeed; } private set { PadSpeed = _padSpeed; } }
    public float MaxX { get { return _maxX; } private set { MaxX = _maxX; } }
    public float MinX { get { return _minX; } private set { MinX = _minX; } }
    public float MaxY { get { return _maxY; } private set { MaxY = _maxY; } }
    public float MinY { get { return _minY; } private set { MinY = _minY; } }
    public int Player1Score { get { return _player1Score; } private set { Player1Score = _player1Score; } }
    public int Player2Score { get { return _player2Score; } private set { Player2Score = _player2Score; } }

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
    #endregion

    #region UnityFunctions

    private void Update()
    {
        OnPlaying();
    }

    #endregion
}

public enum lastPlayer { p1, p2 }
