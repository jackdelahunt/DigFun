using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
	private Chunk[] chunks = new Chunk[10];
	public GameObject chunkPrefab;
	public PlayerController player;

	private void Start()
	{

		player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
		generateBaseTerrain();
		InvokeRepeating("chunksForLoading", 0f, 1f);
		
	}

	public void generateBaseTerrain() {
		for (int i = 0; i < chunks.Length; i++)
		{
			Chunk addingChunk = Instantiate(chunkPrefab, gameObject.transform).GetComponent<Chunk>();
			addingChunk.chunkX = i * ChunkData.chunkWidth;
			addingChunk.chunkY = 0;
			chunks[i] = addingChunk;
		}
	}

	public Chunk getChunk(Vector3 worldPosition) {
		int chunkX = Mathf.FloorToInt(worldPosition.x / 16) * ChunkData.chunkWidth;
		for (int i = 0; i < chunks.Length; i++)
			if (chunks[i].chunkX == chunkX)
				return chunks[i];

		return null;
	}

	public void chunksForLoading()
	{
		for (int i = 0; i < chunks.Length; i++)
			if (Mathf.Abs(player.transform.position.x - chunks[i].chunkX) < 32)
				chunks[i].gameObject.SetActive(true);
			else
				chunks[i].gameObject.SetActive(false);
	}
}
