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
        if (paused || gameOver) return;
        Vector2 move = Vector2.zero;
        move = currentMoveState.ComputeVelocity(grounded, ref velocity);
        //UpdateGrounded only runs when the player-character is grounded
        if (grounded != animator.GetBool("grounded")) UpdateGrounded(grounded);
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

    public void SetPaused(bool paused)
    {
        this.paused = paused;
        if (paused) animator.enabled = false;
        else animator.enabled = true;
    }

    public void GameOver()
    {
        base.gameOver = true;
    }
}
