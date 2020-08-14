using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChunkBackground : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    public RefrenceManager refrenceManager;
    public int[,] tileIDs;

    public int chunkX, chunkY;

    public void Awake()
    {
        tilemap = GetComponent<Tilemap>();
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
    }

    public void setTileToIdArray(int[,] ids)
    {

        // set all of the tileIds in the chunk to the input
        tileIDs = ids;

        // go through each id and set it's respective tile in the tilemap 
        for (int y = 0; y < tileIDs.GetLength(1); y++)
        {
            for (int x = 0; x < tileIDs.GetLength(0); x++)
            {
                // set all tiles in the tile map to the tile id in the array
                tilemap.SetTile(new Vector3Int(chunkX + x, chunkY + y, 0), refrenceManager.itemGroups[tileIDs[x, y]].tile);
            }
        }
    }
}
