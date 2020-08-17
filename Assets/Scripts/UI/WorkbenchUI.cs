using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WorkbenchUI : MonoBehaviour, WorkbenchInterface
{
    public RefrenceManager refrenceManager;
    public Inventory playerInventory;

    public GameObject recipeContentArea;
    public GameObject craftingIconPrefab;

    public GameObject materialContentArea;
    public GameObject materialIconPrefab;

    public Recipe currentSelectedRecipe;


    public void Awake() {

        // say we have no recipe
        currentSelectedRecipe = null;

        // cache our refrences
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
        playerInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }

    public void OnEnable() {

        // when the object is set to active then update the
        // content
        updateContent();
    }

    public void updateContent() {
        
        // first remove all of the contents
        foreach(Transform child in recipeContentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // go though each recipe and make a button for it, if we can craft it
        foreach(Recipe recipe in refrenceManager.getRecipes()) {
            
            // if the recipe is craftable with the contents from the players inventory
            // then disply the recipe
            if(playerInventory.isCraftable(recipe)) {
                GameObject recipeCraftingIcon = Instantiate(craftingIconPrefab, recipeContentArea.transform);
                recipeCraftingIcon.GetComponent<CraftingIcon>().init(recipe, this);
            }

        }
    }

    // called when a crafting icon is pressed
    public void showRecipe(Recipe recipe) {

        currentSelectedRecipe = recipe;

        // clear the content area
        foreach(Transform child in materialContentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        // add a material icon for each material in the recipe
        foreach(StoredItem material in recipe.materials) {
            GameObject materialIcon = Instantiate(materialIconPrefab, materialContentArea.transform);
            materialIcon.GetComponent<MaterialIcon>().init(material.itemGroup, material.amount);
        }
    }

    public void craft() {

        // if we have nothing selected then do not try to craft
        if(currentSelectedRecipe == null)
            return;

        // if the item is craftable
        if(playerInventory.isCraftable(currentSelectedRecipe)) {
            
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
