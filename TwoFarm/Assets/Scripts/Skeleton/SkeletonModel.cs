using UnityEngine;

public class SkeletonModel
{
    public float MoveSpeed { get; set; } = 3.0f;
    public float JumpSpeed { get; set; } = 5.0f;

    public float HitPoints;

    public float MaxHitPoints = 10;

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