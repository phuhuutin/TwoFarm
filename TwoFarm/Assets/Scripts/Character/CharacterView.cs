using System.Collections;
using System.Collections.Generic;
using MVC_EXAMPLE;
using UnityEngine;

public class CharacterView : MonoBehaviour
{
    public Animator characterAnimator;
    public Animator hairAnimator;
    public Animator toolAnimator;
    public Transform characterTransform { get; set; } // Reference to the character's transform for flipping

    public void Initialize()
    {
        characterAnimator = this.gameObject.GetComponent<Animator>();
        // Initialize all animators
        if (hairAnimator == null || toolAnimator == null)
        {
            foreach (Transform child in transform)
            {
                if (child.name == "CharactarHair")
                {
                    hairAnimator = child.GetComponent<Animator>();
                }
                else if (child.name == "CharactarTool")
                {
                    toolAnimator = child.GetComponent<Animator>();
                }
            }
        }
    }
    public void PlayAnimation(AnimationType animation)
    {
        characterAnimator.Play(animation + "");
        hairAnimator.Play(animation + "_Hair");
        toolAnimator.Play(animation + "_Tool");
    }
    public void FlipCharacter(bool facingRight)
    {
        Vector3 scale = characterTransform.localScale;
        scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        characterTransform.localScale = scale;
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
}