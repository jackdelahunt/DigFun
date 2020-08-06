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
        data.chunkTileData = new int[data.keys.Length, LookUpData.chunkWidth, LookUpData.chunkHeight];

        // save the chunk data
        int index = 0;
        foreach (KeyValuePair<int, Chunk> pair in world.chunks)
        {
            for (int y = 0; y < pair.Value.tileIDs.GetLength(1); y++)
            {
                for (int x = 0; x < pair.Value.tileIDs.GetLength(0); x++)
                {
                    data.chunkTileData[index, x, y] = pair.Value.tileIDs[x, y];
                }
            }
            data.keys[index] = pair.Key;

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
    public int[,,] chunkTileData;

    // world dara
    public int seed;

    // inventory data
    public Item[] items;
    public int[] quantaties;

    // player data
    public Transform transform;

}
