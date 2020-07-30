using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class RefrenceManager : MonoBehaviour
{
    [SerializeField]private List<Tile> tiles;
    [SerializeField]private List<Item> items;

    public Item getItem(int index) {
        if(index > 0 && index < items.Count)
            return items[index];
        else 
            return null;
    }

    public Tile getTile(int index) {
        if(index > 0 && index < tiles.Count)
            return tiles[index];
        else 
            return null;
    }
}
