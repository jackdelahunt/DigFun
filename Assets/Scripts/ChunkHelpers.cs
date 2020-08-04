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

    // converts a vector3 that represents a tile in this chunk from it's world coords
    // to local co-ordinates for this chunk
    public static Vector3Int convertWorldCoordToLocalCoord(Vector3Int worldPos, int chunkX)
    {
        return new Vector3Int(Mathf.Abs(Mathf.FloorToInt(chunkX - worldPos.x)), worldPos.y, 0);
    }

}
