using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Paddle))]
public class PlayerInput : MonoBehaviour
{
    public InputModel input;

    private Paddle pad;

    private void Awake()
    {
        pad = GetComponent<Paddle>();
        Timer.OnTimerOn += GetInputs;
    }

    private void GetInputs(bool timer)
    {
        if (timer)
        {
            if (Input.GetKey(input.upKey))
            {
                pad.MoveInZ(1f);
            }
            if (Input.GetKey(input.downKey))
            {
                pad.MoveInZ(-1f);
            }
        }
    }

    private void OnDestroy()
    {
        Timer.OnTimerOn -= GetInputs;
    }
}
