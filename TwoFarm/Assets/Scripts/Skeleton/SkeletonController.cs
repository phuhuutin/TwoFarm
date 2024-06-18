using CustomColliders;
using MEC;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Abstracts;
public class SkeletonController : BaseEntityController, IDamageable
{
    // public SkeletonModel _model;
    // public SkeletonView _view;
    public SkeletonModel Model;
    public SkeletonView View;

    private Transform _playerTransform;
    private bool _facingRight = true;


    public void SetData(SkeletonModel model, SkeletonView view, Transform playerTransform)
    {
        _playerTransform = playerTransform;
        base.SetDataForBase(model, view);
        Initialize(model, view, playerTransform);
    }


    public void Initialize(IModel model, IView view, Transform playerTransform)
    {
        if (model == null)
        {
            Debug.LogError("Model is null in Initialize.");
        }
        if (view == null)
        {
            Debug.LogError("View is null in Initialize.");
        }
        if (playerTransform == null)
        {
            Debug.LogError("PlayerTransform is null in Initialize.");
        }

        if (model == null || view == null || playerTransform == null)
        {
            Debug.LogError("Initialize received a null parameter.");
            return;
        }

        Model = (SkeletonModel)model;
        View = (SkeletonView)view;
        _playerTransform = playerTransform;

        if (Model == null || View == null || _playerTransform == null)
        {
            Debug.LogError("Initialization failed: One of the critical components is still null.");
        }

        Model.HitPoints = Model.MaxHitPoints;
        if (View.healthBar != null)
        {
            View.healthBar.UpdateHealthBar(Model.HitPoints, Model.MaxHitPoints);
        }
        else
        {
            Debug.LogError("HealthBar is null in SkeletonView.");
        }

        Model.size = new Vector2(0.8157916f, 1.041088f);
        Model.BodyBox = new BoundingBox(View.SkeletonTransform.position, Model.size);
        // var skeletonModel = (SkeletonModel)_model;
        // var skeletonView = (SkeletonView)_view;

        // skeletonModel.HitPoints = skeletonModel.MaxHitPoints;
        // skeletonView.healthBar.UpdateHealthBar(skeletonModel.HitPoints, skeletonModel.MaxHitPoints);
        // skeletonModel.BodyBox = new BoundingBox(skeletonView.GetTransform().position, new Vector2(0.8157916f, 1.041088f));
    }


    // public override void SetData(IModel model, IView view, Transform playerTransform)
    // {
    //     this._model = (SkeletonModel)model;
    //     this._view = (SkeletonView)view;
    //     this._playerTransform = playerTransform;
    //     _model.HitPoints = _model.MaxHitPoints;
    //     _view.healthBar.UpdateHealthBar(_model.HitPoints, _model.MaxHitPoints);
    //     //  _view._healthBar.SetHealth(_model.HitPoints, _model.MaxHitPoints);
    //     _model.size = new Vector2(0.8157916f, 1.041088f);
    //     _model.BodyBox = new BoundingBox(_view.SkeletonTransform.position, _model.size);

    // }

    public void TakeHit(float damage)
    {
        Model.HitPoints -= damage;
        View.healthBar.UpdateHealthBar(Model.HitPoints, Model.MaxHitPoints);
        View.PlayAnimation(AnimationType.Hurt);
        //  _view._healthBar.SetHealth(_model.HitPoints, _model.MaxHitPoints);
        if (Model.HitPoints <= 0)
        {

            View.PlayDeathAnimationAndDestroy(0.75f);

            Model.showOnScreen = false;

            Debug.Log("Skeleton Died!!");


        }
    }

    public override void Handle()
    {
        Model.BodyBox.UpdatePosition(View.SkeletonTransform.position);
        Model.BodyBox.DrawDebug();





        float distanceToPlayer = Vector2.Distance(View.transform.position, _playerTransform.position);

        if (distanceToPlayer <= Model.DetectionRadius)
        {
            Vector2 directionToPlayer = (_playerTransform.position - View.transform.position).normalized;
            Model.MovementDirection = directionToPlayer;

            if (distanceToPlayer > 1.0f && Model.Status != SkeletonAnimationType.Attack) // If not close enough to attack, move towards player
            {
                MoveTowardsPlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            Model.MovementDirection = Vector2.zero;
            View.PlayAnimation(AnimationType.Idle);
        }

        HandleMovementAndAnimation();

        //this debug is enable/disable in prefab.
        // if(_view.isDebug){
        //     OnDrawGizmos();
        // }
    }

    private void MoveTowardsPlayer()
    {



        if (Model.Status != SkeletonAnimationType.Walk)
        {
            Model.Status = SkeletonAnimationType.Walk;
        }
        View.PlayAnimation(AnimationType.Walk);

        View.SetPosition(Model.MovementDirection * Model.MoveSpeed * Time.deltaTime);

        if ((Model.MovementDirection.x > 0 && !_facingRight) || (Model.MovementDirection.x < 0 && _facingRight))
        {
            _facingRight = !_facingRight;
            View.FlipTransform(_facingRight);
        }
    }

    private void AttackPlayer()
    {
        if (Model.Status != SkeletonAnimationType.Attack)
        {
            Model.Status = SkeletonAnimationType.Attack;
            View.PlayAnimation(AnimationType.Attack);
            Timing.RunCoroutine(AttackCooldown());
        }
    }

    private void HandleMovementAndAnimation()
    {
        // Additional movement and animation handling   can be done here if needed


    }

    private IEnumerator<float> AttackCooldown()
    {
        yield return Timing.WaitForSeconds(0.5f); // Adjust based on your attack animation length
        Model.Status = SkeletonAnimationType.Walk;
    }

    public void OnDebug()
    {
        //Character Detection Circle.
        if (View != null && Model != null)
        {
            Gizmos.color = Color.red;
            // Draw the detection radius as a wire sphere
            Gizmos.DrawWireSphere(View.transform.position, Model.DetectionRadius);
        }

    }
    public SkeletonModel GetModel()
    {
        return this.Model;
    }

    public void ResolveCollision(SkeletonController other, Vector2 penetrationDepth)
    {
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
        // transform.position += (Vector3)moveDirection / 2;
        View.SetPosition(Model.MoveSpeed * Time.deltaTime * (Vector3)moveDirection * 2);
        other.View.SetPosition(Model.MoveSpeed * Time.deltaTime * -(Vector3)moveDirection * 2);
        // Update bounding boxes
        // BodyBox.UpdatePosition(transform.position);
        // other.BodyBox.UpdatePosition(other.transform.position);
    }

    BoundingBox IDamageable.GetBodyBox()
    {
        return Model.BodyBox;
    }

    // public override void Handle()
    // {
    //     throw new System.NotImplementedException();
    // }
}