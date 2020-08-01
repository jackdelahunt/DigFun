using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChunkHelpers
{
	public static int getRealtiveChunkCoord(Vector3 pos)
	{
		// returns the chunkX of the chunk that this position located
		return Mathf.FloorToInt(pos.x / LookUpData.chunkWidth) * LookUpData.chunkWidth;
	}

}
