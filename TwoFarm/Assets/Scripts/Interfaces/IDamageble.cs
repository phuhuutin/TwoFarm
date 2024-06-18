using CustomColliders;
namespace Assets.Scripts.Interfaces
{
    public interface IDamageable
    {
        void TakeHit(float damage);
        BoundingBox GetBodyBox();
    }
}