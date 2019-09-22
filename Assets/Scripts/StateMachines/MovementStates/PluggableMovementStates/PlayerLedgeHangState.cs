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
        Vector3 currentPos = ppc.gameObject.transform.position;
        currentPos.y = ppc.lastClimbingLocation.y + 1.0f;
        ppc.gameObject.transform.position = currentPos;
        Debug.Log("Entered the ledge hanging state");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        Debug.Log("Exiting the ledge hanging state!");
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        if ((Input.GetKeyDown(KeyCode.A) && ppc.ledgeType == PlayerPlatformController.LEDGE.LEFT)
         || (Input.GetKeyDown(KeyCode.D) && ppc.ledgeType == PlayerPlatformController.LEDGE.RIGHT)
         || Input.GetKeyDown(KeyCode.S))      
        {
            ppc.SetState(LedgeClimbingState);
        }
        else if (Input.GetButtonDown("Jump"))
        {
            velocity.y = ppc.jumpTakeOffSpeed;
            ppc.SetState(LedgeClimbingState);
        }
    }


}
