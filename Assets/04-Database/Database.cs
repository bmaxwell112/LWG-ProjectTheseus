using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Database : MonoBehaviour {

	public List<Item> items = new List<Item>();

	// Use this for initialization
	void Start () {
		items.Add(new Item(0, "Basic Head", "A head taken from the only schematics you have left.", ItemType.head, 10, 0, false));
		items.Add(new Item(1, "Basic Torso", "A torso taken from the only schematics you have left.", ItemType.body, 50, 0, false));
		items.Add(new Item(2, "Basic Left Arm", "A left arm taken from the only schematics you have left.", ItemType.leftArm, 10, 5, false));
		items.Add(new Item(3, "Basic Right Arm", "A right arm taken from the only schematics you have left.", ItemType.rightArm, 10, 5, false));
		items.Add(new Item(4, "Basic Left Leg", "A left leg taken from the only schematics you have left.", ItemType.leftLeg, 10, 0, false));
		items.Add(new Item(5, "Basic Right Leg", "A right leg taken from the only schematics you have left.", ItemType.rightLeg, 10, 0, false));
		items.Add(new Item(6, "Basic Back", "A back taken from the only schematics you have left.", ItemType.back, 20, 0, false));
		items.Add(new Item(7, "Basic AI Core", "An AI Core taken from the only schematics you have left.", ItemType.core, 0, 0, false));
		items.Add(new Item(8, "Head Vulcans", "A head with vulcan guns above the eyes.", ItemType.head, 10, 0, true));
		items.Add(new Item(9, "Heavy Torso", "A bulkier body capable of taking more punishment.", ItemType.body, 80, 0, false));
		items.Add(new Item(10, "Chainsaw Arm", "A melee arm with a chance to cleanly remove a part.", ItemType.leftArm, 10, 10, true));
		items.Add(new Item(11, "Machinegun Arm", "Fires bullets very quickly at foes.", ItemType.rightArm, 10, 10, false));
		items.Add(new Item(12, "Rollerblades (Left)", "Increased movement speed, also makes you look cool.", ItemType.leftLeg, 20, 1, false));
		items.Add(new Item(13, "Rollerblades (Right)", "Increased movement speed, also makes you look cool.", ItemType.rightLeg, 20, 1, false));
		items.Add(new Item(14, "Backpack", "Increases your max battery capacity.", ItemType.back, 20, 0, true));
		items.Add(new Item(15, "Hacking AI", "Allows you to hack doors and computer systems.", ItemType.core, 0, 0, true));
	}
}
