using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
    private RefrenceManager refrenceManager;
    public int[,] tileIDs;
    public Biome biome;
    public int chunkX;
    public int chunkY;

    private void Start()
    {
        tileIDs = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

        tilemap = GetComponent<Tilemap>();
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
        
        setTileToIdArray(TerrainGeneration.generateChunkTiles(chunkX, chunkY, biome));
    }

    // add a tile to a position in this chunk
    public bool addTile(Vector3Int worldPos, Tile tile, int itemRefrence)
    {
        Vector3Int localPos = convertWorldCoordToLocalCoord(worldPos);

        if(!isThisTileInThisChunk(localPos))
            return false;

        // if there is no tile in this area then add the new one else return false
        if (tilemap.GetTile(worldPos) == null)
        {
            tileIDs[localPos.x, localPos.y] = itemRefrence;
            tilemap.SetTile(worldPos, tile);
            return true;
        }
        else
            return false;
    }

    // called when 
    public void setTileToIdArray(int[,] ids) {
        tileIDs = ids;
        for(int y = 0; y < ids.GetLength(1); y++) {
            for(int x = 0; x < ids.GetLength(0); x++) {
                tilemap.SetTile(convertLocalCoordToWorldCoord(new Vector3Int(x, y, 0)), refrenceManager.getTile(ids[x, y]));
            }
        }
    }

    // removes a tile from the location asked and returns the 
    // id of that tile
    public int removeTile(Vector3Int worldPos)
    {
        Vector3Int localPos = convertWorldCoordToLocalCoord(worldPos);

        // if this tile is in the chunk bounds then return the tileID 
        // at that location, else return the flag - 1
        if(isThisTileInThisChunk(localPos)) {

            // remove that tile from the til map
            tilemap.SetTile(worldPos, null);

            // get the id of the tile that is there
            int idOfThatTile = tileIDs[localPos.x, localPos.y];

            // set the id of the tile in out tileID array to 0
            tileIDs[localPos.x, localPos.y] = 0;

            // return the id of the tile we broke
            return idOfThatTile;
        } else
            return -1;
    }

    // converts a vector3 that represents a tile in this chunk from it's world coords
    // to local co-ordinates for this chunk
    public Vector3Int convertWorldCoordToLocalCoord(Vector3Int worldPos)
    {
        return new Vector3Int(Mathf.Abs(Mathf.FloorToInt(chunkX - worldPos.x)), worldPos.y, 0);
    }

    public Vector3Int convertLocalCoordToWorldCoord(Vector3Int localPos) {
        return new Vector3Int(chunkX + localPos.x, localPos.y, localPos.z);
    }

    // used to verify if a tiles postion is within the chunkHeight * chunkWidth
    // tileID array
    public bool isThisTileInThisChunk(Vector3Int tilePosition) {

        // checking if the x position is in the range of the tile id array x
        bool xCorrect = tilePosition.x >= 0 && tilePosition.x <= tileIDs.GetLength(0) - 1;

        // checking if the x position is in the range of the tile id array x
        bool yCorrect = tilePosition.y >= 0 && tilePosition.y <= tileIDs.GetLength(1) - 1;

        // only true if both are in range
        return xCorrect && yCorrect;
    }
}   
