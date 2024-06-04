using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public float MoveSpeed { get; set; } = 5.0f;
    public float RollSpeed { get; set; } = 10.0f; 

    public bool IsRunning { get; set; } = false;
    public Vector2 MovementDirection { get; set; } = Vector2.zero;

    public bool IsRolling { get; set; } = false;

    public void SetSpeed()
    {
        MoveSpeed = IsRunning ? 5.0f : 3.0f; 
    }

}
