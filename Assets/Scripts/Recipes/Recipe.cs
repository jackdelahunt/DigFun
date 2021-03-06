﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "DigFun/Recipe")]
public class Recipe : ScriptableObject
{
    public List<StoredItem> materials;
    public StoredItem result;
}
