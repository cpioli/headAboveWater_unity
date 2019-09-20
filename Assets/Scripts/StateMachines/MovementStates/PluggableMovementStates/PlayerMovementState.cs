using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

public abstract class PlayerMovementState : ScriptableObject {

    public FloatReference gravityModifier;
    public FloatReference jumpTakeOffVelocity;
    public Vector2Reference moveVelocity;

    public abstract void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity);
    public virtual void OnStateEnter(PlayerPlatformController ppc)
    {
        ppc.gravityModifier = this.gravityModifier;
        ppc.jumpTakeOffSpeed = this.jumpTakeOffVelocity;
        ppc.maxSpeed = this.moveVelocity.Value.x;
    }
    public virtual void OnStateExit(PlayerPlatformController ppc)
    {

    }
}

public class LedgeHangState : PlayerMovementState
{
    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        if ((Input.GetKeyUp(KeyCode.A) && ppc.ledgeType == PlayerPlatformController.LEDGE.LEFT)
         || (Input.GetKeyUp(KeyCode.D) && ppc.ledgeType == PlayerPlatformController.LEDGE.RIGHT)
         || Input.GetKeyUp(KeyCode.S))
        {
            //ppc.SetPlayerMovementState(PlayerUnderwaterSwimState)
            return;
        }
        else if (Input.GetButtonDown("Jump"))
        {
            //ppc.SetPlayerMovementState(PlayerClimbingOverLedgeState)
        }
    }

}

public class ClimbingOverLedgeState : PlayerMovementState
{
    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        throw new System.NotImplementedException();
    }
}