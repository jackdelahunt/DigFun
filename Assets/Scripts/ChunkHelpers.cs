using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ChunkHelpers
{
	public static int getRealtiveChunkCoord(Vector3 pos)
	{
		return Mathf.FloorToInt(pos.x / LookUpData.chunkWidth) * LookUpData.chunkWidth;
	}

}
