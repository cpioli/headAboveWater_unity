using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
/// Taken from the Unity2d platforming tutorial at 
/// </summary>
/// 



public class PhysicsObject : MonoBehaviour {
    //note: "TileData" is used by 2d-extras for its code
    //I might want to consider using a different name for this struct
    public struct TileData
    {
        public Vector3Int worldPos { get; set; }
        public string name { get; set; }
        public TileData(Vector3Int v3i, string name = "")
        {
            worldPos = v3i;
            this.name = name;
        }
    }


    public float minGroundNormalY = .65f;
    public float gravityModifier = 1f;

    protected const float minMoveDistance = 0.001f;
    protected const float shellRadius = 0.01f;

    protected bool paused;
    protected bool gameOver;
    protected bool grounded;
    protected bool grabbedLedge;
    protected Vector2 velocity;
    protected Vector2 targetVelocity; //this is where we store incoming input from outside of the class. We're going to plug this into our velocity calculation.
    protected Vector2 groundNormal;
    protected Rigidbody2D rBody2d;
    protected ContactFilter2D contactFilter;
    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected TileData[] tilesHit = new TileData[3];




    private void OnEnable()
    {
        rBody2d = GetComponent<Rigidbody2D>();

    }

    private void ResetHitTiles()
    {
        for (int i = 0; i < tilesHit.Length; i++)
        {
            tilesHit[i].name = "";
            tilesHit[i].worldPos = Vector3Int.zero;
        }
    }

    // Use this for initialization
    void Start() {
        paused = false;
        gameOver = false;
        contactFilter.useTriggers = false;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer));
        contactFilter.useLayerMask = true;
    }

    // Update is called once per frame
    void Update() {
        if (paused || gameOver) return;
        targetVelocity = Vector2.zero;
        ComputeVelocity();
    }

    protected virtual void ComputeVelocity()
    {

    }

    void FixedUpdate()
    {
        if (paused || gameOver) return;
        if (grabbedLedge) return;

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
        velocity.x = targetVelocity.x;

        grounded = false;
        Vector2 deltaPosition = velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x);
        Vector2 move = moveAlongGround * deltaPosition.x;
        Movement(move, false);
        move = Vector2.up * deltaPosition.y;
        Movement(move, true);

    }

    void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;
        if (distance > minMoveDistance)
        {
            int count = rBody2d.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            ResetHitTiles();
            Vector2 ledgeTilePosition = Vector2.zero;
            int j = 0;
            for (int i = 0; i < count; i++)
            {
                hitBufferList.Add(hitBuffer[i]);
                Tilemap tm = hitBuffer[i].collider.GetComponent<Tilemap>();
                if (tm == null) continue;
                GetTile(hitBuffer[i], out tilesHit[i]);
            }

            for (int i = 0; i < hitBufferList.Count; i++)
            {
                Vector2 currentNormal = hitBufferList[i].normal;
                if (currentNormal.y > minGroundNormalY)
                {
                    grounded = true;
                    if (yMovement)
                    {
                        groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0)
                {
                    velocity = velocity - projection * currentNormal;
                }

                float modifiedDistance = hitBufferList[i].distance - shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        rBody2d.position = rBody2d.position + move.normalized * distance;
    }

    private void GetTile(RaycastHit2D hit, out TileData data)
    {
        data = new TileData();
        Tilemap tm = hit.collider.GetComponent<Tilemap>();
        if (tm == null) return;

        Vector2 tilePos = 2f * hit.point - hit.centroid;
        tilePos.x = Mathf.Floor(tilePos.x);
        tilePos.y = Mathf.Floor(tilePos.y);
        Vector3Int worldPos = new Vector3Int((int)tilePos.x, (int)tilePos.y, 0);
        Vector3Int cellPos = tm.layoutGrid.WorldToCell(worldPos);
        TileBase tb = tm.GetTile(cellPos);

        if (tb == null) return;
        data.name = tb.name;
        data.worldPos = worldPos;

    }


}
