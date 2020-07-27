using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
	private RefrenceManager refrenceManager;
	private int[,] tileIDs;

	public int chunkX;
	public int chunkY;

	private void Start()
	{
		tileIDs = new int[ChunkData.chunkWidth, ChunkData.chunkHeight];

		tilemap = GetComponent<Tilemap>();
		refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
		generateStartTerrain();
	}

	// add a tile to a position in this chunk
	public bool addTile(Vector3Int pos, Tile tile, int itemRefrence)
	{
		// if there is no tile in this area then add the new one else return false
		if (tilemap.GetTile(pos) == null)
		{
			tilemap.SetTile(pos, tile);
			tileIDs[pos.x, pos.y] = itemRefrence;
			return true;
		}
		else
			return false;
	}

	// removes a tile from the location asked and returns the 
	// id of that tile
    public int removeTile(Vector3Int pos) 
	{
        tilemap.SetTile(pos, null);
		int idOfThatTile = tileIDs[pos.x, pos.y];
		tileIDs[pos.x, pos.y] = 0;
		return idOfThatTile;
	}

	public void generateStartTerrain()
	{
		// these values will be set by the biome in the future
		int terrainHeight = 20;
		float scale = 0.1f;
		// generate the cave structure
		// chunkHeight - terrainHeight = undergroundHeight??
		for (int y = chunkY; y < chunkY + ChunkData.chunkHeight - terrainHeight; y++) {
			for (int x = chunkX; x < chunkX + ChunkData.chunkWidth; x++)
			{
				if (!Noise.caveNoise(x, y, 0.4f, 0.15f, 0))
					continue;

				if (y == ChunkData.chunkHeight - 1) 
					addTile(new Vector3Int(x, y, 0), refrenceManager.tiles[2] , 2);
				else if (y > ChunkData.chunkHeight - 5)
					addTile(new Vector3Int(x, y, 0), refrenceManager.tiles[1], 1);
				else
					addTile(new Vector3Int(x, y, 0), refrenceManager.tiles[3], 3);

			}
		}

		// generate the above terrain
		for(int x = chunkX; x < chunkX + ChunkData.chunkWidth; x++)
		{
			float noise = Noise.terrainNoise(x, terrainHeight, scale, 100);
			int heightOfTerrainHere = Mathf.FloorToInt(terrainHeight * noise);

			for (int y = 0; y < heightOfTerrainHere; y++)
			{
				addTile(new Vector3Int(x, ChunkData.chunkHeight - terrainHeight + y, 0), refrenceManager.tiles[1], 1);
			}
		}
	}
}
