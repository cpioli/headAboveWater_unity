using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapBehaviour : MonoBehaviour {

    [Range(0.01f, 0.5f)]
    public float radius;
    public TileBase[] LeftLedgeTiles;
    public TileBase[] RightLedgeTiles;
    public GameObject ledgeColliderContainer;

    private const int cellToScreenWidth = 16; //16 cells in the screen
    private int[] nextLedgeIndices = new int[2];
    private int[] currLedgeIndices = new int[2];
    private float playerWidth;
    private Bounds tilemapBounds;
    private CircleCollider2D[] colliders = new CircleCollider2D[16];
    private Dictionary<int, List<Vector2Int>> ledgeIndex;
    private Tilemap tilemap;
    private TileData tileData;
    private Vector2 playerPosition;
    private GameObject swimmer;

#region DefaultMonoBehaviourMethods
    void Awake() {
        try
        {
            tilemap = GetComponent<Tilemap>();
        } catch (NullReferenceException e)
        {
            Debug.LogError("Could not find the Tilemap in object " + name);
            return;
        }
        tilemapBounds = tilemap.localBounds;
        playerWidth = GameObject.Find("Swimmer").GetComponent<CapsuleCollider2D>().size.x;
        swimmer = GameObject.Find("Swimmer");
        ledgeIndex = new Dictionary<int, List<Vector2Int>>();
        CreateLedgeIndex();
        InitializeColliders();
        nextLedgeIndices[0] = int.MinValue;
        nextLedgeIndices[1] = int.MinValue;
        currLedgeIndices[0] = int.MinValue;
        currLedgeIndices[1] = int.MinValue;
        UpdateSwimmerPosition(swimmer.transform.position, ref currLedgeIndices);
    }

    void Start()
    {
        UpdateSwimmerPosition(swimmer.transform.position, ref nextLedgeIndices);
        AddColliders(nextLedgeIndices[0]);
        currLedgeIndices[0] = nextLedgeIndices[0];
        currLedgeIndices[1] = nextLedgeIndices[1];
    }

    // Update is called once per frame
    void Update() {
        if (!UpdateSwimmerPosition(swimmer.transform.position, ref nextLedgeIndices))
        {
            print("New section(s) entered: " + nextLedgeIndices[0] + ", " + nextLedgeIndices[1]);
            ReassignColliders();
            currLedgeIndices[0] = nextLedgeIndices[0];
            currLedgeIndices[1] = nextLedgeIndices[1];
        }
    }
   #endregion  
#region LedgeColliderInitialization
    private void InitializeColliders()
    {
        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = ledgeColliderContainer.AddComponent(typeof(CircleCollider2D)) as CircleCollider2D;
            colliders[i].radius = radius;
            colliders[i].isTrigger = true;
            colliders[i].offset = Vector2.zero;
        }
    }

    private void CreateLedgeIndex()
    {
        int col = (int)tilemapBounds.min.x;
        int row = (int)tilemapBounds.min.y;
        int colMax = (int)tilemapBounds.max.x;
        int rowMax = (int)tilemapBounds.max.y;
        Vector3Int tilePosition = Vector3Int.zero;
        TileBase tb;
        PlayerPlatformController.LEDGE ledgeType;
        while (col <= colMax)
        {
            row = (int)tilemapBounds.min.y;
            while (row <= rowMax)
            {
                tilePosition.x = col;
                tilePosition.y = row;
                tilePosition.z = 0;
                tilePosition = tilemap.WorldToCell(tilePosition);
                ledgeType = PlayerPlatformController.LEDGE.NONE;
                tb = null;
                if (tilemap.HasTile(tilePosition))
                {
                    tb = tilemap.GetTile(tilePosition);
                    ledgeType = IsLedgeTile(ref tb);
                    if (ledgeType != PlayerPlatformController.LEDGE.NONE)
                    {
                        InsertTileIntoIndex(ref ledgeType, ref tilePosition);
                    }
                }
                row++;
            }
            col++;
        }
    }

    public PlayerPlatformController.LEDGE IsLedgeTile(ref TileBase _Tile)
    {
        if (_Tile == null) return PlayerPlatformController.LEDGE.NONE;

        for (int i = 0; i < LeftLedgeTiles.Length; i++)
        {
            if (_Tile.name.Equals(LeftLedgeTiles[i].name))
                return PlayerPlatformController.LEDGE.LEFT;
        }
        for (int i = 0; i < RightLedgeTiles.Length; i++)
        {
            if (_Tile.name.Equals(RightLedgeTiles[i].name))
                return PlayerPlatformController.LEDGE.RIGHT;
        }

        return PlayerPlatformController.LEDGE.NONE;

    }

    private void InsertTileIntoIndex(ref PlayerPlatformController.LEDGE ledgeType, ref Vector3Int position)
    {
        int key = position.x / cellToScreenWidth;
        Vector2Int localPosition = new Vector2Int(position.x, position.y);
        if (ledgeType == PlayerPlatformController.LEDGE.RIGHT)
        {
            localPosition.x += 1;
        }
        localPosition.y += 1;
        if (!ledgeIndex.ContainsKey(key))
        {
            ledgeIndex.Add(key, new List<Vector2Int>());
        }
        ledgeIndex[key].Add(localPosition);
    }
#endregion
#region Update Ledge Colliders
    /// <summary>
    /// Returns true if ledge indices change
    /// </summary>
    /// <param name="position">The Position of the swimmer</param>
    /// <returns></returns>
    private bool UpdateSwimmerPosition(Vector3 position, ref int[] indices)
    {
        int index1 = (int)(position.x / cellToScreenWidth);
        float xPos = position.x - (float)(index1 * cellToScreenWidth);
        if (xPos > (float)(cellToScreenWidth - playerWidth / 2.0f)) //if overlap with "index" to right of current position
        {
            indices[0] = index1;
            indices[1] = index1 + 1;
        }
        else if (xPos < (float)(playerWidth / 2.0f))
        {
            indices[0] = index1 - 1;
            indices[1] = index1;
        } else
        {
            indices[0] = index1;
            indices[1] = int.MinValue;
        }

        return (indices[0] == currLedgeIndices[0] && indices[1] == currLedgeIndices[1]);
    }

    private void ReassignColliders()
    {
        //if the Swimmer's rigidbody is only located in one sector
        if (nextLedgeIndices[1] == int.MinValue)
        {
            if (currLedgeIndices[1] == nextLedgeIndices[0])
            { //we have exited the sector to our left
                print("Removing colliders from index " + currLedgeIndices[0]);
                RemoveColliders(currLedgeIndices[0]);
            }
            else if (currLedgeIndices[0] == nextLedgeIndices[0])
            { //we have exited the sector to our right
                print("Removing colliders from index " + currLedgeIndices[1]);
                RemoveColliders(currLedgeIndices[1]);
            }
        }
        else //if the player's rigidbody is in two sectors (rule: the sectors will always be adjacent)
        {
            print("Player is overlapping two sectors!");
            //if the player is "going right"
            if (currLedgeIndices[0] == nextLedgeIndices[0])
            {
                print("Adding colliders from index " + nextLedgeIndices[1]);
                AddColliders(nextLedgeIndices[1]);
            }
            else if (currLedgeIndices[0] == nextLedgeIndices[1])
            {
                print("Adding colliders from index " + nextLedgeIndices[0]);
                AddColliders(nextLedgeIndices[0]);
            }
        }
    }

    private void RemoveColliders(int index)
    {
        List<Vector2Int> collidersToRemove = ledgeIndex[index]; //remove the colliders from the most previous sector
        for(int i = 0; i < collidersToRemove.Count; i++)
        {
            for(int j = 0; j < colliders.Length; j++)
            {
                if (colliders[j].offset == collidersToRemove[i])
                {
                    colliders[j].offset = Vector2.zero;
                    break;
                }
            }
        }
    }

    private void AddColliders(int index)
    {
        List<Vector2Int> collidersToAdd = ledgeIndex[index];
        int thisColliderToAdd = 0, collidersIndex = 0;
        while(thisColliderToAdd < collidersToAdd.Count)
        {
            try
            {
                if(colliders[ collidersIndex ].offset == Vector2.zero)
                {
                    colliders[collidersIndex].offset = collidersToAdd[thisColliderToAdd];
                    thisColliderToAdd++;
                }
            } catch (IndexOutOfRangeException e)
            {
                Debug.LogError("Error: Not enough room for every Ledge collider!\n" + e.ToString());
            }
            collidersIndex++;
        }
    }
#endregion

}
