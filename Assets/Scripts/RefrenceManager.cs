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

    public Recipe[] getRecipes() {
        return recipes;
    }

    public Recipe getRecipe(int index)
    {
        return recipes[index];
    }
}