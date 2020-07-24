using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToWorld : MonoBehaviour
{
    [SerializeField] World world;
	[SerializeField] Inventory inventory;
	[SerializeField] TileManager tileManager;

	private void Start()
	{
		world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
		tileManager = GameObject.FindGameObjectWithTag("TileManager").GetComponent<TileManager>();
	}

	public void removeTile(Vector3Int pos)
	{
		world.getChunk(pos).removeTile(pos);
	}

	public void addTile(Vector3Int pos)
	{
		world.getChunk(pos).addTile(pos, tileManager.tiles[inventory.consumeCurrentItem()]);
	}
}
