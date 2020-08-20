using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D myRigid;
    public Animator animator;
    public PlayerToWorld playerToWorld;
    public LayerMask whatIsBlockEntity;
    public Inventory inventory;
    public GameObject playerWorkbenchUI;

    // is this player able to be interacted with?
    public bool freeze;

    // run speed of the player 
    public float runSpeed = 40f;

    // the amount of horizontal movement the player is doing this frame
    float horizontalMove = 0f;

    // is the player jumping?
    bool jump = false;

    public void Awake() {
        animator = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
        playerToWorld = GetComponent<PlayerToWorld>();
        inventory = GetComponent<Inventory>();
    }

    public void Start() {
        playerWorkbenchUI = GameObject.FindGameObjectWithTag("PopupUI").GetComponent<PopupUI>().playerWorkbenchUI;
    }

    public void initializeAsNewWorld()
    {
        transform.position = new Vector3(0, LookUpData.chunkHeight, 0);
    }

    // Update is called once per frame
    void Update()
    {
        handleMovement();
        checkPlayerAnimations();
        checkForInteractions();
        getMousePositionAsInt();
		handleMouseInput();
    }

    public void checkPlayerAnimations()
    {

        // if the user is telling the player to move
        // then animate
        if (Input.GetAxisRaw("Horizontal") != 0)
            animator.SetBool("Walking", true);
        else
            animator.SetBool("Walking", false);

        if (jump)
            animator.SetBool("Jump", true);
        else
            animator.SetBool("Jump", false);

    }

    public void handleMovement()
    {
        // if the player is frozen then return
        if(freeze) {

            myRigid.velocity = new Vector2(0 , myRigid.velocity.y);
            return;
        }

        jump = false;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
    }

    public void checkForInteractions() {
        if(Input.GetButtonDown("Interact")) {
            
            // if the player workbench is open then close it and do not
            // interact
            if(playerWorkbenchUI.activeSelf) {
                playerWorkbenchUI.SetActive(false);
                freeze = false;
                return;
            }

            // look for a block entitty in the range of the player        
            Collider2D collider = Physics2D.OverlapCircle(transform.position, LookUpData.playerRange, whatIsBlockEntity);

            // if we hit something then open that else open the player workbench
            if(collider != null) {

                // get the entity script and tell it we interacted with it
                BlockEntity blockEntity = (BlockEntity)collider.gameObject.GetComponent(typeof(BlockEntity));
                blockEntity.interacted(this);
            } else {
                playerWorkbenchUI.SetActive(true);
                freeze = true;
            }
        }
    }

    public void handleMouseInput()
	{
		// if the mouse is in the range of the player and the player is
        // not frozen
		if(!freeze) {

			// if you are clicking left the tell playerToWorld to delete the block
			// if they are clicking right then try and add a block
			if (Input.GetButtonDown("Fire1") && isMouseClickInRange()) {
					playerToWorld.removeTile(getMousePositionAsInt());
			}
			else if (Input.GetButtonDown("Fire2") && isMouseClickInRange() && !isMouseOverPlayer()) {
				playerToWorld.addTile(getMousePositionAsInt());
			}
		}

		// if the player is scrolling at all then change the selected item
		if((int)Input.mouseScrollDelta.y != 0) {
			playerToWorld.changeSelectedItem((int)Input.mouseScrollDelta.y);
		}
		
	}

	public Vector3Int getMousePositionAsInt()
	{
		// convert that to use ints
		return new Vector3Int(Mathf.FloorToInt(getMousePosition().x), Mathf.FloorToInt(getMousePosition().y), 0);
	}

    public Vector3 getMousePosition() 
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        return new Vector3(mousePos.x, mousePos.y, 0);
    }

    public Vector3Int getPlayerPositionAsInt()
	{
		// convert that to use ints
		return new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), 0);
	}

	// returns true if the mouse is in the max mouse range
	public bool isMouseClickInRange() 
    {
        if(Vector3.Distance(getMousePosition(), transform.position) >= LookUpData.playerRange)
            return false;

        return true;
	}

    public bool isMouseOverPlayer() {
        // the position of the tile below the player, y - 1 of player as int
		Vector3Int tileBelow = new Vector3Int(getPlayerPositionAsInt().x, getPlayerPositionAsInt().y - 1, 0);
    
        // if the mouse pos is the same or one below the player int them return false
        if(getMousePositionAsInt() == tileBelow || getMousePositionAsInt() == getPlayerPositionAsInt())
            return true;

        return false;
    }
}
