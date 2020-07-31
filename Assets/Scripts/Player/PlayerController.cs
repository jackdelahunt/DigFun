using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private float hInput;
    public float speed = 1f;
    public float jumpForce = 10f;

    private Rigidbody2D rBody;

    void Start()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        updateMovement();
    }

    void updateMovement()
	{
        hInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(hInput, 0, 0) * speed * Time.deltaTime;
        transform.position += movement;

        if(Input.GetButtonDown("Jump") && rBody.velocity.y < 0.0001f)
            rBody.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
    }
}
