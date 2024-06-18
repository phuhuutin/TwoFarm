using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class SkeletonView : MonoBehaviour, IView
{
    public Animator SkeletonAnimator;
    public Transform SkeletonTransform { get; set; }

    //   public HealthBarBehavior _healthBar; 

    private SkeletonController _controller;

    [SerializeField]
    public FloatingHealthBar healthBar;


    public Boolean isDebug;

    public void Initialize(SkeletonController controller)
    {
        SkeletonAnimator = this.gameObject.GetComponent<Animator>();
        _controller = controller;
        healthBar = GetComponentInChildren<FloatingHealthBar>();

    }

    public SkeletonController GetController()
    {
        return _controller;
    }

    public void PlayAnimation(AnimationType animation)
    {
        SkeletonAnimator.Play(animation.ToString());
    }

    public void FlipTransform(bool facingRight)
    {
        Vector3 scale = SkeletonTransform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        SkeletonTransform.localScale = scale;
    }

    public void SetPosition(Vector2 modelMoveSpeed)
    {
        transform.Translate(modelMoveSpeed);
    }

    public void ActivateGameObject()
    {
        gameObject.SetActive(true);
    }


    public void DestroyMeDaddy()
    {
        Debug.Log("remove from list of GameObject in UNity");
        Destroy(gameObject);
    }

    public void PlayDeathAnimationAndDestroy(float delay)
    {
        StartCoroutine(PlayDeathAnimation(delay));
    }

    private IEnumerator PlayDeathAnimation(float delay)
    {
        // Assuming the death animation is triggered by setting a parameter in the Animator
        PlayAnimation(AnimationType.Death);

        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }


    public Transform GetTransform()
    {
        return SkeletonTransform;
    }

    void IView.Initialize()
    {
        throw new NotImplementedException();
    }


}
