using UnityEngine;
using UnityEngine.Events;

public class PlayerPlatformController : PhysicsObject {

    private UnityEvent currentStrokeEvent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private MovementState currentMoveState;
    private MovementStateInWater moveStateWater;
    private MovementStateOnGround moveStateGround;
    private bool exhausted;
    private bool underwater;
    private bool isPaused;

    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7f;
    public bool xMovement;

    void Awake ()
    {
        exhausted = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveStateWater = GetComponent<MovementStateInWater>();
        moveStateGround = GetComponent<MovementStateOnGround>();
        currentMoveState = moveStateGround;
        currentMoveState.OnStateEnter(animator);
	}
	
    public void SetState(MovementState mState)
    {
        currentMoveState.OnStateExit();
        currentMoveState = mState;
        currentMoveState.OnStateEnter(animator);
    }

    //called once per frame (FixedVelocity can be called more than once per frame)
    protected override void ComputeVelocity()
    {
        if (isPaused) return;
        //get values for target velocity. Move is a value between [-1, 0, 1]
        Vector2 move = Vector2.zero;
        //move = currentMovementType.ComputeVelocity(underwater, exhausted, ref velocity);
        move = currentMoveState.ComputeVelocity(grounded, ref velocity);
        //UpdateGrounded only runs when the player-character is grounded
        if (grounded != animator.GetBool("grounded")) UpdateGrounded(grounded);
        //if (currentMovementType == underwaterGroundMovement) UpdateRiverbedXMovement(move);
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        targetVelocity = move * maxSpeed;
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
    }

    /*
     * Updates the Animator's grounded boolean
     * ALSO: updates the currentMovementType
     */
    private void UpdateGrounded(bool grounded)
    {
        animator.SetBool("grounded", grounded);
    }

    private void UpdateRiverbedXMovement(Vector2 move)
    {
        if(Mathf.Abs(move.x) > 0.01f && !xMovement)
        {
            xMovement = true;
            riverbedWalkingEvent.Invoke();
        }
        if(Mathf.Abs(move.x) < 0.01f && xMovement)
        {
            xMovement = false;
            riverbedStillEvent.Invoke();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("water"))
        {
            print("UNDERWATER!");
            SetState(moveStateWater);
            velocity.y *= 0.6f;
        }
    }

    public void SetSwimmerExhaustion(bool exhausted)
    {
        this.exhausted = exhausted;
    }

    public void SetUnderwater(bool underwater)
    {
        this.underwater = underwater;
        print("Underwater = " + underwater);
    }

    public bool GetUnderwater()
    {
        return underwater;
    }

    public void SetPaused(bool isPaused)
    {
        this.isPaused = isPaused;
    }
}
