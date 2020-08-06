using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerToWorld : MonoBehaviour
{
    [SerializeField] private World world;
    [SerializeField] private Inventory inventory;
    [SerializeField] private RefrenceManager refrenceManager;
    [SerializeField] private GameObject itemEntityPrefab;

    private bool foo = false;

    private void Start()
    {
        world = GameObject.FindGameObjectWithTag("World").GetComponent<World>();
        refrenceManager = GameObject.FindGameObjectWithTag("RefrenceManager").GetComponent<RefrenceManager>();
    }

    // tells the world object to get the chunk that we are in and reomve the tile
    public void removeTile(Vector3Int pos)
    {
        Chunk chunkAtThisPos = world.getChunk(pos);
        // get the id of the item that the tile is that is being removed
        int tileID = chunkAtThisPos.removeTile(pos);

        // get the item based on the tileID
        Item tileItem = refrenceManager.getItem(tileID);

        // if the item is actually an item
        if (tileItem != null)
        {

            // create a entity 
            GameObject itemObject = Instantiate(itemEntityPrefab, new Vector3(pos.x + 0.5f, pos.y + 0.5f, pos.z), new Quaternion(0f, 0f, 0f, 0f));
            ItemEntity itemEntity = itemObject.GetComponent<ItemEntity>();

            // set the item of the itemEntity to the item based on the id
            itemEntity.item = tileItem;

            // initzialize the entity	
            itemEntity.init();

            // add the item object to the chunk entities
            chunkAtThisPos.entities.Add(itemObject);
        }
    }

    public void addTile(Vector3Int pos)
    {
        // get the tile id of the item in the inventory
        int IDOfItemSelectedInInventory = inventory.getIDOfCurrentItem();

        // if it is the flag -1 then do not add this block
        if (IDOfItemSelectedInInventory <= -1)
            return;

        // if this block was added then decrease the count in the inventory  by one
        // this will not happen if there is already a block in that area
        if (world.getChunk(pos).addTile(pos, refrenceManager.getTile(IDOfItemSelectedInInventory), IDOfItemSelectedInInventory))
            inventory.decrementCurrentItem();
    }

    // attempts to add a item to the inventory
    public int addItemToInventory(ItemEntity entity)
    {
        // true if the item was added false if it was not
        return inventory.addItem(entity.item, entity.amount);
    }

    // changed the selected item in the inventory by the input amount
    public void changeSelectedItem(int amount)
    {
        inventory.changeSelectedItem(amount);
    }

    // called when the player collides with another surface
    void OnCollisionEnter2D(Collision2D col)
    {

        // if that other surface was an itemEntity
        if (col.collider.tag == "ItemEntity")
        {
            // get the item that the entity was storing 
            ItemEntity itemEntity = col.collider.gameObject.GetComponent<ItemEntity>();

            // if the item was succefully added to the inventory then destroy it
            int remainder = addItemToInventory(itemEntity);
            if (remainder == -1)
                Destroy(col.collider.gameObject);
            else
                itemEntity.amount = remainder;
        }
    }
}
