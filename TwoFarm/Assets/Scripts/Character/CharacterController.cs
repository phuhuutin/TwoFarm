using System.Collections;
using System.Collections.Generic;
using MVC_EXAMPLE;
using UnityEngine;
using MEC;

public class Controller
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
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            _model.IsRunning = !_model.IsRunning;
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
        if (Input.GetKeyDown(KeyCode.Space) && !_model.IsRolling)
        {
            // Move the character
            _model.IsRolling = true;
            _view.PlayAnimation(AnimationType.Roll);
            _view.SetPosition(moveDirection * _model.MoveSpeed * 2 * Time.deltaTime);
            
            Timing.RunCoroutine(RollCooldown()); // Package Library Support Coroutine when without Monobehavior (MEC on unity asset Store)
            
        }


        if (_model.IsRolling)
        {
            // Move the character with roll speed
            _view.SetPosition(moveDirection * _model.RollSpeed * Time.deltaTime);
        }
        else
        {
            // Move the character
            _view.SetPosition(moveDirection * _model.MoveSpeed * Time.deltaTime);
            // Handle animations
            if (moveDirection != Vector2.zero)
            {
                if (_model.IsRunning)
                {
                    _view.PlayAnimation(AnimationType.Run);
                }
                else
                {
                    _view.PlayAnimation(AnimationType.Walk);
                }
            }
            else
            {
                _view.PlayAnimation(AnimationType.Idle);
            }
        }
    }

    private IEnumerator<float> RollCooldown()
    {
        // Duration of the roll animation
        yield return  Timing.WaitForSeconds(0.5f); // Adjust based on your animation length
        _model.IsRolling = false;
    }

}
