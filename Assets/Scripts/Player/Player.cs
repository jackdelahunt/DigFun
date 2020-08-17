﻿using System.Collections;
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
}
