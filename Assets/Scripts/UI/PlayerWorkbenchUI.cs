using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class PlayerWorkbenchUI : WorkbenchUI
{

    public override void updateContent() {
        // first remove all of the contents
        foreach(Transform child in recipeContentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // go though each recipe and make a button for it, if we can craft it
        foreach(Recipe recipe in refrenceManager.getRecipes()) {
            
            // if the recipe only requires one material and that material only needs 4 items
            // then that recipe is craftable so instantiate a icon for it
            if(recipe.materials.Count <= 2 && playerInventory.isCraftable(recipe)) {
                GameObject recipeCraftingIcon = Instantiate(craftingIconPrefab, recipeContentArea.transform);
                recipeCraftingIcon.GetComponent<CraftingIcon>().init(recipe, this);
            }

        }
    }
}
