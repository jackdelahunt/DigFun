using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{

    // returns a float form 0-1 based on the inputs
    public static float terrainNoise(int x, int y, int seed, float scale, int offset)
    {
        System.Random prng = new System.Random(seed);
        float seedOffsetX = prng.Next(-100000, 100000);
        float seedOffsetY = prng.Next(-100000, 100000);

        float sampleX = (x * scale) + 0.01f;
        float sampleY = (y * scale) + 0.01f;

        float value = Mathf.PerlinNoise(sampleX + offset + seedOffsetX, sampleY + offset + seedOffsetY);
        Debug.Log(value);
        Mathf.Clamp(value, 0, 1);
        return value;
    }

    // returns true or false if the noise form the inputs is above the threshold
    public static bool caveNoise(int x, int y, int seed, float threshold, float scale, int offset)
    {
        return terrainNoise(x, y, seed, scale, offset) > threshold;
    }

    // returns true if the lode should be generated in the x y position
    public static bool lodeNoise(int x, int y, int seed, Lode lode)
    {
        if (y < lode.minHeight || y > lode.maxHeight)
            return false;

        System.Random prng = new System.Random(seed);
        float seedOffsetX = prng.Next(-100000, 100000);
        float seedOffsetY = prng.Next(-100000, 100000);

        float xf = (x + lode.noiseOffset + 0.1f) * lode.scale + lode.noiseOffset + seedOffsetX;
        float yf = (y + lode.noiseOffset + 0.1f) * lode.scale + lode.noiseOffset + seedOffsetY;

        // sample two points of noise 
        float AB = Mathf.PerlinNoise(xf, yf);
        float BA = Mathf.PerlinNoise(yf, xf);

        // if the sum of the noise values is above the threshold return true
        if ((AB + BA) / 2f > lode.threshold)
            return true;
        else
            return false;
    }
}
