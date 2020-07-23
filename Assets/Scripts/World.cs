
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	private Dictionary<int, Chunk> chunks;
	public GameObject chunkPrefab;
	public PlayerController player;
	public List<Chunk> currentLoadedChunks;

	private void Start()
	{
		chunks = new Dictionary<int, Chunk>();
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
	}

	private void Update()
	{
		updateChunksToLoad();
	}

	public void updateChunksToLoad()
	{
		for (int i = ChunkData.getRealtiveChunkCoord(player.transform.position) - (ChunkData.renderDisctance * ChunkData.chunkWidth); i <= ChunkData.getRealtiveChunkCoord(player.transform.position) + (ChunkData.renderDisctance * ChunkData.chunkWidth); i += ChunkData.chunkWidth)
		{
			generateChunk(i);
		}
	}

	public void generateChunk(int x)
	{
		Chunk found;
		// check if the chunk already exists
		if (!chunks.TryGetValue(x, out found)) // if it does not generate a new chunk
		{
			Chunk addingChunk = Instantiate(chunkPrefab, gameObject.transform).GetComponent<Chunk>();
			addingChunk.chunkX = x;
			addingChunk.chunkY = 0;
			chunks.Add(addingChunk.chunkX, addingChunk);
			currentLoadedChunks.Add(addingChunk);
		}
	}

	public Chunk getChunk(Vector3 worldPosition)
	{
		Chunk found;
		chunks.TryGetValue(ChunkData.getRealtiveChunkCoord(worldPosition), out found);
		return found;
	}
}