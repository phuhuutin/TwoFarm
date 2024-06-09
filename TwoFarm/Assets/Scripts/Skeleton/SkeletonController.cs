using MEC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController
{
    private SkeletonModel model;
    private SkeletonView view;
    private Transform playerTransform;
    private bool facingRight = true;

    public void SetData(SkeletonModel model, SkeletonView view, Transform playerTransform)
    {
        this.model = model;
        this.view = view;
        this.playerTransform = playerTransform;
    }

    public void Handle()
    {
        float distanceToPlayer = Vector2.Distance(view.transform.position, playerTransform.position);

        if (distanceToPlayer <= model.DetectionRadius)
        {
            Vector2 directionToPlayer = (playerTransform.position - view.transform.position).normalized;
            model.MovementDirection = directionToPlayer;

            if (distanceToPlayer > 1.0f && model.Status != SkeletonAnimationType.Attack) // If not close enough to attack, move towards player
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
            model.MovementDirection = Vector2.zero;
            view.PlayAnimation(SkeletonAnimationType.Idle);
        }

        HandleMovementAndAnimation();
    }

    private void MoveTowardsPlayer()
    {



        if (model.Status != SkeletonAnimationType.Walk)
        {
            model.Status = SkeletonAnimationType.Walk;
        }
        view.PlayAnimation(SkeletonAnimationType.Walk);

        view.SetPosition(model.MovementDirection * model.MoveSpeed * Time.deltaTime);

        if ((model.MovementDirection.x > 0 && !facingRight) || (model.MovementDirection.x < 0 && facingRight))
        {
            facingRight = !facingRight;
            view.FlipSkeleton(facingRight);
        }
    }

    private void AttackPlayer()
    {
        if (model.Status != SkeletonAnimationType.Attack)
        {
            model.Status = SkeletonAnimationType.Attack;
            view.PlayAnimation(SkeletonAnimationType.Attack);
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
        model.Status = SkeletonAnimationType.Walk;
    }
}