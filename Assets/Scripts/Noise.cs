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

}
