using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, BlockEntity
{
    public PopupUI popupUI;
    public GameObject workbenchUI;

    public void Start() {
        // getting the refrence of the workbench ui from the popup ui, and storing both
        popupUI = GameObject.FindGameObjectWithTag("PopupUI").GetComponent<PopupUI>();
        workbenchUI = popupUI.workbenchUI;
    }

    public void interacted(Player player) {

        // toggle the active state of the workbench ui
        workbenchUI.SetActive(!workbenchUI.activeSelf);

        player.freeze = workbenchUI.activeSelf;

        
    }
}
