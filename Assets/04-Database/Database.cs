using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add(new Item(0, "Basic Head", "A head taken from the only schematics you have left.", ItemLoc.head, ItemType.asthetic, 10, 0, 0, false));
		items.Add(new Item(1, "Basic Torso", "A torso taken from the only schematics you have left.", ItemLoc.body, ItemType.asthetic, 50, 0, 0, false));
		items.Add(new Item(2, "Basic Left Arm", "A left arm taken from the only schematics you have left.", ItemLoc.leftArm, ItemType.melee, 10, 5, 0, false));
		items.Add(new Item(3, "Basic Right Arm", "A right arm taken from the only schematics you have left.", ItemLoc.rightArm, ItemType.melee, 10, 5, 0, false));
		items.Add(new Item(4, "Basic Left Leg", "A left leg taken from the only schematics you have left.", ItemLoc.leftLeg, ItemType.asthetic, 10, 0, 0, false));
		items.Add(new Item(5, "Basic Right Leg", "A right leg taken from the only schematics you have left.", ItemLoc.rightLeg, ItemType.asthetic, 10, 0, 0, false));
		items.Add(new Item(6, "Basic Back", "A back taken from the only schematics you have left.", ItemLoc.back, ItemType.asthetic, 20, 0, 0, false));
		items.Add(new Item(7, "Basic AI Core", "An AI Core taken from the only schematics you have left.", ItemLoc.core, ItemType.asthetic, 0, 0, 0, false));
		items.Add(new Item(8, "Head Vulcans", "A head with vulcan guns above the eyes.", ItemLoc.head, ItemType.asthetic, 10, 0, 0, true));
		items.Add(new Item(9, "Heavy Torso", "A bulkier body capable of taking more punishment.", ItemLoc.body, ItemType.asthetic, 80, 0, 0, false));
		items.Add(new Item(10, "Chainsaw Arm", "A melee arm with a chance to cleanly remove a part.", ItemLoc.leftArm, ItemType.melee, 10, 10, 0, true));
		items.Add(new Item(11, "Machinegun Arm", "Fires bullets very quickly at foes.", ItemLoc.rightArm, ItemType.range, 10, 10, 0, false));
		items.Add(new Item(12, "Rollerblades (Left)", "Increased movement speed, also makes you look cool.", ItemLoc.leftLeg, ItemType.asthetic, 20, 1, 0, false));
		items.Add(new Item(13, "Rollerblades (Right)", "Increased movement speed, also makes you look cool.", ItemLoc.rightLeg, ItemType.asthetic, 20, 1, 0, false));
		items.Add(new Item(14, "Backpack", "Increases your max battery capacity.", ItemLoc.back, ItemType.asthetic, 20, 0, 0, true));
		items.Add(new Item(15, "Hacking AI", "Allows you to hack doors and computer systems.", ItemLoc.core, ItemType.asthetic, 0, 0, 0, true));
	}
}
