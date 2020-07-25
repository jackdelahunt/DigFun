using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
	public Item[] items;
    public int[] quantaties;

    [SerializeField] private int selectedItem;

	private void Start()
	{
		selectedItem = 0;
		quantaties = new int[items.Length];
		for (int i = 0; i < quantaties.Length; i++)
			quantaties[i] = 10;
	}

	// get the tileID of the current item selected in the inventory
	public int getIDOfCurrentItem()
	{
		// if there is a item in the slot then return that id
		// else return the flag -1
		if (items[selectedItem] != null)
			return items[selectedItem].tileID;
		else
			return -1;
	}

	// decrease the current selected item by 1
	// if there is 0 of that item remove it
	public void decrementCurrentItem()
	{
		quantaties[selectedItem] -= 1;
		if (quantaties[selectedItem] <= 0)
			items[selectedItem] = null;
	}
}
 