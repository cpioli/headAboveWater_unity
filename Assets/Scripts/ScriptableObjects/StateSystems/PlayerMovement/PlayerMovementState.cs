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