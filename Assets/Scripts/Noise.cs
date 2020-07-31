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

	public static bool lodeNoise(int x, int y, float threshold, float scale, int offset) {
		 float xf = (x + offset + 0.1f) * scale;
        float yf = (y + offset + 0.1f) * scale;

        float AB = Mathf.PerlinNoise(xf, yf);
        float BA = Mathf.PerlinNoise(yf, xf);

        if ((AB + BA) / 2f > threshold)
            return true;
        else
            return false;
	}
}
