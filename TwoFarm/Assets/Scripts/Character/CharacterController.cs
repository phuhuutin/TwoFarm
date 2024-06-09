using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class CharacterController
{
    private CharacterModel _model;
    private CharacterView _view;
    private bool _facingRight = true;

    public void SetData(CharacterModel model, CharacterView view)
    {
        // Initialize Data Model, View
        this._model = model;
        this._view = view;
    }
    
    public void Handle()
    {
        // Handle running input
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _model.status = _model.status == AnimationType.Run ? AnimationType.Walk : AnimationType.Run;
            _model.SetSpeed();
        }


        // Handle movement input
        float moveX = Input.GetAxisRaw("Horizontal"); // ASDW and arrow keys
        float moveY = Input.GetAxisRaw("Vertical");   // ASDW and arrow keys

        Vector2 moveDirection = new Vector2(moveX, moveY).normalized;
        _model.MovementDirection = moveDirection;
        
        // Handle character facing direction
        if (moveX > 0 && !_facingRight)
        {
            _facingRight = true;
            _view.FlipCharacter(_facingRight);
        }
        else if (moveX < 0 && _facingRight)
        {
            _facingRight = false;
            _view.FlipCharacter(_facingRight);
        }

        // Handle roll skill
        if (Input.GetKeyDown(KeyCode.Space) && _model.status != AnimationType.Roll && _model.status != AnimationType.Fight)
        {
            var tempStatus = _model.status;
            _model.status = AnimationType.Roll;
            _view.PlayAnimation(AnimationType.Roll);
            Timing.RunCoroutine(RollCooldown(tempStatus == AnimationType.Run));
        }

        // Handle fight animation
        if (Input.GetMouseButtonDown(0) && _model.status != AnimationType.Fight && _model.status != AnimationType.Roll)
        {
            var tempStatus = _model.status;

            _model.status = AnimationType.Fight;
            _view.PlayAnimation(AnimationType.Fight);
            Timing.RunCoroutine(FightCooldown(tempStatus == AnimationType.Run));
        }

        // Handle character movement and animations based on status
        switch (_model.status)
        {
            case AnimationType.Roll:
                // Move the character with roll speed
                _view.SetPosition(moveDirection * _model.RollSpeed * Time.deltaTime);
                break;

            case AnimationType.Fight:
                // No movement during fight animation
                break;

            default:
                // Move the character normally
                _view.SetPosition(moveDirection * _model.MoveSpeed * Time.deltaTime);
                if (moveDirection != Vector2.zero)
                {
                    _view.PlayAnimation(_model.status == AnimationType.Run ? AnimationType.Run : AnimationType.Walk);
                }
                else
                {
                    _view.PlayAnimation(AnimationType.Idle);
                }
                break;
        }
    }

  

    private IEnumerator<float> RollCooldown(bool isRunning)
    {
        // Duration of the roll animation
        yield return  Timing.WaitForSeconds(0.5f); // Adjust based on your animation length
        
        _model.status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }

    private IEnumerator<float> FightCooldown(bool isRunning)
    {
        // Duration of the fight animation
        yield return Timing.WaitForSeconds(0.5f); // Adjust based on your animation length
        _model.status = isRunning ? AnimationType.Run : AnimationType.Walk; // Return to idle status after fighting
    }

}
