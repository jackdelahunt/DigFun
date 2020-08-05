using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Debug : MonoBehaviour
{
    public World world;

    public TMP_Text playerX;
    public TMP_Text playerY;
    public TMP_Text chunk;
    public TMP_Text totalChunks;
    public TMP_Text biome;
    public TMP_Text seed;
    public TMP_Text FPS;



    public void Awake()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
    }

    public void Update()
    {
        // get the chunkX of the chunk that the player is in
        int playerChunkX = ChunkHelpers.getRealtiveChunkCoord(world.player.transform.position);

        // get the chunk variable of the chunkX
        Chunk playerChunk;
        world.chunks.TryGetValue(playerChunkX, out playerChunk);

        // set all the fields in the debug screen to the correct values
        playerX.SetText("" + Mathf.RoundToInt(world.player.transform.position.x));
        playerY.SetText("" + Mathf.RoundToInt(world.player.transform.position.y));
        chunk.SetText("" + playerChunk.chunkX);
        totalChunks.SetText("" + world.chunks.Count);
        biome.SetText("" + playerChunk.biome.biomeName);
        seed.SetText("" + world.seed);
        FPS.SetText("" + Mathf.RoundToInt(1 / Time.deltaTime));
    }


}
