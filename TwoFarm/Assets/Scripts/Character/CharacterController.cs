using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using CustomColliders;
using Assets.Scripts.Character;
using Assets.Scripts.Interfaces;
using Assets.Scripts.Abstracts;
public class CharacterController : BaseEntityController
{
    private CharacterModel Model;
    // private CharacterView view;

    private CharacterView View;
    private bool facingRight = true;

    private bool isDelaying = false;


    public void SetData(CharacterModel model, CharacterView view)
    {
        base.SetDataForBase(model, view);
        // Initialize Data Model, View
        this.Model = model;
        this.View = view;
        //  _model.size = new Vector2(0.8136715f,0.9791778f);
        this.Model.BodyBox = new BoundingBox(this.View.GetTransform().position, view.BodyBoxSize);
        this.Model.AttackBox = new BoundingBox(this.View.GetTransform().position, view.AttackBoxSize, view.AttackBoxOffset);

    }

    // public void Initialize(IModel model, IView view, Transform playerTransform)
    // {



    // }


    public override void Handle()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            View.ToolSelector.SelectTool(0);
            Model.CurrentTool = AnimationType.Attack;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            View.ToolSelector.SelectTool(1);
            Model.CurrentTool = AnimationType.Mine;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            View.ToolSelector.SelectTool(2);
            Model.CurrentTool = AnimationType.Axe;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            View.ToolSelector.SelectTool(3);
            Model.CurrentTool = AnimationType.Dig;

        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            View.ToolSelector.SelectTool(4);
            Model.CurrentTool = AnimationType.Fish;

        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            View.ToolSelector.SelectTool(5);
            Model.CurrentTool = AnimationType.Hammer;

        }



        Model.BodyBox.UpdatePosition(View.GetTransform().position);

        Model.BodyBox.DrawDebug();

        Model.AttackBox.UpdatePosition(View.GetTransform().position, !facingRight);

        Model.AttackBox.DrawDebug();





        // Handle running input
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {

            Model.Status = Model.notInRunStatus() ? AnimationType.Run : AnimationType.Walk;
            Model.SetSpeed();
            Debug.Log(Model.Status);
        }


        // Handle movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // ASDW and arrow keys
        float moveY = Input.GetAxisRaw("Vertical");   // ASDW and arrow keys

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        Model.MovementDirection = moveDirection;

        // Handle character facing direction
        if (moveX > 0 && !facingRight && Model.notInFightStatus())
        {
            facingRight = true;
            View.FlipTransform(facingRight);
        }
        else if (moveX < 0 && facingRight && Model.notInFightStatus())
        {
            facingRight = false;
            View.FlipTransform(facingRight);
        }

        // Handle roll skill
        if (Input.GetKeyDown(KeyCode.Space) && Model.notInRolltStatus() && Model.notInFightStatus())
        {
            var tempStatus = Model.Status;
            Model.Status = AnimationType.Roll;
            View.PlayAnimation(AnimationType.Roll);
            Timing.RunCoroutine(RollCooldown(tempStatus == AnimationType.Run));
        }

        // Handle fight animation
        if (Input.GetMouseButtonDown(0) && Model.Status != AnimationType.Attack && Model.Status != AnimationType.Roll)
        {
            var tempStatus = Model.Status;

            Model.Status = AnimationType.Attack;
            View.PlayAnimation(Model.CurrentTool);
            Timing.RunCoroutine(ToolCooldown(tempStatus == AnimationType.Run));
        }

        // Handle character movement and animations based on status
        switch (Model.Status)
        {
            case AnimationType.Roll:
                // Move the character with roll speed
                View.SetPosition(moveDirection * Model.RollSpeed * Time.deltaTime);
                break;

            case AnimationType.Attack:
                // No movement during fight animation
                break;

            default:
                // Move the character normally
                View.SetPosition(moveDirection * Model.MoveSpeed * Time.deltaTime);
                if (moveDirection != Vector2.zero)
                {
                    View.PlayAnimation(Model.Status == AnimationType.Run ? AnimationType.Run : AnimationType.Walk);
                }
                else
                {
                    View.PlayAnimation(AnimationType.Idle);
                }
                break;
        }
    }

    public CharacterModel getModel()
    {
        return this.Model;
    }

    private IEnumerator<float> RollCooldown(bool isRunning)
    {
        // Duration of the roll animation
        yield return Timing.WaitForSeconds(0.5f); // Adjust based on your animation length

        Model.Status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }

    private IEnumerator<float> ToolCooldown(bool isRunning)
    {
        // Duration of the fight animation
        float cooldownTime = 0.5f; // Default cooldown time

        switch (Model.CurrentTool)
        {
            case AnimationType.Attack:
                cooldownTime = 0.5f; // Set cooldown time for Attack
                break;
            case AnimationType.Dig:
                cooldownTime = 0.6f; // Set cooldown time for Dig
                break;
            case AnimationType.Hammer:
                cooldownTime = 1f; // Set cooldown time for Hammer
                break;
            case AnimationType.Axe:
                cooldownTime = 0.7f; // Set cooldown time for Axe
                break;
            case AnimationType.Mine:
                cooldownTime = 0.9f; // Set cooldown time for Mine
                break;
            default:
                cooldownTime = 0.5f; // Default cooldown time if no match
                break;
        }
        yield return Timing.WaitForSeconds(cooldownTime); // Adjust based on your animation length
        Model.Status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }




    private IEnumerator ApplyDamageWithDelay(IDamageable DamageableController)
    {
        // Set delaying flag to prevent overlapping delays
        isDelaying = true;

        // Wait for 0.3 seconds
        yield return new WaitForSeconds(0.25f);

        // Get the skeleton's controller to deal damage
        if (Time.time - Model.lastHitTime >= 0.5f)
        {
            DamageableController.TakeHit(4); // Character deals 10 damage to skeleton
            Model.lastHitTime = Time.time;
        }

        // Reset delaying flag
        isDelaying = false;
    }

    public void AttackCollisionCheck(IDamageable controller)
    {
        if (Model.AttackBox.Intersects(controller.GetBodyBox()) && !Model.notInFightStatus())
        {
            if (!isDelaying)
            {
                // Start the delay coroutine
                View.StartCoroutine(ApplyDamageWithDelay(controller));
            }
        }
    }



}
