using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	GameObject player;

	private void Start()
	{
		// get the player object in the scene
		player = GameObject.FindGameObjectWithTag("Player");
	}

	private void Update()
	{
		// centre the camera on the player at all times
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, -10);
	}
}
