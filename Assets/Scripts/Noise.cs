using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    
	// returns a float form 0-1 based on the inputs
    public static float terrainNoise(int x, int y, float scale, int offset)
	{
		return Mathf.PerlinNoise((x * scale) + offset, (y * scale) + offset);
	}

	// returns true or false if the noise form the inputs is above the threshold
	public static bool caveNoise(int x, int y, float threshold, float scale, int offset)
	{
		return terrainNoise(x, y, scale, offset) > threshold;
	}

	// returns true if the lode should be generated in the x y position
	public static bool lodeNoise(int x, int y, float threshold, float scale, int offset) {

		float xf = (x + offset + 0.1f) * scale;
        float yf = (y + offset + 0.1f) * scale;

		// sample two points of noise 
        float AB = Mathf.PerlinNoise(xf, yf);
        float BA = Mathf.PerlinNoise(yf, xf);

		// if the sum of the noise values is above the threshold return true
        if ((AB + BA) / 2f > threshold)
            return true;
        else
            return false;
	}
}
