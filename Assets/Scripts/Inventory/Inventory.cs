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
    public void changeSelectedItem(int amount)
    {

        // change the selected item
        selectedItem -= amount;

        // if the selected item is out of the item array bounds then
        // wrap it to the other end of the array
        if (selectedItem < 0)
            selectedItem = items.Length - 1;
        else if (selectedItem > items.Length - 1)
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

    public int addItem(Item item, int addingAmount)
    {
        // if the item we are trying to add is null then return false
        if (item == null)
            return -1;

        // going through looking for same items
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
                continue;

            // go through each item that is the same as ours and add the 
            // amount we can to it, after if we still have some left then continue
            if (items[i].tileID == item.tileID && quantaties[i] < LookUpData.maxNumberOfItemsPerSlot)
            {
                // if the current amount plus the adding amount is more than max
                // then set the amount to add as the number it would take to fill the slot
                // if the amount after the adding is less then max then use the current item amount
                int amountAddingToThisSlot = quantaties[i] + addingAmount > LookUpData.maxNumberOfItemsPerSlot ? LookUpData.maxNumberOfItemsPerSlot - quantaties[i] : addingAmount;

                // add what we we have space for 
                quantaties[i] += amountAddingToThisSlot;

                // then minus the remainder if any
                addingAmount -= amountAddingToThisSlot;

                // if there is none left to add then update the ui and return the flag
                if (addingAmount == 0)
                {
                    uIInventory.updateUIContents();
                    return -1;
                }
            }
        }

        // going through looking for empty slots
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null)
            {
                int amountToAdd = addingAmount > LookUpData.maxNumberOfItemsPerSlot ? LookUpData.maxNumberOfItemsPerSlot : addingAmount;
                items[i] = item;
                quantaties[i] = amountToAdd;
                addingAmount -= amountToAdd;

                if (addingAmount == 0)
                {
                    uIInventory.updateUIContents();
                    return -1;
                }
            }
        }


        uIInventory.updateUIContents();
        // if the loop ended and we have nod added the item then
        // return false as we did not add it 
        return addingAmount;
    }
}
