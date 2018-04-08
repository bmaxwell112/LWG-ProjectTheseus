using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour {

	Database db;
	PlayerController player;
	int[] invID = new int[3];
	bool[] invActive = new bool[3];

	// Use this for initialization
	void Start()
	{
		db = FindObjectOfType<Database>();
		player = GetComponent<PlayerController>();
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
			// Vulcan Cannons
			if (invID[i] == 7 && !invActive[0])
			{				
				StartCoroutine(VulcanCannons());				
			}
		}
	}

	private IEnumerator VulcanCannons()
	{
		invActive[0] = true;
		while (invActive[0])
		{
			print(db.items[7].itemValue + ", " + db.items[7].itemValueTwo);
			player.SpawnBullets(db.items[7], transform, 0.25f);
			yield return new WaitForSeconds(0.25f);
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
