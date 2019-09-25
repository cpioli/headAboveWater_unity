using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapBehaviour : MonoBehaviour {

    [Range(0.01f, 0.5f)]
    public float radius;
    public TileBase[] LedgeTiles;

    private const int cellToScreenWidth = 16; //16 cells in the screen
    private int[] nextLedgeIndices = new int[2];
    private int[] currLedgeIndices = new int[2];
    private float playerWidth;
    private Bounds tilemapBounds;
    private CircleCollider2D[] colliders = new CircleCollider2D[16];
    private Dictionary<int, LinkedList<Vector3Int>> ledgeIndex;
    private Tilemap tilemap;
    private TileData tileData;
    private Vector2 playerPosition;
    private GameObject swimmer;

	// Use this for initialization
	void Awake () {
		try
        {
            tilemap = GetComponent<Tilemap>();
        } catch(NullReferenceException e)
        {
            Debug.LogError("Could not find the Tilemap in object " + name);
            return;
        }
        tilemapBounds = tilemap.localBounds;
        playerWidth = GameObject.Find("Swimmer").GetComponent<CapsuleCollider2D>().size.x;
        swimmer = GameObject.Find("Swimmer");
        ledgeIndex = new Dictionary<int, LinkedList<Vector3Int>>();
        CreateLedgeIndex();
        nextLedgeIndices[0] = int.MinValue;
        nextLedgeIndices[1] = int.MinValue;
        currLedgeIndices[0] = int.MinValue;
        currLedgeIndices[1] = int.MinValue;
        UpdateSwimmerPosition(swimmer.transform.position, ref currLedgeIndices);
	}
	
	// Update is called once per frame
	void Update () {
        if(!UpdateSwimmerPosition(swimmer.transform.position, ref nextLedgeIndices))
        {
            print("New section(s) entered: " + nextLedgeIndices[0] + ", " + nextLedgeIndices[1]);
        }
	}

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
        else if(xPos < (float)(playerWidth / 2.0f))
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

    private void InitializeColliders()
    {
        for(int i = 0; i < colliders.Length; i++)
        {
            colliders[i] = new CircleCollider2D();
            colliders[i].radius = radius;
            colliders[i].transform.position = Vector3.zero;
        }
    }

    private void ReassignColliders()
    {
        if(currLedgeIndices[0] != nextLedgeIndices[0]
            && currLedgeIndices[0] != nextLedgeIndices[1])
        {

        }
    }

    private void RemoveColliders(int index)
    {
        LinkedList<Vector3Int> collidersToRemove = ledgeIndex[index];

        for(int i = 0; i < collidersToRemove.Count; i++)
        {
            
        }
    }

    private void CreateLedgeIndex()
    {
        int col = (int)tilemapBounds.min.x;
        int row = (int)tilemapBounds.min.y;
        int colMax = (int)tilemapBounds.max.x;
        int rowMax = (int)tilemapBounds.max.y;
        print("Bounds: " + "(" + col + "," + row + ")" + "," + "(" + colMax + "," + rowMax + ")");
        Vector3Int tilePosition = Vector3Int.zero;
        TileBase tb;
        while (col <= colMax)
        {
            row = (int)tilemapBounds.min.y;
            while (row <= rowMax)
            {
                tilePosition.x = col;
                tilePosition.y = row;
                tilePosition.z = 0;
                tilePosition = tilemap.WorldToCell(tilePosition);
                tb = null;
                if (tilemap.HasTile(tilePosition))
                {
                    tb = tilemap.GetTile(tilePosition);
                }
                else
                {
                    row++;
                    continue;
                }
                if ( tb != null &&IsLedgeTile(ref tb))
                {
                    InsertTileIntoIndex(ref tb, ref tilePosition);
                }
                else
                {
                    row++;
                    continue;
                }
                row++;
            }
            col++;
        }
    }

    private bool IsLedgeTile(ref TileBase _Tile)
    {
        for(int i = 0; i < LedgeTiles.Length; i++)
        {
            if (_Tile.name.Equals(LedgeTiles[i].name)) return true;
        }

        return false;
       
    }

    private void InsertTileIntoIndex(ref TileBase tb, ref Vector3Int position)
    {
        int key = position.x / cellToScreenWidth;
        if(!ledgeIndex.ContainsKey(key))
        {
            ledgeIndex.Add(key, new LinkedList<Vector3Int>());
        }
        ledgeIndex[key].AddLast(position);
    }
}
