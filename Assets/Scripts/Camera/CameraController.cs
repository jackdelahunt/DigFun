using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public Camera mainCamera;
    public float minCameraZoom = 2f;
    public float maxCameraZoom = 12f;

    private void Start()
    {
        // get the player object in the scene
        player = GameObject.FindGameObjectWithTag("Player");
        mainCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        // centre the camera on the player at all times
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);

        if (Input.GetAxis("Crouch") != 0)
        {
            if ((int)Input.mouseScrollDelta.y != 0)
            {
                float newSize = mainCamera.orthographicSize + Input.mouseScrollDelta.y;
                newSize = Mathf.Clamp(newSize, minCameraZoom, maxCameraZoom);
                mainCamera.orthographicSize = newSize;

            }
        }
    }
}
