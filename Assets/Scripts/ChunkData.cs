using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChunkData
{

	public static readonly int chunkHeight = 64;
	public static readonly int chunkWidth = 16;
	public static readonly int renderDisctance = 2;

	public static int getRealtiveChunkCoord(Vector3 pos)
	{
		return Mathf.FloorToInt(pos.x / chunkWidth) * chunkWidth;
	}

}
