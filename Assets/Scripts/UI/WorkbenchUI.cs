using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WorkbenchUI : MonoBehaviour
{
    public RefrenceManager refrenceManager;
    public Inventory playerInventory;

    public GameObject recipeContentArea;
    public GameObject craftingIconPrefab;

    public GameObject materialContentArea;
    public GameObject materialIconPrefab;


    public void Awake() {
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
        playerInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }
    public void updateContent() {
        
        // first remove all of the contents
        foreach(Transform child in recipeContentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Recipe recipe in refrenceManager.getRecipes()) {
            GameObject recipeCraftingIcon = Instantiate(craftingIconPrefab, recipeContentArea.transform);
            recipeCraftingIcon.GetComponent<CraftingIcon>().init(recipe);
        }
    }

    public void showRecipe(Recipe recipe) {
        foreach(Transform child in materialContentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(StoredItem material in recipe.materials) {
            GameObject materialIcon = Instantiate(materialIconPrefab, materialContentArea.transform);
            materialIcon.GetComponent<MaterialIcon>().init(material.itemGroup, material.amount);
        }
    }
}
