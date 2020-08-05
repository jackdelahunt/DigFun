using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save
{

    public SavedData data;

    public string serialize(World world, Inventory inventory, Transform playerTransform)
    {
        data.keys = new int[world.chunks.Keys.Count];
        data.chunks = new Chunk[world.chunks.Values.Count];

        // save the chunk data
        int index = 0;
        foreach (KeyValuePair<int, Chunk> pair in world.chunks)
        {
            data.keys[index] = pair.Key;
            data.chunks[index] = pair.Value;

            index++;
        }

        // save the world data
        data.seed = world.seed;

        // save the inventory data
        data.items = inventory.items;
        data.quantaties = inventory.quantaties;

        // saving the player transform
        data.transform = playerTransform;


        string json = JsonUtility.ToJson(data, true);

        // save the json to a file
        StreamWriter sr = File.CreateText("Saves/SaveData.txt");
        sr.WriteLine(json);
        sr.Close();


        return json;
    }

}

[System.Serializable]
public struct SavedData
{
    // chunk data
    public int[] keys;
    public Chunk[] chunks;

    // world dara
    public int seed;

    // inventory data
    public Item[] items;
    public int[] quantaties;

    // player data
    public Transform transform;

}
