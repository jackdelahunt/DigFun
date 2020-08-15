using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftButton : MonoBehaviour
{
    public WorkbenchUI ui;

    public void craft() {
        ui.craft();
    }
}
