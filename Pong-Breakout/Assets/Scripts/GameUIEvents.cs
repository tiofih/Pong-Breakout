using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUIEvents : MonoBehaviour
{

    [SerializeField] private TMP_Text p1;
    [SerializeField] private TMP_Text p2;

    private int p1Points;
    private int p2Points;

    private void Start()
    {
        Ball.OnBallGoal += UpdateHud;
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
}
