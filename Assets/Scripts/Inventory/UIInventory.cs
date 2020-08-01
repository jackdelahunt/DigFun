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

    // the item select UI element that moves along the hotbar
    public GameObject itemSelectGameObject;

    public void Start() {
        playerInventory = FindObjectOfType<Inventory>();

        // set the initial values of the UI
        updateUIContents();
    }

    public void updateUIContents() {

        // got through each item in the player inventory
        for(int i = 0; i < playerInventory.items.Length; i++) {

            // if the item is empty
            if(playerInventory.items[i] == null) {

                // clear the text
                hotbarCounts[i].SetText("");

                // set that hotbar image to false, setting the .sprite to null does not work
                hotbarImages[i].gameObject.SetActive(false);
                continue;
            }

            // the quantatie of the current item
            int currentAmount = playerInventory.quantaties[i];

            // set the repsective text to it's amount
            hotbarCounts[i].SetText("" + currentAmount);

            // set the image to the items sprite
            hotbarImages[i].sprite = playerInventory.items[i].sprite;

            // display the image
            hotbarImages[i].gameObject.SetActive(true);
        }
    }

    public void updateItemSelect() {

        // get the slot that the selected item represents
        GameObject slotGameObject =  hotbarCounts[playerInventory.selectedItem].gameObject.transform.parent.gameObject;

        // move the selecte item object to that position
        itemSelectGameObject.transform.position = slotGameObject.transform.position;
    }

}
