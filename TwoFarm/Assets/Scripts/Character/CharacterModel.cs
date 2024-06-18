using System;
using UnityEngine;
using CustomColliders;
using Assets.Scripts.Interfaces;
public class CharacterModel : IModel
{
    public float MoveSpeed { get; set; } = 5.0f;
    public float RollSpeed { get; set; } = 10.0f;

    public AnimationType Status { get; set; } = AnimationType.Idle;

    public AnimationType CurrentTool { get; set; } = AnimationType.Attack;



    public bool IsRunning { get; set; } = false;
    public Vector2 MovementDirection { get; set; } = Vector2.zero;

    public bool IsRolling { get; set; } = false;

    public BoundingBox BodyBox { get; set; }


    public BoundingBox AttackBox;

    public Vector2 size;


    public float lastHitTime = -1f;

    public Boolean notInFightStatus()
    {
        return this.Status != AnimationType.Attack;
    }

    public Boolean notInRolltStatus()
    {
        return this.Status != AnimationType.Roll;
    }

    public Boolean notInRunStatus()
    {
        return this.Status != AnimationType.Run;
    }

    public void SetSpeed()
    {
        MoveSpeed = Status == AnimationType.Run ? 7.0f : 5.0f;
    }



}
