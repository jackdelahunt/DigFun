﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CraftingIcon : MonoBehaviour
{
    public Image image;
    public Recipe recipe;
    public WorkbenchUI ui;

    public void init(Recipe recipe, WorkbenchUI ui) {
        this.recipe = recipe;
        this.ui = ui;

        image.sprite = recipe.result.itemGroup.sprite;
    }

    public void showRecipe() {
        ui.showRecipe(recipe);
    }
}
