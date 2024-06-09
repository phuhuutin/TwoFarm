using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonView : MonoBehaviour
{
    public Animator skeletonAnimator;
    public Transform skeletonTransform { get; set; }

    public void Initialize()
    {
        skeletonAnimator = this.gameObject.GetComponent<Animator>();
    }

    public void PlayAnimation(SkeletonAnimationType animation)
    {
        skeletonAnimator.Play(animation.ToString());
    }

    public void FlipSkeleton(bool facingRight)
    {
        Vector3 scale = skeletonTransform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        skeletonTransform.localScale = scale;
    }

    public void SetPosition(Vector2 modelMoveSpeed)
    {
        transform.Translate(modelMoveSpeed);
    }

    public void ActivateGameObject()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        Debug.Log("HIT From Skeleton");
    }
}
