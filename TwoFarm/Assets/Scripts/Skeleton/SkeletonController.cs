using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController
{
    private SkeletonModel _model;
    private SkeletonView _view;
    private Transform _playerTransform;
    private bool _facingRight = true;

    public void SetData(SkeletonModel model, SkeletonView view, Transform playerTransform)
    {
        this._model = model;
        this._view = view;
        this._playerTransform = playerTransform;
    }

    public void Handle()
    {
        float distanceToPlayer = Vector2.Distance(_view.transform.position, _playerTransform.position);

        if (distanceToPlayer <= _model.DetectionRadius)
        {
            Vector2 directionToPlayer = (_playerTransform.position - _view.transform.position).normalized;
            _model.MovementDirection = directionToPlayer;

            if (distanceToPlayer > 1.0f && _model.Status != SkeletonAnimationType.Attack) // If not close enough to attack, move towards player
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
            _model.MovementDirection = Vector2.zero;
            _view.PlayAnimation(SkeletonAnimationType.Idle);
        }

        HandleMovementAndAnimation();
    }

    private void MoveTowardsPlayer()
    {



        if (_model.Status != SkeletonAnimationType.Walk)
        {
            _model.Status = SkeletonAnimationType.Walk;
        }
        _view.PlayAnimation(SkeletonAnimationType.Walk);

        _view.SetPosition(_model.MovementDirection * _model.MoveSpeed * Time.deltaTime);

        if ((_model.MovementDirection.x > 0 && !_facingRight) || (_model.MovementDirection.x < 0 && _facingRight))
        {
            _facingRight = !_facingRight;
            _view.FlipSkeleton(_facingRight);
        }
    }

    private void AttackPlayer()
    {
        if (_model.Status != SkeletonAnimationType.Attack)
        {
            _model.Status = SkeletonAnimationType.Attack;
            _view.PlayAnimation(SkeletonAnimationType.Attack);
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
        _model.Status = SkeletonAnimationType.Walk;
    }
}