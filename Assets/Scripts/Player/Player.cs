using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public CharacterController2D controller;
    public Rigidbody2D myRigid;
    public Animator animator;

    public float runSpeed = 40f;

    float horizontalMove = 0f;
    bool jump = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        myRigid = GetComponent<Rigidbody2D>();
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
        jump = false;
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }

        controller.Move(horizontalMove * Time.fixedDeltaTime, jump);
    }
}
