using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class PlayerInventory : MonoBehaviour {

	[SerializeField] Item[] inventory = new Item[8];
	Database database;

	// Use this for initialization
	void Start () {
		database = FindObjectOfType<Database>();
		InitializePlayer(); 
	}

	// Resets the player to basic loadout.
	private void InitializePlayer()
	{
		inventory[0] = database.items[0];
		inventory[1] = database.items[1];
		inventory[2] = database.items[2];
		inventory[3] = database.items[3];
		inventory[4] = database.items[4];
		inventory[5] = database.items[5];
		inventory[6] = database.items[6];
		inventory[7] = database.items[7];
	}

	public void ReplacePart(Item item)
	{
		for (int i = 0; i < inventory.Length; i++)
		{
			if (inventory[i].itemType == item.itemType)
			{
				inventory[i] = item;
				break;
			}
		}
	}
}
