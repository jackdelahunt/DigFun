using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Item[] items;
    public int[] quantaties;
	public UIInventory uIInventory;
    [SerializeField] private int selectedItem;

	private void Start()
	{
		uIInventory = FindObjectOfType<UIInventory>();

		selectedItem = 0;
		quantaties = new int[items.Length];
	}

	// get the tileID of the current item selected in the inventory
	public int getIDOfCurrentItem()
	{
		// if there is a item in the slot then return that id
		// else return the flag -1
		return items[selectedItem] == null ? -1 : items[selectedItem].tileID;
	}

	// decrease the current selected item by 1
	// if there is 0 of that item remove it     
	public void decrementCurrentItem()
	{
		quantaties[selectedItem] -= 1;
		if (quantaties[selectedItem] <= 0)
			items[selectedItem] = null;

		uIInventory.updateUIContents();
	}

	public bool addItem(Item item)
	{
		// if the item we are trying to add is null then return false
		if(item == null)
			return false;

		for(int i = 0; i < items.Length; i++)
		{
			// if the slot is empty then set the item to this, and it's quantaty to 1
			if (items[i] == null)
			{
				items[i] = item;
				quantaties[i] = 1;
				uIInventory.updateUIContents();
				return true;
			} else if (items[i].tileID == item.tileID)
			{
				// if this slot has reached the max items go to the next slot
				if(quantaties[i] >= LookUpData.maxNumberOfItemsPerSlot)
					continue;

				quantaties[i] += 1;
				uIInventory.updateUIContents();
				return true;
			}
		}
		return false;
	}
}
 