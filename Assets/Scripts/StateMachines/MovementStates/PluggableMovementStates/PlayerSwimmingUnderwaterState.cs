﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Events;

[CreateAssetMenu(menuName = "StateSystem/Swimmer/Underwater", order = 3)]
public class SwimmingUnderwaterState : PlayerSwimState
{

    public GameEvent UnderwaterStrokeEvent;
    public PlayerMovementState AbovewaterState;

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
        base.ComputeVelocity(ppc, ref velocity);
        if (Input.GetButtonDown("Jump")) UnderwaterStrokeEvent.Raise();
        if (!CheckHeadUnderwater(ppc)) ppc.SetState(AbovewaterState);
    }
}
