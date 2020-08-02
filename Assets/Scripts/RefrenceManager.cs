using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefrenceManager : MonoBehaviour
{
    [SerializeField]private Tile[] tiles;
    [SerializeField]private Item[] items;
    [SerializeField]private Biome[] biomes;

    public Item getItem(int index) {
        if(index > 0 && index < items.Length)
            return items[index];
        else 
            return null;
    }

    public Tile getTile(int index) {
        if(index > 0 && index < tiles.Length)
            return tiles[index];
        else 
            return null;
    }

    public Biome[] getBiomes() {
        return biomes;
    }

    public Biome getBiome(int index) {
        return biomes[index];
    }
}
