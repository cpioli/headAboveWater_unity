using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

public class PlayerSwimState : PlayerMovementState
{
    public GameEvent StrokeEvent;
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

    }


}