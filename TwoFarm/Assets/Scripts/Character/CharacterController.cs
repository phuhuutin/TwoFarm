using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class CharacterController
{
    private CharacterModel model;
    private CharacterView view;
    private bool facingRight = true;

    public void SetData(CharacterModel model, CharacterView view)
    {
        // Initialize Data Model, View
        this.model = model;
        this.view = view;
    }
    
    public void Handle()
    {
        // Handle running input
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            model.status = model.status == AnimationType.Run ? AnimationType.Walk : AnimationType.Run;
            model.SetSpeed();
        }


        // Handle movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // ASDW and arrow keys
        float moveY = Input.GetAxisRaw("Vertical");   // ASDW and arrow keys

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        model.MovementDirection = moveDirection;
        
        // Handle character facing direction
        if (moveX > 0 && !facingRight)
        {
            facingRight = true;
            view.FlipCharacter(facingRight);
        }
        else if (moveX < 0 && facingRight)
        {
            facingRight = false;
            view.FlipCharacter(facingRight);
        }

        // Handle roll skill
        if (Input.GetKeyDown(KeyCode.Space) && model.status != AnimationType.Roll && model.status != AnimationType.Fight)
        {
            var tempStatus = model.status;
            model.status = AnimationType.Roll;
            view.PlayAnimation(AnimationType.Roll);
            Timing.RunCoroutine(RollCooldown(tempStatus == AnimationType.Run));
        }

        // Handle fight animation
        if (Input.GetMouseButtonDown(0) && model.status != AnimationType.Fight && model.status != AnimationType.Roll)
        {
            var tempStatus = model.status;

            model.status = AnimationType.Fight;
            view.PlayAnimation(AnimationType.Fight);
            Timing.RunCoroutine(FightCooldown(tempStatus == AnimationType.Run));
        }

        // Handle character movement and animations based on status
        switch (model.status)
        {
            case AnimationType.Roll:
                // Move the character with roll speed
                view.SetPosition(moveDirection * model.RollSpeed * Time.deltaTime);
                break;

            case AnimationType.Fight:
                // No movement during fight animation
                break;

            default:
                // Move the character normally
                view.SetPosition(moveDirection * model.MoveSpeed * Time.deltaTime);
                if (moveDirection != Vector2.zero)
                {
                    view.PlayAnimation(model.status == AnimationType.Run ? AnimationType.Run : AnimationType.Walk);
                }
                else
                {
                    view.PlayAnimation(AnimationType.Idle);
                }
                break;
        }
    }

  

    private IEnumerator<float> RollCooldown(bool isRunning)
    {
        // Duration of the roll animation
        yield return  Timing.WaitForSeconds(0.5f); // Adjust based on your animation length
        
        model.status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }

    private IEnumerator<float> FightCooldown(bool isRunning)
    {
        // Duration of the fight animation
        yield return Timing.WaitForSeconds(0.5f); // Adjust based on your animation length
        model.status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }

}
