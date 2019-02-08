using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PInputModel", menuName = "Pong-Breakout/PInputModel", order = 0)]
public class PInputModel : ScriptableObject
{
    public KeyCode _upKey;
    public KeyCode _downKey;
    public KeyCode _rightKey;
    public KeyCode _leftKey;
    public KeyCode _confirmKey;

}
