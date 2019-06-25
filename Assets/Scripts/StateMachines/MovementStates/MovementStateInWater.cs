using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// The class housing the player's underwater movement state
/// Unlike the GroundMovement class, this class keeps track of whether the 
/// player is above or underwater.
/// An event listener for submerging and emerging from the river must be included
/// </summary>
public class MovementStateInWater : MovementState {

    public UnityEvent StrokeEvent;
    public UnityEvent UnderwaterStrokeEvent;
    public UnityEvent SurfaceStrokeEvent;
    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;

    private const float waterGravityModifier = 0.265f;
    private bool submerged;

    public MovementStateInWater(PlayerPlatformController ppController) : base(ppController)
    {
        this.ppController = ppController;
        moveSpeed = new Vector2(2f, 0f);
    }

    public override void OnStateEnter(Animator animator)
    {
        base.OnStateEnter(animator);
        animator.SetBool("inWater", true);
        ppController.gravityModifier = this.gravityModifier;
        ppController.jumpTakeOffSpeed = this.jumpTakeOffSpeed;
        
    }

    public override void OnStateExit()
    {
        base.OnStateExit();
    }

    /// <summary>
    /// Keeps track of time underwater or above water for the Oxygen and
    /// Stamina meters. Also keeps track of time.
    /// </summary>
    public override void Tick()
    {
        throw new System.NotImplementedException();
    }

    //returns a value for vector.y
    //probably should delegate the entire jump procedure to this method, but
    //it could override the normal tracking code in PhysicsObject.cs
    public override Vector2 ComputeVelocity(bool grounded, ref Vector2 velocity)
    {
        Vector2 move = Vector2.zero;
        move.x = Input.GetAxis("Horizontal");
        //if(grounded && ppController.xMovement && Mathf.Abs(move.x) > 0.0f)
        if (!exhausted) ComputeYVelocity(ref velocity);
        print("Exhausted: " + exhausted);
        return move;
    }

    private void ComputeYVelocity(ref Vector2 velocity)
    {
        print("ComputeYVelocity is called.");
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = jumpTakeOffSpeed;
            animator.SetTrigger("strokePerformed");
            StrokeEvent.Invoke();
            if (submerged) UnderwaterStrokeEvent.Invoke();
            else SurfaceStrokeEvent.Invoke();
        }
        else if (Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0.0f) velocity.y *= 0.5f;
        }
    }

    public void SetSubmerged(bool isSubmerged)
    {
        this.submerged = isSubmerged;
    }

    public void SetExhausted(bool isExhausted)
    {
        this.exhausted = isExhausted;
    }
}
