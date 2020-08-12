using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockEntity : MonoBehaviour
{
    public RefrenceManager refrenceManager;
    public GameObject popupUI;
    public GameObject blockEntityUI;
    public int blockEntityID = 0;

    void Awake() {
        popupUI = GameObject.FindGameObjectWithTag("PopupUI");
        blockEntityUI = Instantiate(refrenceManager.getBlockEntity(blockEntityID), popupUI.transform);
    }
    public void interacted() {

    }
}
