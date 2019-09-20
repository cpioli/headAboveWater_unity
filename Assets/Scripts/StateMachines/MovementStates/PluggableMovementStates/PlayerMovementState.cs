using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;

public abstract class PlayerMovementState : ScriptableObject {

    public FloatReference gravityModifier;
    public FloatReference jumpTakeOffVelocity;
    public FloatReference moveVelocity;

    public abstract void ComputeVelocity(PlayerPlatformController ppc);
    public virtual void OnStateEnter(PlayerPlatformController ppc)
    {
        ppc.gravityModifier = this.gravityModifier;
        ppc.jumpTakeOffSpeed = this.jumpTakeOffVelocity;
        ppc.maxSpeed = this.moveVelocity;
    }
    public virtual void OnStateExit(PlayerPlatformController ppc)
    {

    }
}

public class PlayerSwimState : PlayerMovementState
{
    public UnityEvent StrokeEvent;
    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc)
    {
        ppc.move = Vector2.zero;
        ppc.move.x = Input.GetAxis("Horizontal");
        if (ppc.exhausted) return;
        if (Input.GetButtonDown("Jump"))
        {
            ppc.move.y = ppc.jumpTakeOffSpeed;
            ppc.animator.SetTrigger("strokePerformed");
            StrokeEvent.Invoke();
        }
    }
}

public abstract class SwimmingAbovewaterState : PlayerSwimState
{
    public UnityEvent AbovewaterStrokeEvent;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("inWater", true);
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc)
    {
        base.ComputeVelocity(ppc);
        if(Input.GetButtonDown("Jump")) AbovewaterStrokeEvent.Invoke();
    }
}

public class SwimmingUnderwaterState : PlayerSwimState
{

    public UnityEvent UnderwaterStrokeEvent;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("inWater", true);
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
    }

    public override void ComputeVelocity(PlayerPlatformController ppc)
    {
        base.ComputeVelocity(ppc);
        if (Input.GetButtonDown("Jump")) UnderwaterStrokeEvent.Invoke();
    }
}

public class PlayerMidairState : PlayerMovementState
{
    public override void ComputeVelocity(PlayerPlatformController ppc)
    {
        ppc.move = Vector2.zero;
        ppc.move.x = Input.GetAxis("Horizontal");
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

    public override void ComputeVelocity(PlayerPlatformController ppc)
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
    public override void ComputeVelocity(PlayerPlatformController ppc)
    {
        throw new System.NotImplementedException();
    }
}