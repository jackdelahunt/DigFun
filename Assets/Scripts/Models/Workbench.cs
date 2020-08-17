using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, BlockEntity
{
    public PopupUI popupUI;
    public GameObject workbenchUIGameObject;
    public WorkbenchUI ui;

    public void Start() {
        // getting the refrence of the workbench ui from the popup ui, and storing both
        popupUI = GameObject.FindGameObjectWithTag("PopupUI").GetComponent<PopupUI>();
        workbenchUIGameObject = popupUI.workbenchUI;
        ui = workbenchUIGameObject.GetComponent<WorkbenchUI>();
    }

    public void interacted(Player player) {

        // toggle the active state of the workbench ui
        workbenchUIGameObject.SetActive(!workbenchUIGameObject.activeSelf);

        player.freeze = workbenchUIGameObject.activeSelf;
    }
}
