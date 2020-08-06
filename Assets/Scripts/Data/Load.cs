using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Load
{
    public LoadedData data;

    public void deserialize()
    {


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
