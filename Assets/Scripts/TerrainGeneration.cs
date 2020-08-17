using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainGeneration
{

    public static int[,] generateChunkTiles(int chunkX, int chunkY, int seed, Biome biome)
    {

        int[,] ids = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

        // the height of the cave portion of the chunk
        int caveHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.caveRatio);

        // height of the terrain portion of the chunk
        int terrainHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.terrainRatio);

        // set the bottom layer to bedrock
        for(int x = 0; x < LookUpData.chunkWidth; x++) {
            ids[x, 0] = 8;
        }

        // cave generation, start at y = 1, becasue bedrock is there
        for (int y = 1; y < caveHeight; y++)
        {
            for (int x = 0; x < LookUpData.chunkWidth; x++)
            {
                // check if we are able to place a tile here 
                if (Noise.caveNoise(chunkX + x, chunkY + y, seed, LookUpData.caveGenerationThreshold, LookUpData.caveScale, 0))
                {
                    // set it to stone first and maybe change with a lode after
                    ids[x, y] = biome.caveTileId;

                    // go through the global lodes
                    foreach (Lode globalLode in LodeManager.globalLodes)
                    {
                        // if lode noise returns true, meaning we are in range 
                        // and the noise was above the threshold
                        if (Noise.lodeNoise(chunkX + x, chunkY + y, seed, globalLode))
                        {
                            // set that id in the array to the lode id
                            ids[x, y] = globalLode.tileID;
                        }
                    }

                    // go through the global lodes
                    foreach (Lode biomeLode in biome.biomeLodes)
                    {
                        // if lode noise returns true, meaning we are in range 
                        // and the noise was above the threshold
                        if (Noise.lodeNoise(chunkX + x, chunkY + y, seed, biomeLode))
                        {
                            ids[x, y] = biomeLode.tileID;
                        }
                    }
                }

            }
        }

        // terrain generation
        // going through all of the x values in the terrain
        for (int x = 0; x < LookUpData.chunkWidth; x++)
        {
            // getting the max of the terrain at this x value based on noise
            int terrainHeightAtThisPoint = Mathf.FloorToInt(Noise.terrainNoise(chunkX + x, chunkY, seed, biome.terrainScale, biome.noiseOffset) * terrainHeight);

            // constructing terrain all the way up to the max height 
            for (int y = caveHeight; y <= caveHeight + terrainHeightAtThisPoint; y++)
            {
                ids[x, y] = y <= caveHeight + terrainHeightAtThisPoint - biome.surfaceThickness ? biome.subSurfaceTileId : biome.surfaceTileId;
            }

            // the surface of the terrain plus one
            int whereTreesStart = caveHeight + terrainHeightAtThisPoint + 1;

            // try and generate a tree here
            foreach(TreeGroup treeGroup in biome.trees) {
                if(Noise.caveNoise(chunkX + x, chunkY, seed, treeGroup.threshold, treeGroup.scale, treeGroup.offset)) {
                    int additionalHeightFromMin = Mathf.RoundToInt(Noise.terrainNoise(x, chunkY, seed, treeGroup.scale, treeGroup.offset) * (treeGroup.tree.maxHeight - treeGroup.tree.minHeight));

                    for(int y = whereTreesStart; y <= whereTreesStart + treeGroup.tree.minHeight + additionalHeightFromMin; y++) {
                        ids[x, y] = treeGroup.tree.logId;
                    }
                    
                }
            }
        }

        return ids;
    }

    public static int[,] generateChunkBackground(int[,] chunkIds, Biome biome)
    {
        int[,] backgroundIds = chunkIds;

        // the height of the cave portion of the chunk
        int caveHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.caveRatio);

        // height of the terrain portion of the chunk
        int terrainHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.terrainRatio);

        // go throught each element in the background id array
        for (int y = 0; y < caveHeight; y++)
        {
            for (int x = 0; x < LookUpData.chunkWidth; x++)
            {
                // if there is nothing in this space then add the cave tile
                // nothing in this place means that it is a cave
                if (backgroundIds[x, y] == 0)
                    backgroundIds[x, y] = biome.caveTileId;
            }
        }

        return backgroundIds;
    }
}
