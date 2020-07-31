using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
    private RefrenceManager refrenceManager;
    public int[,] tileIDs;

    public int chunkX;
    public int chunkY;

    private void Start()
    {
        tileIDs = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

        tilemap = GetComponent<Tilemap>();
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
        generateStartTerrain();
    }

    // add a tile to a position in this chunk
    public bool addTile(Vector3Int pos, Tile tile, int itemRefrence)
    {
        Vector3Int local = convertWorldCoordToLocalCoord(pos);

        if(!isThisTileInThisChunk(local))
            return false;

        // if there is no tile in this area then add the new one else return false
        if (tilemap.GetTile(pos) == null)
        {
            tileIDs[local.x, local.y] = itemRefrence;
            tilemap.SetTile(pos, tile);
            return true;
        }
        else
            return false;
    }

    // removes a tile from the location asked and returns the 
    // id of that tile
    public int removeTile(Vector3Int pos)
    {
        Vector3Int local = convertWorldCoordToLocalCoord(pos);

        // if this tile is in the chunk bounds then return the tileID 
        // at that location, else return the flag - 1
        if(isThisTileInThisChunk(local)) {

            // remove that tile from the til map
            tilemap.SetTile(pos, null);

            // get the id of the tile that is there
            int idOfThatTile = tileIDs[local.x, local.y];

            // set the id of the tile in out tileID array to 0
            tileIDs[local.x, local.y] = 0;

            // return the id of the tile we broke
            return idOfThatTile;
        } else
            return -1;
    }

    public void generateStartTerrain()
    {
        // these values will be set by the biome in the future
        int terrainHeight = 20;
        float scale = 0.1f;
        // generate the cave structure
        // chunkHeight - terrainHeight = undergroundHeight??
        for (int y = chunkY; y < chunkY + LookUpData.chunkHeight - terrainHeight; y++)
        {
            for (int x = chunkX; x < chunkX + LookUpData.chunkWidth; x++)
            {
                if (!Noise.caveNoise(x, y, 0.4f, 0.15f, 0))
                    continue;

                if (y == LookUpData.chunkHeight - 1)
                    addTile(new Vector3Int(x, y, 0), refrenceManager.getTile(2), 2);
                else if (y > LookUpData.chunkHeight - 5)
                    addTile(new Vector3Int(x, y, 0), refrenceManager.getTile(1), 1);
                else
                    addTile(new Vector3Int(x, y, 0), refrenceManager.getTile(3), 3);

            }
        }

        // generate the above terrain
        for (int x = chunkX; x < chunkX + LookUpData.chunkWidth; x++)
        {
            float noise = Noise.terrainNoise(x, terrainHeight, scale, 100);
            int heightOfTerrainHere = Mathf.FloorToInt(terrainHeight * noise);

            for (int y = 0; y < heightOfTerrainHere; y++)
            {
                addTile(new Vector3Int(x, LookUpData.chunkHeight - terrainHeight + y, 0), refrenceManager.getTile(1), 1);
            }
        }
    }

    // converts a vector3 that represents a tile in this chunk from it's world coords
    // to local co-ordinates for this chunk
    public Vector3Int convertWorldCoordToLocalCoord(Vector3Int pos)
    {
        return new Vector3Int(Mathf.Abs(Mathf.FloorToInt(chunkX - pos.x)), pos.y, 0);
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
