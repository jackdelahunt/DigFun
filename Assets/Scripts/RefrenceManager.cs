using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefrenceManager : MonoBehaviour
{
    [SerializeField] public ItemGroup[] itemGroups;
    [SerializeField] private Biome[] biomes;
    [SerializeField] private Recipe[] recipes;

    public Biome[] getBiomes()
    {
        return biomes;
    }

    public Biome getBiome(int index)
    {
        return biomes[index];
    }

    public Recipe getRecipe(int index)
    {
        return recipes[index];
    }
}

[System.Serializable]
public struct blockEntityRefrence {
    public Item item;
    public GameObject blockEntity;
}

[System.Serializable]
public struct ItemGroup {
    public int id;
    public Item item;
    public Tile tile;
    public GameObject blockEntity;
    public bool indestructable;
}