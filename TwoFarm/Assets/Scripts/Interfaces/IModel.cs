using CustomColliders;
using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IModel
    {
        float MoveSpeed { get; set; }
        BoundingBox BodyBox { get; set; }
        Vector2 MovementDirection { get; set; }
    }
}