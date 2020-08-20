using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;



[CreateAssetMenu(fileName = "Item", menuName = "DigFun/ItemGroup")]
public class ItemGroup : ScriptableObject
{
    public string itemName;
    public Sprite sprite;
    public int id;
    public int dropItemId;
    public Tile tile;
    public GameObject blockEntity;
    public bool indestructable;
}
