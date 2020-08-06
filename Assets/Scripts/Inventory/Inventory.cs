using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public StoredItem[] storedItems;

    // the ui that this inventory needs to use to update the hotbar
    public UIInventory uIInventory;

    // the current selected item in the inventory
    public int selectedItem;

    private void Start()
    {
        uIInventory = FindObjectOfType<UIInventory>();

        selectedItem = 0;

        // 9 is the amount items this iventory can hold
        storedItems = new StoredItem[9];
    }

    // get the tileID of the current item selected in the inventory
    public int getIDOfCurrentItem()
    {
        // if there is a item in the slot then return that id
        // else return the flag -1
        return storedItems[selectedItem].item == null ? -1 : storedItems[selectedItem].item.tileID;
    }

    // change the selected
    public void changeSelectedItem(int amount)
    {

        // change the selected item
        selectedItem -= amount;

        // if the selected item is out of the item array bounds then
        // wrap it to the other end of the array
        if (selectedItem < 0)
            selectedItem = storedItems.Length - 1;
        else if (selectedItem > storedItems.Length - 1)
            selectedItem = 0;

        // once this has changed tell the inventory ui to update
        uIInventory.updateItemSelect();
    }

    // decrease the current selected item by 1
    // if there is 0 of that item remove it     
    public void decrementCurrentItem()
    {
        storedItems[selectedItem].amount -= 1;

        // if there is none of the item left net remove it from the items array
        if (storedItems[selectedItem].amount <= 0)
            storedItems[selectedItem].item = null;

        // once this has changed tell the inventory ui to update
        uIInventory.updateUIContents();
    }

    public int addItem(Item item, int addingAmount)
    {
        // if the item we are trying to add is null then return false
        if (item == null)
            return -1;

        // going through looking for same items
        for (int i = 0; i < storedItems.Length; i++)
        {
            if (storedItems[i].item == null)
                continue;

            // go through each item that is the same as ours and add the 
            // amount we can to it, after if we still have some left then continue
            if (storedItems[i].item.tileID == item.tileID && storedItems[i].amount < LookUpData.maxNumberOfItemsPerSlot)
            {
                // if the current amount plus the adding amount is more than max
                // then set the amount to add as the number it would take to fill the slot
                // if the amount after the adding is less then max then use the current item amount
                int amountAddingToThisSlot = storedItems[i].amount + addingAmount > LookUpData.maxNumberOfItemsPerSlot ? LookUpData.maxNumberOfItemsPerSlot - storedItems[i].amount : addingAmount;

                // add what we we have space for 
                storedItems[i].amount += amountAddingToThisSlot;

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
        for (int i = 0; i < storedItems.Length; i++)
        {
            if (storedItems[i].item == null)
            {
                int amountToAdd = addingAmount > LookUpData.maxNumberOfItemsPerSlot ? LookUpData.maxNumberOfItemsPerSlot : addingAmount;
                storedItems[i].item = item;
                storedItems[i].amount = amountToAdd;
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

public struct StoredItem
{
    public int amount;
    public Item item;
}
