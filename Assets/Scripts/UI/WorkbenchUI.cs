using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class WorkbenchUI : MonoBehaviour
{
    public RefrenceManager refrenceManager;
    public Inventory playerInventory;
    public GameObject contentArea;
    public GameObject craftingIconPrefab;


    public void Awake() {
        print("START");
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
        playerInventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
    }
    public void updateContent() {
        print("UPDATE");
        // first remove all of the contents
        foreach(Transform child in contentArea.transform) {
            GameObject.Destroy(child.gameObject);
        }

        foreach(Recipe recipe in refrenceManager.getRecipes()) {
            GameObject recipeCraftingIcon = Instantiate(craftingIconPrefab, contentArea.transform);
            recipeCraftingIcon.GetComponent<Image>().sprite = recipe.result.item.sprite;
        }
    }
}
