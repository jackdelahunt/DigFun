using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour, BlockEntity
{
    public GameObject workbenchUI;

    public void Start() {
        workbenchUI = GameObject.FindGameObjectWithTag("WorkbenchUI");
    }

    public void interacted() {
        workbenchUI.SetActive(!workbenchUI.activeSelf);
    }
}
