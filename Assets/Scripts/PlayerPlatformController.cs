using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;
using cpioli.Events;

public class PlayerPlatformController : PhysicsObject, ICommonGameEvents {

    private UnityEvent currentStrokeEvent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private MovementState currentMoveState;
    private MovementStateInWater moveStateWater;
    private MovementStateOnGround moveStateGround;
    //private ICommonGameEvents[] childrenListeners;
    private bool underwater;

    public Vector3Reference startPosition;
    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7f;
    public bool xMovement;

    void Awake ()
    {
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
        if (grounded != animator.GetBool("grounded"))
            animator.SetBool("grounded", grounded);
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite) spriteRenderer.flipX = !spriteRenderer.flipX;
        targetVelocity = move * maxSpeed;
        animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetFloat("velocityY", velocity.y);
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

    public void GameOver()
    {
        base.gameOver = true;

    }

    public void GamePaused()
    {
        this.paused = true;
        animator.enabled = false;
    }

    public void GameResumed()
    {
        this.paused = false;
        animator.enabled = true;
    }

    public void LevelStarted()
    {
        base.gameOver = false;
        gameObject.transform.position = startPosition;
        SetState(moveStateGround);
    }

    public void LevelCompleted()
    {
        throw new System.NotImplementedException();
    }
}
