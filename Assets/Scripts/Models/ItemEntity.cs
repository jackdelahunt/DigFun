using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    public Item item;
    public SpriteRenderer spriteRenderer;
    public Vector2 entityScale;

    public void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void init() {
        spriteRenderer.sprite = item.sprite;
        transform.localScale = new Vector3(entityScale.x, entityScale.y, transform.localScale.z);
    }
}
