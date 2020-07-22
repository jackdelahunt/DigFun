using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Vector3 mousePosition;
    [SerializeField] Vector3Int mousePositionAsInt;
	[SerializeField] World world;

	private void Start()
	{
		world = GameObject.FindGameObjectWithTag("ChunkManager").GetComponent<World>();
	}

	private void Update()
	{
		getMousePosition();
		handleMouseInput();
	}

	void handleMouseInput()
	{
		// if you are clicking left then delete this tile
		if (Input.GetButtonDown("Fire1"))
			world.getChunk(mousePositionAsInt).removeTile(mousePositionAsInt);
		else if (Input.GetButtonDown("Fire2"))
			world.getChunk(mousePositionAsInt).addTile(mousePositionAsInt, null);
	}

	Vector3Int getMousePosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		mousePositionAsInt = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0);

		return mousePositionAsInt;
	}
}
