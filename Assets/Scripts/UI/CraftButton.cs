using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    WorkbenchInterface ui;

    public void Awake() {
        ui = transform.parent.GetComponent<WorkbenchInterface>();
    }

    public void craft() {
        ui.craft();
    }
}
