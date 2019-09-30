using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

[CreateAssetMenu(menuName="StateSystem/Swimmer/Abovewater", order = 2)]
public class PlayerSwimmingAbovewaterState : PlayerMovementState
{
    public GameEvent StrokeEvent;
    public GameEvent AbovewaterStrokeEvent;
    public PlayerMovementState UnderwaterState;
    public PlayerMovementState LedgeHangState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("inWater", true);
        Debug.Log("Player has entered the SwimmingAbovewaterState");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Player has exited the SwimmingAbovewaterState");
    }

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        ppc.move = Vector2.zero;
        ppc.move.x = Input.GetAxis("Horizontal");
        if (ppc.exhausted) return;
        if (Input.GetButtonDown("Jump"))
        {
            Debug.Log("Jumping!");
            velocity.y = ppc.jumpTakeOffSpeed;
            ppc.animator.SetTrigger("strokePerformed");
            StrokeEvent.Raise();
            AbovewaterStrokeEvent.Raise();
        }
        if (ppc.headCollider.IsTouching(ppc.waterCollider)) ppc.SetState(UnderwaterState);
        if (ppc.FindCollision("Ledge"))
        {
            ppc.GetLedgeInfo(ppc.move);
            if (ppc.ledgeType == PlayerPlatformController.LEDGE.NONE) return;
            ppc.SetState(LedgeHangState);
        }
    }
}