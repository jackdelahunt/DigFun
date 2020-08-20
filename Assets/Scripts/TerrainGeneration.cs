using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainGeneration
{

    public static int[,] generateChunkTiles(int chunkX, int chunkY, int seed, Biome biome, out int[,] background)
    {

        int[,] ids = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];
        background = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

        // the height of the cave portion of the chunk
        int caveHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.caveRatio);

        // height of the terrain portion of the chunk
        int terrainHeight = Mathf.RoundToInt(LookUpData.chunkHeight * biome.terrainRatio);

        // set the bottom layer to bedrock
        for(int x = 0; x < LookUpData.chunkWidth; x++) {
            ids[x, 0] = 8;
            background[x, 0] = 8;
        }

        // cave generation, start at y = 1, becasue bedrock is there
        for (int y = 1; y < caveHeight; y++)
        {
            for (int x = 0; x < LookUpData.chunkWidth; x++)
            {
                // set each tile in the chunk in the cave level to stone
                // even if there is a block there or not
                background[x, y] = biome.caveTileId;

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
                            background[x, y] = globalLode.tileID;
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
                            background[x, y] = biomeLode.tileID;
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
                int idOfThisTile = y <= caveHeight + terrainHeightAtThisPoint - biome.surfaceThickness ? biome.subSurfaceTileId : biome.surfaceTileId;
                ids[x, y] = idOfThisTile;
                background[x, y] = idOfThisTile;
            }

            // so we do not spawn a tree on a chunk border 
            if( x > 0 && x < ids.GetLength(0) - 1) {
                // the surface of the terrain plus one
                int whereTreesStart = caveHeight + terrainHeightAtThisPoint + 1;

                // try and generate a tree here
                foreach(TreeGroup treeGroup in biome.trees) {

                    // if a treee is spawnable here
                    if(Noise.caveNoise(chunkX + x, chunkY, seed, treeGroup.threshold, treeGroup.scale, treeGroup.offset)) {

                        // get the height from the start of the wood
                        int additionalHeightFromMin = Noise.treeHeightNoise(x, chunkY, seed, treeGroup);

                        // from where trees start to the min height plus additional
                        for(int y = whereTreesStart; y <= whereTreesStart + treeGroup.tree.minHeight + additionalHeightFromMin; y++) {
                            
                            // if we are above the min height add a leaf blcok as the trunk and leaf block to the side
                            // else just add the log block
                            if(y > whereTreesStart + treeGroup.tree.minHeight) {
                                ids[x, y] = treeGroup.tree.leafId;
                                ids[x + 1, y] = treeGroup.tree.leafId;
                                ids[x - 1, y] = treeGroup.tree.leafId;
                            } else {
                                ids[x, y] = treeGroup.tree.logId;
                            }
                        }

                        // add a leaf at the top to cap the tree
                        ids[x, whereTreesStart + treeGroup.tree.minHeight + additionalHeightFromMin + 1] = treeGroup.tree.leafId;
                    }
                }
            }
        }

        return ids;
    }

}
