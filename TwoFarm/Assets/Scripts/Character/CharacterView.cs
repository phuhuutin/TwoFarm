
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    public Animator CharacterAnimator;
    public Animator HairAnimator;
    public Animator ToolAnimator;
    public Transform CharacterTransform { get; set; } // Reference to the character's transform for flipping

    private CharacterController _controller;
    private Rigidbody2D rb2d;

    public void Initialize( CharacterController  controller)
    {   
        rb2d = GetComponent<Rigidbody2D>();

        _controller = controller;

        if (CharacterAnimator == null)
            CharacterAnimator = this.gameObject.GetComponent<Animator>();
        // Initialize all animators
        if (HairAnimator == null || ToolAnimator == null)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "CharacterHair")
                {
                    HairAnimator = child.GetComponent<Animator>();
                }
                else if (child.name == "CharacterTool")
                {
                    ToolAnimator = child.GetComponent<Animator>();
                }
            }
        }
    }
    public void PlayAnimation(AnimationType animation)
    {
        CharacterAnimator.Play(animation + "");
        HairAnimator.Play(animation + "_Hair");
        ToolAnimator.Play(animation + "_Tool");
    }
    public void FlipCharacter(bool facingRight)
    {
        Vector3 scale = CharacterTransform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        CharacterTransform.localScale = scale;
    }
    public void SetPosition(Vector2 modelMoveSpeed)
    {
        transform.Translate(modelMoveSpeed);
    }
    public void ActivateGameObject()
    {
        // Activate the GameObject
        gameObject.SetActive(true);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other == null) return;
        if (other.CompareTag("Skeleton") && _controller.getModel().status == AnimationType.Fight) 
        {
            var skeletonView = other.GetComponent<SkeletonView>();
            if (skeletonView != null)
            {
                _controller.OnSkeletonCollision(skeletonView);
            }
        }

    
    }


    public Rigidbody2D GetRigidbody2D()
    {
        return rb2d;
    }
}
