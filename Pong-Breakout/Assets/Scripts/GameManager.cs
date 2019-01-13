using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : Singleton<GameManager>
{
    #region Variables
    protected GameManager() { }
    public float padSpeed = 1f;
    public float maxX = 3.2f;
    public float minX = -3.2f;
    public float maxY = 2f;
    public float minY = -2f;
    public lastPlayer lp;

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
}

public enum lastPlayer { p1, p2 }
