using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

[CreateAssetMenu (menuName = "StateSystem/Swimmer/ClimbingLedgeState", order = 6)]
public class PlayerLedgeClimbingState : PlayerMovementState {

    private Vector3 ledgePosition;

    public GameEvent StrokeEvent;
    public GameEvent UnderwaterStrokeEvent;
    public PlayerMovementState UnderwaterSwimState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ledgePosition = ppc.lastClimbingLocation;
        Debug.Log("Entered the Climbing State");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Exited the Climbing State");
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
            //ppc.move.y = ppc.jumpTakeOffSpeed;
            ppc.animator.SetTrigger("strokePerformed");
            StrokeEvent.Raise();
        }
        if ((ppc.gameObject.transform.position - ledgePosition).magnitude < 1.0f) {
            ppc.SetState(UnderwaterSwimState);
        }
    }
}
