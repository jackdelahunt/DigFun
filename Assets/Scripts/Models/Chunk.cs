using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
    private RefrenceManager refrenceManager;

    // an array of tile ids that refer to the ids of the tiles in the chunk
    public int[,] tileIDs;

    // the biome of this chunk
    public Biome biome;

    // the xCoord of the chunk
    public int chunkX;

    // the yCoord of the chunk
    public int chunkY;

    private void Awake()
    {
        // create the tile id array, all values are 0
        tileIDs = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];
        tilemap = GetComponent<Tilemap>();
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
    }

    public void init() {
         // get the chunk daat from the terrain generator class and set the tilemap to it
        setTileToIdArray(TerrainGeneration.generateChunkTiles(chunkX, chunkY, biome));
    }

    // add a tile to a position in this chunk
    public bool addTile(Vector3Int worldPos, Tile tile, int itemRefrence)
    {
        // get the locaion of this tile but to the local chunk Coord
        Vector3Int localPos = convertWorldCoordToLocalCoord(worldPos);

        // if the tile is outside the chunk then do not add it
        if(!isThisTileInThisChunk(localPos))
            return false;

        // if there is no tile in this area then add the new one else return false
        if (tilemap.GetTile(worldPos) == null)
        {
            // set the correct value in the tile id array 
            tileIDs[localPos.x, localPos.y] = itemRefrence;

            // change the tile map to fit that change
            tilemap.SetTile(worldPos, tile);
            return true;
        }
        else
            return false;
    }

    // called when 
    public void setTileToIdArray(int[,] ids) {

        // set all of the tileIds in the chunk to the input
        tileIDs = ids;

        // go through each id and set it's respective tile in the tilemap 
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
