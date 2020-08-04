using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntity : MonoBehaviour
{
    // the item that this itemEntity represents
    public Item item;

    // the amount of the item this entity stores
    public int amount;

    // the sprite renderer of the gameObject
    public SpriteRenderer spriteRenderer;

    // what scale should this entity display at
    public Vector2 entityScale;

    // what layer the entity interacts with when updating
    public LayerMask mask;

    public bool mergedWithOtherEntity;

    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        amount = 1;
        mergedWithOtherEntity = false;
    }

    public void init()
    {

        // set the correct sprite from the object
        spriteRenderer.sprite = item.sprite;

        // set the scale of the object to the correct scale
        transform.localScale = new Vector3(entityScale.x, entityScale.y, transform.localScale.z);
    }

    public void lookForOtherEntities()
    {
        // get all item entity colliders in this range
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f, mask);

        // create a collider list based on the colliders array
        List<Collider2D> colliderList = new List<Collider2D>(colliders);

        // remove this objects collider from the list so
        // we do not destroy ourselves
        colliderList.Remove(GetComponent<Collider2D>());

        for (int i = 0; i < colliderList.Count; i++)
        {
            // get the itemEntity class from the collider
            ItemEntity other = colliderList[i].gameObject.GetComponent<ItemEntity>();

            // if the other item is the same as this
            if (other.item.tileID == item.tileID)
            {
                // add the others amount to ours
                amount += other.amount;

                // flag it as about to be merged so it will not update
                other.mergedWithOtherEntity = true;

                // then destroy the game object relating to it
                Destroy(other.gameObject);
            }
        }
    }
}
