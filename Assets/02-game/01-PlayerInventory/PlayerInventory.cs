using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class PlayerInventory : MonoBehaviour {

	[SerializeField] Item head, body, leftArm, rightArm, leftLeg, rightLeg, back, core;
	Database database;

	// Use this for initialization
	void Start () {
		database = FindObjectOfType<Database>();
		InitializePlayer(); 
	}

	// Resets the player to basic loadout.
	private void InitializePlayer()
	{
		head = database.items[0];
		body = database.items[1];
		leftArm = database.items[2];
		rightArm = database.items[3];
		leftLeg = database.items[4];
		rightLeg = database.items[5];
		back = database.items[6];
		core = database.items[7];
	}

	// Update is called once per frame
	void Update () {
		
	}

	void AddItemByType(Item item, int id, ItemType type)
	{		
		for (int j = 0; j < database.items.Count; j++)
		{
			if (database.items[j].itemID == id)
			{
				if (database.items[j].itemType == type)
				{
					item = database.items[j];
					print("did this");
					break;
				}
				else
				{
					Debug.LogWarning("Item of type " + type + " can not go into " + item);
				}
			}			
		}		
	}
}
