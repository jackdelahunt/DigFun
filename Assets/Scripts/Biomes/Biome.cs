using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Biome", menuName = "Terrain/Biome")]
public class Biome : ScriptableObject
{
    // the name of this biome
    public string biomeName;

    // how often does this biome generate
    public int weight;

    // proportion of of the chunk the terrain takes up
    public float terrainRatio;

    // proportion of of the chunk the cave takes up
    public float caveRatio;

    // scale for the noise function
    public float terrainScale;

    // how many block does the surface tile take up
    public int surfaceThickness;

    // the tile id of the surface block
    public int surfaceTileId;

    // the tile id of the block below the surface in the terrain
    public int subSurfaceTileId;

    // the tile id for the cave tile
    public int caveTileId;

    // offset for the noise function
    public int noiseOffset;

    // the lodes that are unique to this biome
    public Lode[] biomeLodes;
}

[System.Serializable]
public class Lode
{

    // the name of the lode
    public string lodeName;

    // the tile id of the tile that this lode represents
    public int tileID;

    // the minimum height this lode can be generated
    public int minHeight;

    // the maximum height this lode can be generated at once
    // it is below the start of the terrain
    public int maxHeight;

    // the noise scale for generation
    public float scale;

    // the threshold value the noise function needs to reach 
    // for the lode to generate
    public float threshold;

    // offset for the noise function
    public int noiseOffset;

    public Lode(string lodeName, int tileID, int minHeight, int maxHeight, float scale, float threshold, int noiseOffset)
    {
        this.lodeName = lodeName;
        this.tileID = tileID;
        this.minHeight = minHeight;
        this.maxHeight = maxHeight;
        this.scale = scale;
        this.threshold = threshold;
        this.noiseOffset = noiseOffset;
    }
}
