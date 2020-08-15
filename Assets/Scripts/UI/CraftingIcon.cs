using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingIcon : MonoBehaviour
{
    public Image image;
    public Recipe recipe;
    public WorkbenchUI ui;

    public void Awake() {
        ui = GameObject.FindGameObjectWithTag("WorkbenchUI").GetComponent<WorkbenchUI>();
    }

    public void init(Recipe recipe) {
        this.recipe = recipe;
        image.sprite = recipe.result.itemGroup.sprite;
    }

    public void showRecipe() {
        ui.showRecipe(recipe);
    }
}
