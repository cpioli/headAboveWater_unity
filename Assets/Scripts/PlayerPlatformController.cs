using UnityEngine;
using UnityEngine.Tilemaps;
using cpioli.Variables;
using cpioli.Events;

public class PlayerPlatformController : PhysicsObject, ICommonGameEvents {

    public enum LEDGE
    {
        LEFT,
        NONE,
        RIGHT
    };

    private SpriteRenderer spriteRenderer;
    private PlayerMovementState currentPMState;

    [HideInInspector]
    public bool exhausted;
    [HideInInspector]
    public float maxSpeed = 7;
    [HideInInspector]
    public float jumpTakeOffSpeed = 7f;
    [HideInInspector]
    public Vector2 lastClimbingLocation;
    [HideInInspector]
    public bool xMovement;
    [HideInInspector]
    public Vector2 move;
    [HideInInspector]
    public BoxCollider2D headCollider, waterCollider, ledgeHangCollider;
    [HideInInspector]
    public Animator animator;
    [HideInInspector]
    public LEDGE ledgeType;

    public PlayerMovementState initialPMState;
    public Vector3Reference startPosition;

    void Awake ()
    {
        lastClimbingLocation = Vector2.zero;
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        SetState(initialPMState);
        move = Vector2.zero;
        exhausted = false;
        headCollider = gameObject.GetComponentInChildren<BoxCollider2D>();
        waterCollider = GameObject.FindGameObjectWithTag("water").GetComponent<BoxCollider2D>();
        //ledgeHangCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
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
        animator.SetFloat("velocityX", move.x);
        animator.SetFloat("velocityY", velocity.y);
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
            {
                return true;
            }

        }

        return false;
    }
    
    public bool isGrounded()
    {
        return this.grounded;
    }

    public void SetHanging(bool isHanging)
    {
        hanging = isHanging;
    }

    public void GetLedgeInfo(Vector2 move)
    {
        int count = rBody2d.Cast(move, contactFilter, hitBuffer, 1.0f);
        ResetHitTiles();
        ledgeType = LEDGE.NONE;
        lastClimbingLocation = Vector3.zero;
        TileData td;
        for (int i = 0; i < count; i++)
        {
            Tilemap tm = hitBuffer[i].collider.GetComponent<Tilemap>();
            if (tm == null) continue;
            GetTile(hitBuffer[i], out td);
            if (string.Equals(td.name, "spritesheet_ground_39")
             || string.Equals(td.name, "spritesheet_ground_18"))
            {
                ledgeType = LEDGE.RIGHT;
                lastClimbingLocation.x = td.worldPos.x;
                lastClimbingLocation.y = td.worldPos.y;
                //return;
            }
            else if (string.Equals(td.name, "spritesheet_ground_40")
           || string.Equals(td.name, "spritesheet_ground_19"))
            {
                ledgeType = LEDGE.LEFT;
                lastClimbingLocation.x = td.worldPos.x;
                lastClimbingLocation.y = td.worldPos.y;
                //return;
            }
        }
        //ledgeType = LEDGE.NONE;
        //return;
    }
}
