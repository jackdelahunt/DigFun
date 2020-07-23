using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chunk : MonoBehaviour
{
    private Tilemap tilemap;
	public List<Tile> tiles;

	public int chunkX;
	public int chunkY;

	private void Start()
	{
		tilemap = GetComponent<Tilemap>();
		generateStartTerrain();
	}

	public void addTile(Vector3Int pos, Tile tile)
	{
		if (tilemap.GetTile(pos) == null)
		{
			if(tile == null)
				tilemap.SetTile(pos, tiles[0]);
			else
				tilemap.SetTile(pos, tile);
		}
	}

    public void removeTile(Vector3Int pos) 
	{
        tilemap.SetTile(pos, null);
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
					addTile(new Vector3Int(x, y, 0), tiles[1]);
				else if (y > ChunkData.chunkHeight - 5)
					addTile(new Vector3Int(x, y, 0), tiles[0]);
				else
					addTile(new Vector3Int(x, y, 0), tiles[2]);

			}
		}

		// generate the above terrain
		for(int x = chunkX; x < chunkX + ChunkData.chunkWidth; x++)
		{
			float noise = Noise.terrainNoise(x, terrainHeight, scale, 100);
			int heightOfTerrainHere = Mathf.FloorToInt(terrainHeight * noise);

			for (int y = 0; y < heightOfTerrainHere; y++)
			{
				addTile(new Vector3Int(x, ChunkData.chunkHeight - terrainHeight + y, 0), tiles[0]);
			}
		}
	}
}
