using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float hInput;
    private float vInput;
    public float speed = 10f;

    private Rigidbody2D rBody;

    void Start()
    {
        hInput = 0f;
        vInput = 0f;

        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        updateMovement();
    }

    void updateMovement()
	{
        hInput = Input.GetAxis("Horizontal");

        vInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(hInput, vInput);

        rBody.AddForce(movement * speed);
    }
}
