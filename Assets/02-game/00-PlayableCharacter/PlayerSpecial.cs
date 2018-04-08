using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour {

	Database db;
	int[] invID = new int[3];

	// Use this for initialization
	void Start()
	{
		db = FindObjectOfType<Database>();
		for (int i = 0; i < invID.Length; i++)
		{
			invID[i] = -1;
		}
	}

	// Update is called once per frame
	void Update() {
		SpecialListener();
	}

	void SpecialListener()
	{
		for (int i = 0; i < invID.Length; i++)
		{
			if (invID[i] == 7)
			{
				print("Fireing head cannons");
			}
		}
	}
	void SpecialPassive()
	{
		// activate passive things on pickup.
	}

	public void SpecialCheck (Item item)
	{
		if (item.itemSpecial)
		{
			switch (item.itemLoc)
			{
				case ItemLoc.head:
					invID[0] = item.itemID;
					break;
				case ItemLoc.leftArm:
					invID[1] = item.itemID;
					break;
				case ItemLoc.rightArm:
					invID[2] = item.itemID;
					break;
				default:
					break;
			}
			SpecialPassive();
		}
	}
}
