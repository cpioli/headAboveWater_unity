﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is the default/starting MovementState: when the player is in an
/// ordinary atmosphere
/// </summary>
public class MovementStateOnGround : MovementState
{

    public MovementStateOnGround(PlayerPlatformController ppController) : base(ppController)
    {
        //jumpTakeOffSpeed = 7f;
        //gravityModifier = 1f;
        this.ppController = ppController;
        moveSpeed = new Vector2(2.5f, 0f);
    }
    
    public override void OnStateEnter(Animator animator)
    {
        base.OnStateEnter(animator);
        animator.SetBool("inWater", false);
        ppController.gravityModifier = this.gravityModifier;
        ppController.jumpTakeOffSpeed = this.jumpTakeOffSpeed;
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    public override void Tick()
    {

        //throw new System.NotImplementedException();
    }

    public override Vector2 ComputeVelocity(bool grounded, ref Vector2 velocity)
    {
        //get values for target velocity
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal"); //comes from whatever connects to Horizontal: keypad, game controller, etc.

        if (Input.GetButtonDown("Jump") && grounded)
        { //effect: hold down for longer jump, once button is released velocity cuts in half
            velocity.y = jumpTakeOffSpeed;
        }
        else if (Input.GetButtonUp("Jump"))
        //effect: hold down for longer jump. Once released, velocity is cut in half
        {
            if (velocity.y > 0f) velocity.y = velocity.y * .5f;
        }

        return move;
    }
}