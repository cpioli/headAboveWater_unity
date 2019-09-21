using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerSwimState : PlayerMovementState
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
            StrokeEvent.Invoke();
        }
    }

    protected bool CheckHeadUnderwater(PlayerPlatformController ppc)
    {
        return ppc.headCollider.IsTouching(ppc.waterCollider);
    }
}