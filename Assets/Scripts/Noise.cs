using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    
    public static float terrainNoise(int x, int y, float scale, int offset)
	{
		return Mathf.PerlinNoise((x * scale) + offset, (y * scale) + offset);
	}

	public static bool caveNoise(int x, int y, float threshold, float scale, int offset)
	{
		return Mathf.PerlinNoise((x * scale) + offset, (y * scale) + offset) > threshold;
	}

	public static int[,] generateChunkTiles(int chunkX, int chunkY, Biome biome) {

		int[,] ids = new int[LookUpData.chunkWidth, LookUpData.chunkHeight];

		// chunkHeight - terrainHeight = undergroundHeight
		for(int y = 0; y < ids.GetLength(1); y++) {
            for(int x = 0; x < ids.GetLength(0); x++) {
                
				// cavePass
				if(y < LookUpData.chunkHeight - biome.terrainHeight) {
					if(caveNoise(chunkX + x, chunkY + y, 0.4f, 0.2f, 0))
						ids[x, y] = 3;
					continue;
				}				
            }
        }

		// terrain pass
		for(int x = 0; x < LookUpData.chunkWidth; x++) {

			int heightAtThisPoint = Mathf.FloorToInt(biome.terrainHeight * terrainNoise(chunkX + x, chunkY, biome.terrainScale, 0));
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
