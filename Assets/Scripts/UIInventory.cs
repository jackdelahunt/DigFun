using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class UIInventory : MonoBehaviour
{
    public Inventory playerInventory;
    public Image[] hotbarImages;
    public TMP_Text[] hotbarCounts;
    public GameObject itemSelectGameObject;

    public void Start() {
        playerInventory = FindObjectOfType<Inventory>();

        updateUIContents();
    }

    public void updateUIContents() {
        for(int i = 0; i < playerInventory.items.Length; i++) {

            if(playerInventory.items[i] == null) {
                hotbarCounts[i].SetText("");
                hotbarImages[i].gameObject.SetActive(false);
                continue;
            }

            int currentAmount = playerInventory.quantaties[i];
            hotbarCounts[i].SetText("" + currentAmount);
            hotbarImages[i].sprite = playerInventory.items[i].sprite;
            hotbarImages[i].gameObject.SetActive(true);
        }
    }

    public void updateItemSelect() {
        GameObject slotGameObject =  hotbarCounts[playerInventory.selectedItem].gameObject.transform.parent.gameObject;
        itemSelectGameObject.transform.position = slotGameObject.transform.position;
    }

}
