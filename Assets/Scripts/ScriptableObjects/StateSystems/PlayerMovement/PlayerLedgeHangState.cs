﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "StateSystem/Swimmer/LedgeHang", order =5)]
public class PlayerLedgeHangState : PlayerMovementState
{

    public PlayerMovementState UnderwaterSwimState;
    public PlayerMovementState LedgeClimbingState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetTrigger("grabbedLedge");
        Vector3 currentPos = ppc.gameObject.transform.position;
        currentPos.y = ppc.lastClimbingLocation.y + 1.0f;
        currentPos.x = ppc.lastClimbingLocation.x - 0.25f;
        if (ppc.ledgeType == PlayerPlatformController.LEDGE.RIGHT)
            currentPos.x = ppc.lastClimbingLocation.x + 1.25f;
        ppc.gameObject.transform.position = currentPos;
        ppc.move = Vector2.zero;
        ppc.SetHanging(true);
        Debug.Log("Entered the ledge hanging state");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        Debug.Log("Exiting the ledge hanging state!");
        ppc.SetHanging(false);
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        velocity.y = 0.0f;
        velocity.x = 0.0f;
        if ((SwimmerInput.GetKeyDown(KeyCode.A) && ppc.ledgeType == PlayerPlatformController.LEDGE.LEFT)
         || (SwimmerInput.GetKeyDown(KeyCode.D) && ppc.ledgeType == PlayerPlatformController.LEDGE.RIGHT)
         || SwimmerInput.GetKeyDown(KeyCode.S))      
        {
            ppc.animator.SetTrigger("grabbedLedge");
            ppc.SetState(UnderwaterSwimState);
            return;
        }
        else if (SwimmerInput.GetButtonDown("Jump"))
        {
            //velocity.y = ppc.jumpTakeOffSpeed;
            ppc.SetState(LedgeClimbingState);
            return;
        }
    }


}
