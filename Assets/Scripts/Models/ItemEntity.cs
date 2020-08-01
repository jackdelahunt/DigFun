using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    // the item that this itemEntity represents
    public Item item;

    // the sprite renderer of the gameObject
    public SpriteRenderer spriteRenderer;

    // what scale should this entity display at
    public Vector2 entityScale;
    
    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void init() {

        // set the correct sprite from the object
        spriteRenderer.sprite = item.sprite;

        // set the scale of the object to the correct scale
        transform.localScale = new Vector3(entityScale.x, entityScale.y, transform.localScale.z);
    }
}
