using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Save
{

    public SavedData data;

    public string serialize(World world, Inventory inventory, Transform playerTransform)
    {
        return "Eat Ass";
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
