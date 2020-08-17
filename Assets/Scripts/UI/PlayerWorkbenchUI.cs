using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerWorkbenchUI : WorkbenchUI
{

    public override void craft() {

        // if we have nothing selected then do not try to craft
        if(currentSelectedRecipe == null)
            return;

        // if the item is craftable
        if(currentSelectedRecipe.materials.Count == 1 && currentSelectedRecipe.materials[0].amount <= 4 && playerInventory.isCraftable(currentSelectedRecipe)) {
            
            // for each material remove that amount from the player inventory
            foreach(StoredItem material in currentSelectedRecipe.materials) {
                playerInventory.removeItem(material.itemGroup, material.amount);
            }

            // then add the resukt to the onventory
            playerInventory.addItem(currentSelectedRecipe.result.itemGroup, currentSelectedRecipe.result.amount);
        }

        updateContent();

        // if the recipe is not longer craftable
        if(!playerInventory.isCraftable(currentSelectedRecipe)) {
            
            // remove all of the material icons
            foreach(Transform child in materialContentArea.transform) {
                GameObject.Destroy(child.gameObject);  
            }

            // remove the selection
            currentSelectedRecipe = null;
        }
    }
}
