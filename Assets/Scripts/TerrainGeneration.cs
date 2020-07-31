using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TerrainGeneration
{

    public static int[,] generateChunkTiles(int chunkX, int chunkY, Biome biome) {

		int[,] ids = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

		// chunkHeight - terrainHeight = undergroundHeight
		for(int y = 0; y < ids.GetLength(1); y++) {
            for(int x = 0; x < ids.GetLength(0); x++) {
                
				// cavePass
				if(y < LookUpData.chunkHeight - biome.terrainHeight) {
					if(Noise.caveNoise(chunkX + x, chunkY + y, 0.4f, 0.2f, 0))
						ids[x, y] = 3;
				}

                foreach(Lode lode in LodeManager.globalLodes) {
                    if(y >= lode.minHeight && y <= lode.maxHeight && y < LookUpData.chunkHeight - biome.terrainHeight)
                        if(Noise.lodeNoise(chunkX + x, chunkY + y, lode.threshold, lode.scale, lode.noiseOffset)) {
                            ids[x, y] = lode.blockID;
                        }
                }	

                foreach(Lode lode in biome.biomeLodes) {
                    if(y >= lode.minHeight && y <= lode.maxHeight && y < LookUpData.chunkHeight - biome.terrainHeight)
                        if(Noise.lodeNoise(chunkX + x, chunkY + y, lode.threshold, lode.scale, lode.noiseOffset)) {
                            ids[x, y] = lode.blockID;
                        }
                }   			
            }
        }

		// terrain pass
		for(int x = 0; x < LookUpData.chunkWidth; x++) {

			int heightAtThisPoint = Mathf.FloorToInt(biome.terrainHeight * Noise.terrainNoise(chunkX + x, chunkY, biome.terrainScale, biome.noiseOffset));
			int highestPointAtThisPos =  LookUpData.chunkHeight - biome.terrainHeight + heightAtThisPoint;
			for(int y = LookUpData.chunkHeight - biome.terrainHeight; y <= highestPointAtThisPos; y++) {

				if(y == highestPointAtThisPos)
					ids[x, y] = biome.surfaceTileId;
				else
					ids[x,y] = biome.subSurfaceTileId;
			}
		}
		return ids;
	}
}
