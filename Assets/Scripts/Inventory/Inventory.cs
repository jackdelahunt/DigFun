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

    public RefrenceManager refrenceManager;

    public void Awake() {
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
    }

    private void Start()
    {
        uIInventory = GameObject.FindGameObjectWithTag("UIInventory").GetComponent<UIInventory>();

        selectedItem = 0;

        // 9 is the amount items this iventory can hold
        storedItems = new StoredItem[9];

        // give the player a workbench at the start
        storedItems[0].amount = 1;
        storedItems[0].itemGroup = refrenceManager.itemGroups[7];
    }

    // get the tileID of the current item selected in the inventory
    public int getIDOfCurrentItem()
    {
        // if there is a item in the slot then return that id
        // else return the flag -1
        return storedItems[selectedItem].itemGroup == null ? -1 : storedItems[selectedItem].itemGroup.id;
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
            storedItems[selectedItem].itemGroup = null;

        // once this has changed tell the inventory ui to update
        uIInventory.updateUIContents();
    }

    public int addItem(ItemGroup addingItemGroup, int addingAmount)
    {
        // if the item we are trying to add is null then return false
        if (addingItemGroup == null)
            return -1;

        // going through looking for same items
        for (int i = 0; i < storedItems.Length; i++)
        {
            if (storedItems[i].itemGroup == null)
                continue;

            // go through each item that is the same as ours and add the 
            // amount we can to it, after if we still have some left then continue
            if (storedItems[i].itemGroup.id == addingItemGroup.id && storedItems[i].amount < LookUpData.maxNumberOfItemsPerSlot)
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
            if (storedItems[i].itemGroup == null)
            {
                // set the adding amount to the max amount we can add based on the current amount in the 
                // slot, if we can add the whole amount then add it
                int amountToAdd = addingAmount > LookUpData.maxNumberOfItemsPerSlot ? LookUpData.maxNumberOfItemsPerSlot : addingAmount;
                storedItems[i].itemGroup = addingItemGroup;
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

    // returns the amount of an item we have in the inventory
    public int containsItem(ItemGroup itemGroup)
    {
        int count = 0;

        // go through all the stored items if the item is the one
        // we are looking for then add the amount to the total count
        foreach (StoredItem storedItem in storedItems)
        {
            if (storedItem.itemGroup == itemGroup)
                count += storedItem.amount;
        }

        return count;
    }

    public bool isCraftable(Recipe recipe)
    {
        // if there is no materials then return false;
        if (recipe.materials.Count == 0)
            return false;

        // go through each material in the recipe and if the inventory
        // contains the item and the correct amount then do not return false
        // once a material's needs are not met then return false
        foreach (StoredItem material in recipe.materials)
        {
            if (containsItem(material.itemGroup) < material.amount)
                return false;
        }

        // if we have reached this point and all materials requried have
        // sufficent amounts in the inventory then return true
        return true;
    }
}

[System.Serializable]
public struct StoredItem
{
    public ItemGroup itemGroup;
    public int amount;
}
