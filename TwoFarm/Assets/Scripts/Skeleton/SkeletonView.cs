using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonView : MonoBehaviour
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

    public void PlayAnimation(SkeletonAnimationType animation)
    {
        SkeletonAnimator.Play(animation.ToString());
    }

    public void FlipSkeleton(bool facingRight)
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

    
    public void DestroyMeDaddy(){
        Debug.Log("remove from list of GameObject in UNity");
        Destroy(gameObject);
    }



    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other == null) return;
    //     Debug.Log("HIT From Skeleton");
    // }


}
