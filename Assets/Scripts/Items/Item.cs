﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Terrain/Item")]
public class Item : ScriptableObject
{
    public string itemName;
    public int tileID;
    public Sprite sprite;
}
    