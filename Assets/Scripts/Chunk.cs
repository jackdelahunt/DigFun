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
		if (pos.y < ChunkData.chunkHeight - 5)
		{
			tilemap.SetTile(pos, tiles[2]);
			return;
		}

		if (tilemap.GetTile(pos + new Vector3Int(0, 1, 0)) == null)
			tilemap.SetTile(pos, tiles[1]);
		else
			tilemap.SetTile(pos, tiles[0]);

		if(tilemap.GetTile(pos + new Vector3Int(0, -1, 0)) != null)
			tilemap.SetTile(pos + new Vector3Int(0, -1, 0), tiles[0]);
	}

    public void removeTile(Vector3Int pos) 
	{
        tilemap.SetTile(pos, null);

		if (tilemap.GetTile(pos + new Vector3Int(0, -1, 0)) != null)
			tilemap.SetTile(pos + new Vector3Int(0, -1, 0), tiles[1]);
	}

	public void generateStartTerrain()
	{
		for (int y = chunkY; y < chunkY + ChunkData.chunkHeight; y++) {
			for (int x = chunkX; x < chunkX + ChunkData.chunkWidth; x++)
			{
				addTile(new Vector3Int(x, y, 0), null);
			}
		}
	}
}
