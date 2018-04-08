using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Specials : MonoBehaviour {

	Database db;

	void Start()
	{
		db = GetComponent<Database>();
	}

	public void CheckSpecial(Item item)
	{
		switch (item.itemID)
		{
			case 7:
				print("Vulcan head");
				break;
			default:
				return;
		}
	}

	void VulcanCannons(Item item)
	{

	}
}
