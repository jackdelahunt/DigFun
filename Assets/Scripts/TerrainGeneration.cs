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

					// if we can spawn a block here
					if(Noise.caveNoise(chunkX + x, chunkY + y, 0.4f, 0.2f, 0)) {

						// first generate stone an then add lodes later
						ids[x, y] = 3;

						// first go through each global lode and try spawn a block
						foreach(Lode lode in LodeManager.globalLodes) {

							// if we are in the lodes range
                    		if(y >= lode.minHeight && y <= lode.maxHeight && y < LookUpData.chunkHeight - biome.terrainHeight) {
								
								// if this lode can spawn spawn it and move to the next tile
                        		if(Noise.lodeNoise(chunkX + x, chunkY + y, lode.threshold, lode.scale, lode.noiseOffset)) {
                            		ids[x, y] = lode.blockID;
									break;
								}
                        	}
                		}

						// go through the biomes specific lodes next
						foreach(Lode lode in biome.biomeLodes) {

							// if we are in the lodes range
                    		if(y >= lode.minHeight && y <= lode.maxHeight && y < LookUpData.chunkHeight - biome.terrainHeight) {

								// if this lode can spawn here
                        		if(Noise.lodeNoise(chunkX + x, chunkY + y, lode.threshold, lode.scale, lode.noiseOffset)) {

									// set the tile to the id and go to next tile
                            		ids[x, y] = lode.blockID;
									break;  
								}
                			}   
						}
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
