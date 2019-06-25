using UnityEngine;
using cpioli.Variables;

public abstract class MovementState : MonoBehaviour {

    protected PlayerPlatformController ppController;
    protected Animator animator;
    protected Vector2 moveSpeed; //velocity.x
    public FloatReference jumpTakeOffSpeed;
    public FloatReference gravityModifier;
    public Vector2Reference airVelocity; //of an unladen swallow?
    public Vector2Reference groundedVelocity;
    protected bool exhausted;

    private void Awake()
    {
        this.ppController = gameObject.GetComponent<PlayerPlatformController>();
        exhausted = false;
    }

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
}
