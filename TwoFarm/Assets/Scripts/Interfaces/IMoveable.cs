using CustomColliders;
namespace Assets.Scripts.Interfaces
{
    public interface IMoveable
    {
        void TakeHit(float damage);
        BoundingBox GetBodyBox();
    }
}