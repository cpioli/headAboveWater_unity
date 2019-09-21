using UnityEngine;
using UnityEngine.Events;
using cpioli.Variables;
using cpioli.Events;

public class PlayerPlatformController : PhysicsObject, ICommonGameEvents {

    public enum LEDGE
    {
        LEFT,
        NONE,
        RIGHT
    };
    public LEDGE ledgeType;

    private Vector2 lastClimbingLocation;
    private UnityEvent currentStrokeEvent;
    private SpriteRenderer spriteRenderer;
    private PlayerMovementState currentPMState;
    //private bool climbing;

    [HideInInspector]
    public bool xMovement;
    [HideInInspector]
    public Vector2 move;
    [HideInInspector]
    public BoxCollider2D headCollider, waterCollider;
    public PlayerMovementState initialPMState;
    public Animator animator;
    public bool exhausted;
    public bool inWater;
    public Vector3Reference startPosition;
    public UnityEvent riverbedWalkingEvent;
    public UnityEvent riverbedStillEvent;
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 7f;

    void Awake ()
    {
        lastClimbingLocation = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        SetState(initialPMState);
        //grabbedLedge = false;
        //climbing = false;
        inWater = false;
        move = Vector2.zero;
        exhausted = false;
        headCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        waterCollider = GameObject.FindGameObjectWithTag("water").GetComponent<BoxCollider2D>();
    }
	
    public void SetState(PlayerMovementState mState)
    {
        if(currentPMState != null)
            currentPMState.OnStateExit(this);
        currentPMState = mState;
        currentPMState.OnStateEnter(this);
    }

    //called once per frame (FixedVelocity can be called more than once per frame)
    protected override void ComputeVelocity()
    {
        if (paused || gameOver) return;
        currentPMState.ComputeVelocity(this, ref this.velocity);
        if (grounded != animator.GetBool("grounded"))
            animator.SetBool("grounded", grounded);
        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < -0.01f));
        if (flipSprite) spriteRenderer.flipX = !spriteRenderer.flipX;
        targetVelocity.x = move.x * maxSpeed;
        animator.SetFloat("velocityX", Mathf.Abs(targetVelocity.x) / maxSpeed);
        animator.SetFloat("velocityY", targetVelocity.y);
        /*if (!grabbedLedge && !climbing)
        {
            CalculateMovement();
            grabbedLedge = GrabbingLedge(out lastClimbingLocation);
            if (grabbedLedge)
            {
                print("I grabbed the ledge!?");
                GrabTheLedge(lastClimbingLocation);
            }
        }
        else if(grabbedLedge && !climbing)
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
        else if(!grabbedLedge && climbing)
        {
            Vector2 distance = rBody2d.position - lastClimbingLocation;
            if (distance.magnitude > 1.0f)
                    climbing = false;
        }*/

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
            //print("No ledge type!");
            return false;
        }
        tilePos = new Vector2(tilesHit[i].worldPos.x, tilesHit[i].worldPos.y);
        Vector2 distance = rBody2d.position - tilePos;
        return (distance.y >= 0.25f && distance.y <= 1.2f);
        
    }

    private void GrabTheLedge(Vector2 tilePos)
    {
        //grabbedLedge = true;
        Vector3 currPosition = transform.position;

        currPosition.y = tilePos.y + 1f;
        transform.position = currPosition;
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
    }

    public void LevelCompleted()
    {
        throw new System.NotImplementedException();
    }

    public bool FindCollision(string tagName)
    {
        for(int i = 0; i < hitBufferList.Count; i++)
        {
            if (hitBufferList[i].collider.gameObject.CompareTag(tagName))
                return true;
        }

        return false;
        
    }    
}
