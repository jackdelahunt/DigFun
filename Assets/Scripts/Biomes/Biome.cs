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
    public int noiseOffset;
    public Lode[] biomeLodes;
}

[System.Serializable]
public class Lode {
    public string lodeName;
    public int blockID;
    public int minHeight;
    public int maxHeight;
    public float scale;
    public float threshold;
    public int noiseOffset;

    public Lode(string lodeName, int blockID, int minHeight, int maxHeight, float scale, float threshold, int noiseOffset) {
        this.lodeName = lodeName;
        this.blockID = blockID;
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.scale = scale;
        this.threshold = threshold;
        this.noiseOffset = noiseOffset;
    }
}
