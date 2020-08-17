using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface WorkbenchInterface
{
    void updateContent();
    void craft();
    void showRecipe(Recipe recipe);
}
