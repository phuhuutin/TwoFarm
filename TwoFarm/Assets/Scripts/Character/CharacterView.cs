using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    public Animator CharacterAnimator;
    public Animator HairAnimator;
    public Animator ToolAnimator;
    public Transform CharacterTransform { get; set; } // Reference to the character's transform for flipping

    public void Initialize()
    {
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other == null) return;
        Debug.Log("HIT");
    }
}
