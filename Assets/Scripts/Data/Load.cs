using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load
{
    public LoadedData data;

    public LoadedData deserialize()
    {

        SavedData savedData = JsonUtility.FromJson<SavedData>(File.ReadAllText("Saves/SaveData.txt"));

        // copy saved data that is the same
        data.seed = savedData.seed;
        data.items = savedData.items;
        data.quantaties = savedData.quantaties;
        data.transform = savedData.transform;

        // create a dictionary and construct what would have been a 
        // copy with the saved data
        Dictionary<int, Chunk> chunks = new Dictionary<int, Chunk>();
        Chunk[] savedChunks = savedData.chunks;

        for (int i = 0; i < savedData.keys.Length; i++)
        {
            chunks.Add(savedData.keys[i], savedData.chunks[i]);
        }

        data.chunks = chunks;

        return data;
    }
}

public struct LoadedData
{

    // chunk data
    public Dictionary<int, Chunk> chunks;

    // world data
    public int seed;

    // inventory data
    public Item[] items;
    public int[] quantaties;

    // player data
    public Transform transform;
}
