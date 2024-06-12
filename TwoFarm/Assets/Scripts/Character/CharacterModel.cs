using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModel
{
    public float MoveSpeed { get; set; } = 5.0f;
    public float RollSpeed { get; set; } = 10.0f;

    public AnimationType status { get; set; } = AnimationType.Idle;


    public bool IsRunning { get; set; } = false;
    public Vector2 MovementDirection { get; set; } = Vector2.zero;

    public bool IsRolling { get; set; } = false;

    public void SetSpeed()
    {
        MoveSpeed = status == AnimationType.Run ? 7.0f : 5.0f; 
    }

    

    public float lastHitTime = -1f;

    public Boolean notInFightStatus(){
        return this.status != AnimationType.Fight;
    }

    public Boolean notInRolltStatus(){
        return this.status != AnimationType.Roll;
    }

    public Boolean notInRunStatus(){
        return this.status != AnimationType.Run;
    }


}
