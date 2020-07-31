using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MouseManager : MonoBehaviour
{
    [SerializeField] private Vector3 mousePosition;
    [SerializeField] private Vector3Int mousePositionAsInt;
	[SerializeField] private PlayerToWorld playerToWorld;

	[SerializeField] private Transform playerTransform;	

	private void Update()
	{
		getMousePositionAsInt();
		handleMouseInput();
	}

	public void handleMouseInput()
	{
		if(isMouseClickInRange()) {
			// if you are clicking left the tell playerToWorld to delete the block
			if (Input.GetButtonDown("Fire1")) {
					playerToWorld.removeTile(mousePositionAsInt);
			}
			else if (Input.GetButtonDown("Fire2")) {
				playerToWorld.addTile(mousePositionAsInt);
			}
		}

		// if the player is scrolling at all then change the selected item
		if((int)Input.mouseScrollDelta.y !=0) {
			playerToWorld.changeSelectedItem((int)Input.mouseScrollDelta.y);
		}
		
	}

	public Vector3Int getMousePositionAsInt()
	{
		mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
		mousePositionAsInt = new Vector3Int(Mathf.FloorToInt(mousePosition.x), Mathf.FloorToInt(mousePosition.y), 0);

		return mousePositionAsInt;
	}

	// returns true if the mouse is in the max mouse range
	public bool isMouseClickInRange() {

		// if that disance is below the max rang return true
		float playerToMouseDistance = Vector3.Distance(playerTransform.position, mousePositionAsInt);
		return playerToMouseDistance < LookUpData.playerMouseRange;
	}
}
