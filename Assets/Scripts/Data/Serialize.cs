using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Serialize
{

    public SavedData savedData;

   public void serialize(World world) {
       saveWorldData(world);

        string json = JsonUtility.ToJson(savedData, true);
        // save the json to a file
        StreamWriter sr = File.CreateText("Saves/SaveData.txt");
        sr.WriteLine(json);
        sr.Close();

    }

   private void saveWorldData(World world) {

       List<SerializableChunk> chunks = new List<SerializableChunk>();

       foreach(KeyValuePair<int, Chunk> pair in world.chunks) {

            // create the savable chunk from the current chunk
            SerializableChunk addingChunk = new SerializableChunk{};
            addingChunk.tileIDs = pair.Value.tileIDs;
            addingChunk.biomeID = pair.Value.biomeRefrence.refrence;
            addingChunk.chunkX = pair.Value.chunkX;
            addingChunk.chunkY = pair.Value.chunkY;
            addingChunk.seed = pair.Value.seed;

            // add that savable to the chunk list
            chunks.Add(addingChunk);    
       }

       savedData.chunks = chunks.ToArray();
   }
}

[System.Serializable]
public struct SavedData {
    public SerializableChunk[] chunks;
}

[System.Serializable]
public struct SerializableChunk {
    public int[] tileIDs;
    public int biomeID;
    public int chunkX;
    public int chunkY;
    public int seed;
    public SerializableChunkBackground background;


}

[System.Serializable]
public struct SerializableChunkBackground {
    public int[,] tileIDs;
}



