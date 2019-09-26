using UnityEngine;
using cpioli.Events;

[CreateAssetMenu(menuName = "StateSystem/Swimmer/Underwater", order = 3)]
public class PlayerSwimmingUnderwaterState : PlayerMovementState
{
    public GameEvent StrokeEvent;
    public GameEvent UnderwaterStrokeEvent;
    public PlayerMovementState AbovewaterState;
    public PlayerMovementState GroundedState;
    public PlayerMovementState LedgeHangState;

    public override void OnStateEnter(PlayerPlatformController ppc)
    {
        base.OnStateEnter(ppc);
        ppc.animator.SetBool("inWater", true);
        Debug.Log("Entered the underwater state!");
    }

    public override void OnStateExit(PlayerPlatformController ppc)
    {
        base.OnStateExit(ppc);
        Debug.Log("Exited the underwater state!");
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
        }
        if (Input.GetButtonDown("Jump")) UnderwaterStrokeEvent.Raise();
        if (ppc.isGrounded())
        {
            ppc.SetState(GroundedState);
        }
        if (!ppc.headCollider.IsTouching(ppc.waterCollider))
        {
            ppc.SetState(AbovewaterState);
        }
        if (ppc.GrabbingLedge())
        {
            ppc.SetState(LedgeHangState);
        }
    }
}
