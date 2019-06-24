using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using cpioli;

public class PlayerPlatformController : PhysicsObject {

    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7f;

    public GroundMovement groundMovement;
    public UnderwaterMovement underwaterMovement;
    public GroundMovement underwaterGroundMovement;

    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;
    private UnityEvent currentStrokeEvent;
    private Movement currentMovementType;

    private bool exhausted;
    private bool xMovement;
    private bool underwater;
    private bool isPaused;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

	void Awake ()
    {
        exhausted = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        currentMovementType = groundMovement;
        currentMovementType.Initialize(this.gameObject);
	}
	
    public void SetState(Movement mState)
    {
        currentMovementType = mState;
        currentMovementType.Initialize(this.gameObject);
    }

    //called once per frame (FixedVelocity can be called more than once per frame)
    protected override void ComputeVelocity()
    {
        if (isPaused) return;
        //get values for target velocity. Move is a value between [-1, 0, 1]
        Vector2 move = Vector2.zero;
        move = currentMovementType.ComputeVelocity(underwater, exhausted, ref velocity);

        //UpdateGrounded only runs when the player-character is grounded
        if (grounded != animator.GetBool("grounded")) UpdateGrounded(grounded);
        if (currentMovementType == underwaterGroundMovement) UpdateRiverbedXMovement(move);
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
        if(currentMovementType == underwaterMovement && grounded)
        {
            SetState(underwaterGroundMovement);
        }
        if(currentMovementType == underwaterGroundMovement && !grounded)
        {
            SetState(underwaterMovement);
        }
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
            SetState(underwaterMovement);
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
