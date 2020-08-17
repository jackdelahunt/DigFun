using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    WorkbenchUI ui;

    public void Awake() {
        ui = transform.parent.GetComponent<WorkbenchUI>();
    }

    public void craft() {
        ui.craft();
    }
}
