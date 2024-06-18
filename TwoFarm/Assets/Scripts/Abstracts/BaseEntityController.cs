using Assets.Scripts.Interfaces;
using CustomColliders;
using UnityEngine;

namespace Assets.Scripts.Abstracts
{
    //Resolve Collision to prevent overlay sprites between Entities.
    public abstract class BaseEntityController
    {
        protected IModel _model;
        protected IView _view;

        public void SetDataForBase(IModel model, IView view)
        {
            this._model = model;
            this._view = view;
        }

        //     public abstract void Initialize(IModel model, IView view, Transform playerTransform);

        public void ResolveCollision(BaseEntityController other)
        {

            if (_model == null)
            {
                Debug.LogError("NULLLLLLLLLLLLLL");
            }

            if (_model.BodyBox.Intersects(other._model.BodyBox))
            {
                Vector2 penetrationDepth = GetPenetrationDepth(_model.BodyBox);
                // Determine the direction to move the objects apart
                Vector2 moveDirection;
                if (penetrationDepth.x < penetrationDepth.y)
                {
                    moveDirection = new Vector2(penetrationDepth.x, 0);
                }
                else
                {
                    moveDirection = new Vector2(0, penetrationDepth.y);
                }

                // Move the objects apart
                _view.SetPosition(_model.MoveSpeed * Time.deltaTime * (Vector3)moveDirection * 2);
                other._view.SetPosition(_model.MoveSpeed * Time.deltaTime * -(Vector3)moveDirection * 2);
            }


        }

        public Vector2 GetPenetrationDepth(BoundingBox other)
        {
            float overlapX = Mathf.Min(_model.BodyBox.position.x + _model.BodyBox.size.x, other.position.x + other.size.x) - Mathf.Max(_model.BodyBox.position.x, other.position.x);
            float overlapY = Mathf.Min(_model.BodyBox.position.y + _model.BodyBox.size.y, other.position.y + other.size.y) - Mathf.Max(_model.BodyBox.position.y, other.position.y);

            return new Vector2(overlapX, overlapY);
        }

        public abstract void Handle();
    }
}
