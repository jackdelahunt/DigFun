using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToWorld : MonoBehaviour
{
    [SerializeField] World world;
	[SerializeField] Inventory inventory;
	[SerializeField] RefrenceManager refrenceManager;

	private void Start()
	{
		world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
		refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
	}

	// tells the world object to get the chunk that we are in and reomve the tile
	public void removeTile(Vector3Int pos)
	{
		int tileID = world.getChunk(pos).removeTile(pos);
		inventory.addItem(refrenceManager.getItem(tileID));
	}

	public void addTile(Vector3Int pos)
	{
		// get the tile id of the item in the inventory
		int IDOfItemSelectedInInventory = inventory.getIDOfCurrentItem();

		// if it is the flag -1 then do not add this block
		if (IDOfItemSelectedInInventory <= -1)
			return;

		// if this block was added then decrease the count in the inventory  by one
		// this will not happen if there is already a block in that area
		if (world.getChunk(pos).addTile(pos, refrenceManager.getTile(IDOfItemSelectedInInventory), IDOfItemSelectedInInventory))
			inventory.decrementCurrentItem();
	}

	public bool addItemToInventory(Item item)
	{
		return inventory.addItem(item);
	}

	public void changeSelectedItem(int amount) {
		inventory.changeSelectedItem(amount);
	}
}
