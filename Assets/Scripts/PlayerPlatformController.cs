using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;
using cpioli.Events;

public class PlayerPlatformController : PhysicsObject, ICommonGameEvents {

    protected enum LEDGE
    {
        LEFT,
        NONE,
        RIGHT
    };
    protected LEDGE ledgeType;

    private Vector2 lastClimbingLocation;
    private UnityEvent currentStrokeEvent;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private MovementState currentMoveState;
    private MovementStateInWater moveStateWater;
    private MovementStateOnGround moveStateGround;
    private bool underwater;
    private bool climbing;

    public Vector3Reference startPosition;
    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7f;
    [HideInInspector]
    public bool xMovement;


    void Awake ()
    {
        lastClimbingLocation = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        moveStateWater = GetComponent<MovementStateInWater>();
        moveStateGround = GetComponent<MovementStateOnGround>();
        currentMoveState = moveStateGround;
        currentMoveState.OnStateEnter(animator);
        grabbedLedge = false;
        climbing = false;
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
        if (!grabbedLedge && !climbing)
        {
            grabbedLedge = GrabbingLedge(out lastClimbingLocation);
            if (grabbedLedge)
            {
                print("I grabbed the ledge!?");
                GrabTheLedge(lastClimbingLocation);
            }
        }
        if(grabbedLedge)
        {
            if (LettingGoOfLedge())
            {
                grabbedLedge = false;
                CalculateMovement();
            } else if(ClimbingOverLedge())
            {
                grabbedLedge = false;
                climbing = true;
                CalculateMovement();
            }
        }
        else
        {
            /*if(climbing)
            {
                Vector2 distance = rBody2d.position - lastClimbingLocation;
                if (distance.magnitude > 1.0f)
                    climbing = false;
            }*/

            CalculateMovement();
        }
        
    }

    private bool LettingGoOfLedge()
    {
        bool falling = (ledgeType == LEDGE.LEFT && Input.GetKeyDown(KeyCode.A))
            || (ledgeType == LEDGE.RIGHT && Input.GetKeyDown(KeyCode.D))
            || (Input.GetKeyDown(KeyCode.S));
        if(falling)
        {
            print("Falling down");
        }

        return falling;
    }

    private bool ClimbingOverLedge()
    {
        bool isClimbing = (Input.GetKeyDown(KeyCode.Space));
        if (isClimbing)
        {
            print("Climbing");
        }
        return isClimbing;
    }

    private void CalculateMovement()
    {
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

    private bool GrabbingLedge(out Vector2 tilePos)
    {
        tilePos = Vector2.zero;
        if (tilesHit[0].worldPos == Vector3Int.zero) return false;
        int i;
        for (i = 0; i < tilesHit.Length; i++)
        {
            if (string.Equals(tilesHit[i].name, "spritesheet_ground_39")
             || string.Equals(tilesHit[i].name, "spritesheet_ground_18")
             || string.Equals(tilesHit[i].name, "spritesheet_ground_40")
             || string.Equals(tilesHit[i].name, "spritesheet_ground_19"))
            {
                break;
            }
        }
        if (i == tilesHit.Length)
        {
            print("No ledge type!");
            return false;
        }
        tilePos = new Vector2(tilesHit[i].worldPos.x, tilesHit[i].worldPos.y);
        Vector2 distance = rBody2d.position - tilePos;
        return (distance.y >= 0.5f && distance.y <= 1.2f);
        
    }

    private void GrabTheLedge(Vector2 tilePos)
    {
        grabbedLedge = true;
        Vector3 currPosition = transform.position;

        currPosition.y = tilePos.y + 1f;
        transform.position = currPosition;
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
    /*
     * 
     */
}
