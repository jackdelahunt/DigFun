using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Terrain/Item")]
public class Item : ScriptableObject
{
    // the name of the item
    public string itemName;

    // the tileID of the tile that item represents
    public int tileID;

    // the sprite that will show in the inventory
    public Sprite sprite;
}
