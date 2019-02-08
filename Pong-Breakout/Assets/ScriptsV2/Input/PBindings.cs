using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PBindings : List<PKeyBinding>, PInputProvider
{
    public PBindings() { }

    public PBindings(bool isPlayerOne)
    {
        this.LoadDefaultBindings(isPlayerOne);
    }

    public void ClearBindings()
    {
        this.Clear();
    }


    private void LoadDefaultBindings(bool isPlayerOne)
    {
        this.Clear();
        if (isPlayerOne)
        {
            this.AddRange(new[] {
                new PKeyBinding (KeyCode.W, "Joy1_Up", PInputKeys.Up),
                new PKeyBinding (KeyCode.S, "Joy1_Down", PInputKeys.Down),
                new PKeyBinding (KeyCode.A, "Joy1_Left", PInputKeys.Left),
                new PKeyBinding (KeyCode.D, "Joy1_Right", PInputKeys.Right),
                new PKeyBinding (KeyCode.F, KeyCode.Joystick1Button0, PInputKeys.Action),
            });
        }
        else
        {
            this.AddRange(new[] {
                new PKeyBinding (KeyCode.UpArrow, "Joy2_Up", PInputKeys.Up),
                new PKeyBinding (KeyCode.DownArrow, "Joy2_Down", PInputKeys.Down),
                new PKeyBinding (KeyCode.LeftArrow, "Joy2_Left", PInputKeys.Left),
                new PKeyBinding (KeyCode.RightArrow, "Joy2_Right", PInputKeys.Right),
                new PKeyBinding (KeyCode.RightControl, KeyCode.Joystick2Button0, PInputKeys.Action),
            });
        }
    }

    private PInputKeys CheckKeys()
    {
        PInputKeys pressedKeys = PInputKeys.None;
        foreach (PKeyBinding binding in this)
        {
            if (Input.GetKey(binding.PrimaryKey))
            {
                pressedKeys = pressedKeys.SetFlag(binding.TriggeredKeys);
            }
            else
            {
                if (!binding.IsAxis)
                {
                    if (Input.GetKey(binding.AlternativeKey))
                    {
                        pressedKeys = pressedKeys.SetFlag(binding.TriggeredKeys);
                    }
                }
                else
                {
                    if (Input.GetAxis(binding.AlternativeAxis) > 0.5f)
                    {
                        pressedKeys = pressedKeys.SetFlag(binding.TriggeredKeys);
                    }
                }

            }
        }
        return pressedKeys;
    }

    public PInputKeys ProvideKeys()
    {
        return this.CheckKeys();
    }


}
