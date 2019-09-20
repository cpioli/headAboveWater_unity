using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "StateSystem/Swimmer/Underwater", order = 3)]
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

    public override void ComputeVelocity(PlayerPlatformController ppc, ref Vector2 velocity)
    {
        base.ComputeVelocity(ppc, ref velocity);
        if (Input.GetButtonDown("Jump")) UnderwaterStrokeEvent.Invoke();
    }
}
