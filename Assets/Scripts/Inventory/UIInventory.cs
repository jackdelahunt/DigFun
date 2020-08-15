using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour
{
    public Inventory playerInventory;

    // all of the images
    public Image[] hotbarImages;

    // the number counts of each item that are displayed as text
    public TMP_Text[] hotbarCounts;

    // the name diaplay for the current item selected
    public TMP_Text displayName;

    // the item select UI element that moves along the hotbar
    public GameObject itemSelectGameObject;

    public void Start()
    {
        playerInventory = FindObjectOfType<Inventory>();

        // set the initial values of the UI
        updateUIContents();
    }

    public void updateUIContents()
    {

        // got through each item in the player inventory
        for (int i = 0; i < playerInventory.storedItems.Length; i++)
        {

            // if the item is empty
            if (playerInventory.storedItems[i].itemGroup == null)
            {

                // clear the text
                hotbarCounts[i].SetText("");

                // set that hotbar image to false, setting the .sprite to null does not work
                hotbarImages[i].gameObject.SetActive(false);
                continue;
            }

            // the quantatie of the current item
            int currentAmount = playerInventory.storedItems[i].amount;

            // set the repsective text to it's amount
            hotbarCounts[i].SetText("" + currentAmount);

            // set the image to the items sprite
            hotbarImages[i].sprite = playerInventory.storedItems[i].itemGroup.sprite;

            // display the image
            hotbarImages[i].gameObject.SetActive(true);
        }

        updateDisplayName();
    }

    public void updateItemSelect()
    {

        // get the slot that the selected item represents
        GameObject slotGameObject = hotbarCounts[playerInventory.selectedItem].gameObject.transform.parent.gameObject;

        // move the selecte item object to that position
        itemSelectGameObject.transform.position = slotGameObject.transform.position;

        updateDisplayName();
    }

    public void updateDisplayName()
    {
        // if the item is not null then display the name else display nothing
        if (playerInventory.storedItems[playerInventory.selectedItem].itemGroup != null)
            displayName.SetText("" + playerInventory.storedItems[playerInventory.selectedItem].itemGroup.itemName);
        else
            displayName.SetText("");

    }

}
