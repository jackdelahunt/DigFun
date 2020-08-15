using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
    private RefrenceManager refrenceManager;
    public GameObject chunkBackgroundPrefab;
    public GameObject itemEntityPrefab;


    // an array of tile ids that refer to the ids of the tiles in the chunk
    public int[,] tileIDs;

    // the biome of this chunk
    public BiomeRefrence biomeRefrence;

    // the xCoord of the chunk
    public int chunkX;

    // the yCoord of the chunk
    public int chunkY;

    // the seed of the world
    public int seed;

    // list of all entities item entities in this chunk
    public List<GameObject> itemEntities;

    public List<GameObject> blockEntities;

    // the background tile map for this chunk
    public ChunkBackground chunkBackground;

    // is this object active or not
    public bool loaded;

    private void Awake()
    {
        // create the tile id array, all values are 0
        tileIDs = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];
        tilemap = GetComponent<Tilemap>();

        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
    }

    public void init()
    {
        // get the chunk daat from the terrain generator class and set the tilemap to it
        setTileToIdArray(TerrainGeneration.generateChunkTiles(chunkX, chunkY, seed, biomeRefrence.biome));

        // create the chunk background object
        createChunkBackground();

        // set the backgrounds tiles correctly based on our tile id
        chunkBackground.setTileToIdArray(TerrainGeneration.generateChunkBackground(tileIDs, biomeRefrence.biome));
    }

    public void setLoadStatus(bool loaded)
    {
        // save the load value
        this.loaded = loaded;

        // update the entities to react to the load update
        updateItemEntities();

        // load or unload the chunk based on the input
        gameObject.SetActive(loaded);
    }

    public void createChunkBackground()
    {
        // create the chunk background object
        chunkBackground = Instantiate(chunkBackgroundPrefab, gameObject.transform).GetComponent<ChunkBackground>();

        // set it's chunkX to ours
        chunkBackground.chunkX = chunkX;

        // set it's chunkY to ours 
        chunkBackground.chunkY = chunkY;
    }

    // add a tile to a position in this chunk
    public bool addTile(Vector3Int worldPos, ItemGroup itemGroup)
    {
        // get the locaion of this tile but to the local chunk Coord
        Vector3Int localPos = ChunkHelpers.convertWorldCoordToLocalCoord(worldPos, chunkX);

        // if the tile is outside the chunk then do not add it, or it is air
        if (!isThisTileInThisChunk(localPos) || itemGroup.id == 0)
            return false;

        // if there is no tile in this area then add the new one else return false
        if (tilemap.GetTile(worldPos) == null)
        {
            // set the correct value in the tile id array 
            tileIDs[localPos.x, localPos.y] = itemGroup.id;

            // change the tile map to fit that change
            tilemap.SetTile(worldPos, itemGroup.tile);

            // check if this tile needs a respective tile entity
            GameObject blockEntity  = itemGroup.blockEntity;

            if(blockEntity != null) {

                // create a copy of the block entity needed and add that to the 
                // block entity list
                GameObject createdBlockEntity = Instantiate(blockEntity, new Vector3(worldPos.x + 0.5f, worldPos.y + 0.5f, worldPos.z), new Quaternion(), transform);
                blockEntities.Add(createdBlockEntity);
            }

            return true;
        }
        else
            return false;
    }

    // called when 
    public void setTileToIdArray(int[,] ids)
    {

        // set all of the tileIds in the chunk to the input
        tileIDs = ids;

        // go through each id and set it's respective tile in the tilemap 
        for (int y = 0; y < ids.GetLength(1); y++)
        {
            for (int x = 0; x < ids.GetLength(0); x++)
            {
                // go through each id in the ids array and add it to the tilemap 
                tilemap.SetTile(convertLocalCoordToWorldCoord(new Vector3Int(x, y, 0)), refrenceManager.itemGroups[ids[x, y]].tile);
            }
        }
    }

    // removes a tile from the location asked and returns the 
    // id of that tile
    public int removeTile(Vector3Int worldPos)
    {
        Vector3Int localPos = ChunkHelpers.convertWorldCoordToLocalCoord(worldPos, chunkX);

        // if this tile is in the chunk bounds then return the tileID 
        // at that location, else return the flag - 1
        if (isThisTileInThisChunk(localPos) && tilemap.GetTile(worldPos) != null)
        {

            // remove that tile from the til map
            tilemap.SetTile(worldPos, null);

            // get the id of the tile that is there
            int idOfThatTile = tileIDs[localPos.x, localPos.y];

            // set the id of the tile in out tileID array to 0
            tileIDs[localPos.x, localPos.y] = 0;

            // create an ItemEntity 
            GameObject itemObject = Instantiate(itemEntityPrefab, new Vector3(worldPos.x + 0.5f, worldPos.y + 0.5f, worldPos.z), new Quaternion(0f, 0f, 0f, 0f), transform);
            ItemEntity itemEntity = itemObject.GetComponent<ItemEntity>();

            // set the itemGroup of the itemEntity to the item based on the id
            itemEntity.itemGroup = refrenceManager.itemGroups[idOfThatTile];

            // initzialize the entity	
            itemEntity.init();

            // add the item object to the chunk entities
            itemEntities.Add(itemObject);

            // TODO: if the tile had a block entity then remove that entity from the list

            // return the id of the tile we broke
            return idOfThatTile;
        }
        else
            return -1;
    }

    public Vector3Int convertLocalCoordToWorldCoord(Vector3Int localPos)
    {
        return new Vector3Int(chunkX + localPos.x, localPos.y, localPos.z);
    }

    // used to verify if a tiles postion is within the chunkHeight * chunkWidth
    // tileID array
    public bool isThisTileInThisChunk(Vector3Int tilePosition)
    {

        // checking if the x position is in the range of the tile id array x
        bool xCorrect = tilePosition.x >= 0 && tilePosition.x <= tileIDs.GetLength(0) - 1;

        // checking if the x position is in the range of the tile id array x
        bool yCorrect = tilePosition.y >= 0 && tilePosition.y <= tileIDs.GetLength(1) - 1;

        // only true if both are in range
        return xCorrect && yCorrect;
    }

    public void updateItemEntities()
    {
        // if this chunk is not active then do not update
        if (!gameObject.activeSelf)
            return;

        // go through each item entity in the list
        for (int i = 0; i < itemEntities.Count; i++)
        {
            // if the entity is null or the entity is about to be mergerd
            // with another entity then remove it 
            if (itemEntities[i] == null || itemEntities[i].GetComponent<ItemEntity>().mergedWithOtherEntity)
            {
                itemEntities.RemoveAt(i);
                continue;
            }

            // if the chunk is loaded then set the entity to the load state and 
            // update it 
            if (loaded)
            {
                itemEntities[i].SetActive(true);
                itemEntities[i].GetComponent<ItemEntity>().lookForOtherEntities();
            }
            else
            {
                itemEntities[i].SetActive(false);
            }
        }
    }
}

public struct BiomeRefrence {
    public Biome biome;
    public int refrence;
}
