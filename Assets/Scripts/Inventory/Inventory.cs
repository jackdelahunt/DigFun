using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

	// an array of all the items that the inventory stores
	public Item[] items;

	// the respective quantaties of each item in the inventory
    public int[] quantaties;

	// the ui that this inventory needs to use to update the hotbar
	public UIInventory uIInventory;

	// the current selected item in the inventory
	public int selectedItem;

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

	// change the selected
	public void changeSelectedItem(int amount) {
		
		// change the selected item
		selectedItem -= amount;

		// if the selected item is out of the item array bounds then
		// wrap it to the other end of the array
		if(selectedItem < 0)
			selectedItem = items.Length - 1;
		else if(selectedItem > items.Length - 1)
			selectedItem = 0;

		// once this has changed tell the inventory ui to update
		uIInventory.updateItemSelect();
	}

	// decrease the current selected item by 1
	// if there is 0 of that item remove it     
	public void decrementCurrentItem()
	{
		quantaties[selectedItem] -= 1;

		// if there is none of the item left net remove it from the items array
		if (quantaties[selectedItem] <= 0)
			items[selectedItem] = null;

		// once this has changed tell the inventory ui to update
		uIInventory.updateUIContents();
	}

	public bool addItem(Item item)
	{
		// if the item we are trying to add is null then return false
		if(item == null)
			return false;

		for(int i = 0; i < items.Length; i++)
		{
			// if the slot is empty then set the item to this
			if (items[i] == null)
			{
				// set the item
				items[i] = item;

				// this is the first item so set the quantatie to 1
				quantaties[i] = 1;

				// update the UI
				uIInventory.updateUIContents();

				// return that we added the item
				return true;
			} // if the item we are adding is in this slot and it is not full add it, else just continue the loop
			else if (items[i].tileID == item.tileID && quantaties[i] < LookUpData.maxNumberOfItemsPerSlot)
			{
				// add the amount
				quantaties[i] += 1;

				// update the UI
				uIInventory.updateUIContents();

				// return that we added the item
				return true;
			}
		}

		// if the loop ended and we have nod added the item then
		// return false as we did not add it 
		return false;
	}
}
 