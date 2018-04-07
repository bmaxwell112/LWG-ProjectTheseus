using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add(new Item(0, "Basic Head", "A head taken from the only schematics you have left.", ItemLoc.head, ItemType.aesthetic, 10, 0, 0, false));
		items.Add(new Item(1, "Basic Torso", "A torso taken from the only schematics you have left.", ItemLoc.body, ItemType.aesthetic, 50, 0, 0, false));
		items.Add(new Item(2, "Basic Left Arm", "A left arm taken from the only schematics you have left.", ItemLoc.leftArm, ItemType.melee, 10, 5, 0, false));
		items.Add(new Item(3, "Basic Right Arm", "A right arm taken from the only schematics you have left.", ItemLoc.rightArm, ItemType.melee, 10, 5, 0, false));
		items.Add(new Item(4, "Basic Legs", "A pair of legs taken from the only schematics you have left.", ItemLoc.legs, ItemType.speed, 10, 2, 0, false));		
		items.Add(new Item(5, "Basic Back", "A back taken from the only schematics you have left.", ItemLoc.back, ItemType.aesthetic, 20, 0, 0, false));
		items.Add(new Item(6, "Basic AI Core", "An AI Core taken from the only schematics you have left.", ItemLoc.core, ItemType.aesthetic, 0, 0, 0, false));
		items.Add(new Item(7, "Head Vulcans", "A head with vulcan guns above the eyes.", ItemLoc.head, ItemType.aesthetic, 10, 0, 0, true));
		items.Add(new Item(8, "Heavy Torso", "A bulkier body capable of taking more punishment.", ItemLoc.body, ItemType.aesthetic, 80, 0, 0, false));
		items.Add(new Item(9, "Chainsaw Arm L", "A melee arm with a chance to cleanly remove a part.", ItemLoc.leftArm, ItemType.melee, 10, 10, 0, true));
		items.Add(new Item(10, "Chainsaw Arm R", "A melee arm with a chance to cleanly remove a part.", ItemLoc.rightArm, ItemType.melee, 10, 10, 0, true));
		items.Add(new Item(11, "Machinegun Arm L", "Fires bullets very quickly at foes.", ItemLoc.leftArm, ItemType.range, 10, 5, 10, false));
		items.Add(new Item(12, "Machinegun Arm R", "Fires bullets very quickly at foes.", ItemLoc.rightArm, ItemType.range, 10, 5, 10, false));
		items.Add(new Item(13, "Rollerblades", "Increased movement speed, also makes you look cool.", ItemLoc.legs, ItemType.speed, 20, 3, 0, false));		
		items.Add(new Item(14, "Backpack", "Increases your max battery capacity.", ItemLoc.back, ItemType.aesthetic, 20, 0, 0, true));
		items.Add(new Item(15, "Hacking AI", "Allows you to hack doors and computer systems.", ItemLoc.core, ItemType.aesthetic, 0, 0, 0, true));
	}

	public Item RandomItemOut(ItemLoc loc)
	{
		Item randItem = new Item();
		List<int> locItems = new List<int>();
		for (int i = 0; i < items.Count; i++)
		{
			if (items[i].itemLoc == loc)
			{				
				locItems.Add(items[i].itemID);
				if (items[i].itemID < 8)
				{
					locItems.Add(items[i].itemID);
					locItems.Add(items[i].itemID);
				}
			}
		}
		if (locItems.Count != 0)
		{
			int rand = Random.Range(0, locItems.Count);
			randItem = items[locItems[rand]];
		}
		return randItem;
	}
}
