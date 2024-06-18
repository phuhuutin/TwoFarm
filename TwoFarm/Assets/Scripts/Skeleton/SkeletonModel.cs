using System;
using CustomColliders;
using UnityEngine;
using Assets.Scripts.Interfaces;
public class SkeletonModel : IModel
{
    public float MoveSpeed { get; set; } = 3.0f;
    public float JumpSpeed { get; set; } = 5.0f;

    public float HitPoints;

    public Boolean showOnScreen = true;

    public float MaxHitPoints = 10;

    public BoundingBox BodyBox { get; set; }

    public Vector2 size;


    public SkeletonAnimationType Status { get; set; } = SkeletonAnimationType.Idle;

    public Vector2 MovementDirection { get; set; } = Vector2.zero;

    public float DetectionRadius { get; set; } = 5.0f; // Radius within which the skeleton detects the player
}

public enum SkeletonAnimationType
{
    Idle,
    Walk,
    Jump,
    Attack,
    Hurt,
    Death
}