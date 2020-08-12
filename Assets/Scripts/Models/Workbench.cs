using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public GameObject workbenchUI;

    
    public void interacted() {
        workbenchUI.SetActive(!workbenchUI.activeSelf);
    }
}
