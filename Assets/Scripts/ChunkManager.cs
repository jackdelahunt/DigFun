using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
	private Chunk[] chunks = new Chunk[10];
	public GameObject chunkPrefab;

	private void Start()
	{
		for (int i = 0; i < chunks.Length; i++) {
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
}
