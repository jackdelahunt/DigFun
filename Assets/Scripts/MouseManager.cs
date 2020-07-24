using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : MonoBehaviour
{
    [SerializeField] Vector3 mousePosition;
    [SerializeField] Vector3Int mousePositionAsInt;
	[SerializeField] PlayerToWorld playerToWorld;
	

	private void Update()
	{
		getMousePosition();
		handleMouseInput();
	}

	void handleMouseInput()
	{
		// if you are clicking left then delete this tile
		if (Input.GetButtonDown("Fire1"))
			playerToWorld.removeTile(mousePositionAsInt);
		else if (Input.GetButtonDown("Fire2"))
			playerToWorld.addTile(mousePositionAsInt);
	}

	Vector3Int getMousePosition()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		mousePositionAsInt = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0);

		return mousePositionAsInt;
	}
}
