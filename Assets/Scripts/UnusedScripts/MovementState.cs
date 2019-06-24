using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cpioli.Variables;
using UnityEngine.Events;

public abstract class MovementState {

    protected PlayerPlatformController ppController;
    protected Animator animator;
    protected Vector2 moveSpeed; //velocity.x
    protected FloatReference jumpTakeOffSpeed;
    protected FloatReference gravityModifier;
    protected bool grounded;
    protected bool exhausted;

    public MovementState(PlayerPlatformController ppController)
    {
        this.ppController = ppController;
    }

    public abstract void Tick();

    public abstract Vector2 ComputeVelocity(bool grounded, ref Vector2 velocity);

    public virtual void OnStateEnter(Animator animator)
    {
        this.animator = animator;
    }
    public virtual void OnStateExit()
    {

    }

    public virtual void SetPlayerGrounded(bool isGrounded)
    {
        this.grounded = isGrounded;
    }
}
