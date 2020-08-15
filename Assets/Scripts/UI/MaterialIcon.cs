using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;


public class MaterialIcon : MonoBehaviour
{
    public TMP_Text amountText;
    public Image image;

    public void init(ItemGroup itemGroup, int amount) {
        image.sprite = itemGroup.sprite;

        if(amount == 1)
            amountText.SetText("");
        else
            amountText.SetText("" + amount);
        
    }
}