using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Terrain/Biome")]
public class Biome : ScriptableObject
{
    public string biomeName;
    public int terrainHeight;
    public float terrainScale;
    public int surfaceTileId;
    public int subSurfaceTileId;
}
