using UnityEngine;

namespace Assets.Scripts.Interfaces
{
    public interface IView
    {
        void Initialize();
        void PlayAnimation(AnimationType animation);
        void FlipTransform(bool facingRight);
        void SetPosition(Vector2 position);
        void ActivateGameObject();
        Transform GetTransform();
    }
}