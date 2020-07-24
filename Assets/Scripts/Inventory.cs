using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public int[] quantaties;

    [SerializeField] private int selectedItem;

	private void Start()
	{
		selectedItem = 1;
		quantaties = new int[items.Length];
		for (int i = 0; i < quantaties.Length; i++)
			quantaties[i] = 10;
	}
	public int consumeCurrentItem()
	{
		int id = items[selectedItem].tileID;
		quantaties[selectedItem] -= 1;
		return id;
	}
}
 