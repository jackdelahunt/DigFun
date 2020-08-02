
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour
{
	// a dictionary that stores the chunkX as the key and
	// the chunk object as the value
	private Dictionary<int, Chunk> chunks;
	public GameObject chunkPrefab;
	public GameObject player;

	public RefrenceManager refrenceManager;

	// a list of all the loaded chunks in the scene
	public List<Chunk> currentLoadedChunks;

	// the list of refremces to each biome with weights accounted for
	public int[] weightedBiomeList;

	private void Start()
	{
		chunks = new Dictionary<int, Chunk>();
		player = GameObject.FindGameObjectWithTag("Player");
		refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();

		weightedBiomeList = generateWeightedBiomeList(refrenceManager.getBiomes());

		// call the chunkloader every second
		InvokeRepeating("updateChunksToLoad", 0f, 1f);
	}


	public void updateChunksToLoad()
	{
		//create a blank chunk list
		List<Chunk> newChunks = new List<Chunk>();
		for (int i = ChunkHelpers.getRealtiveChunkCoord(player.transform.position) - (LookUpData.renderDisctance * LookUpData.chunkWidth); i <= ChunkHelpers.getRealtiveChunkCoord(player.transform.position) + (LookUpData.renderDisctance * LookUpData.chunkWidth); i += LookUpData.chunkWidth)
		{
			// get the chunk at this position
			Chunk newLoadedChunk = generateChunk(i);

			// set it to active if it is not
			newLoadedChunk.gameObject.SetActive(true);

			// add this chunk to the newly loaded chunks
			newChunks.Add(newLoadedChunk);
		}

		// go through all the loaded chunks in the last frame
		for(int i = 0; i < currentLoadedChunks.Count; i++)
		{
			// if the chunk is not in the new list of chunks to
			// load then set active to false (unloading)
			if (!newChunks.Contains(currentLoadedChunks[i]))
				currentLoadedChunks[i].gameObject.SetActive(false);
		}

		// set the current loaded chunks to the new list of chunks
		currentLoadedChunks = newChunks;
	}

	public Chunk generateChunk(int x)
	{
		// create a blank template for this chunk
		Chunk found;
		// check if the chunk already exists if it does then set found to it
		// if the chunk does not exist then generate a new chunk
		if (!chunks.TryGetValue(x, out found)) 
		{
			found = Instantiate(chunkPrefab, gameObject.transform).GetComponent<Chunk>();
			found.chunkX = x;
			found.chunkY = 0;
			found.biome = getBiomeAtThisChunk(found.chunkX, found.chunkY);
			found.init();
			

			// add this new chunk the chunk dictionary
			chunks.Add(found.chunkX, found);
		}

		// return either the newly generated chunk or the already created chunk
		return found;
	}

	public Chunk getChunk(Vector3 worldPosition)
	{
		Chunk found;
		
		// get what chunk is at this x position, if there is none then it is null
		chunks.TryGetValue(ChunkHelpers.getRealtiveChunkCoord(worldPosition), out found);
		return found;
	}

	public int[] generateWeightedBiomeList(Biome[] biomes) {
		List<int> weightedList = new List<int>(); 

		// go through each biome in the list
		for(int i = 0; i < biomes.Length; i++) {

			// for each biome add the the index to the list weight number of times
			for(int w = 0; w < biomes[i].weight; w++) {
				weightedList.Add(i);
			}
		}

		// convert it to an array and return
		return weightedList.ToArray();
	}

	public Biome getBiomeAtThisChunk(int chunkX, int chunkY) {

		// get the noise value at this point and rount it to an int between 0 and the length of the biome list
		// goto the weighted biome list and check which biome this number is and get that biome in the refrence manager
		int noise = Mathf.RoundToInt(Noise.terrainNoise(chunkX, chunkY, LookUpData.biomeGenerationScale, 0) * (weightedBiomeList.Length - 1));
		print(noise);
		return refrenceManager.getBiome( weightedBiomeList[noise]);
	}
}