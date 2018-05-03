using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour {

	public static Database instance = null;   //Static instance of Database which allows it to be accessed by any other script.
	[SerializeField] TextAsset csvFile;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}

	public List<Item> items = new List<Item>();
	public List<RangedWeapon> rangedWeapons = new List<RangedWeapon>();

	// Use this for initialization
	void Start () {
		SetupItems();
		SetupRangedWeapons();
	}
	private void SetupItems()
	{
		items.Add(new Item(0, "Basic Head", "A head taken from the only schematics you have left.", ItemLoc.head, ItemType.aesthetic, GetSprite("GreenGuy/head"), 10, 0, 0, 0, false));
		items.Add(new Item(1, "Basic Torso", "A torso taken from the only schematics you have left.", ItemLoc.body, ItemType.aesthetic, GetSprite("GreenGuy/torso"), 50, 0, 0, 0, false));
		items.Add(new Item(2, "Basic Left Arm", "A left arm taken from the only schematics you have left.", ItemLoc.leftArm, ItemType.melee, GetSprite("GreenGuy/arm"), 10, 5, 0, 0, false));
		items.Add(new Item(3, "Basic Right Arm", "A right arm taken from the only schematics you have left.", ItemLoc.rightArm, ItemType.melee, GetSprite("GreenGuy/arm"), 10, 5, 0, 0, false));
		items.Add(new Item(4, "Basic Legs", "A pair of legs taken from the only schematics you have left.", ItemLoc.legs, ItemType.speed, GetSprite("GreenGuy/leg"), 10, 0, 2, 0, false));
		items.Add(new Item(5, "Basic Back", "A back taken from the only schematics you have left.", ItemLoc.back, ItemType.aesthetic, GetSprite("GreenGuy/head"), 20, 0, 0, 0, false));
		items.Add(new Item(6, "Basic AI Core", "An AI Core taken from the only schematics you have left.", ItemLoc.core, ItemType.aesthetic, GetSprite("GreenGuy/head"), 0, 0, 0, 0, false));
		items.Add(new Item(7, "Head Vulcans", "A head with vulcan guns above the eyes.", ItemLoc.head, ItemType.range, GetSprite("GreenGuy/head"), 10, 2, 0, 0, false));
		items.Add(new Item(8, "Heavy Torso", "A bulkier body capable of taking more punishment.", ItemLoc.body, ItemType.aesthetic, GetSprite("GreenGuy/torso"), 80, 0, 0, 0, false));
		items.Add(new Item(9, "Chainsaw Arm L", "A melee arm with a chance to cleanly remove a part.", ItemLoc.leftArm, ItemType.melee, GetSprite("Secondary/arm"), 10, 10, 0, 0, false));
		items.Add(new Item(10, "Chainsaw Arm R", "A melee arm with a chance to cleanly remove a part.", ItemLoc.rightArm, ItemType.melee, GetSprite("Secondary/arm"), 10, 10, 0, 0, false));
		items.Add(new Item(11, "Machinegun Arm L", "Fires bullets very quickly at foes.", ItemLoc.leftArm, ItemType.range, GetSprite("Secondary/armMachineGun"), 10, 1, 0, 0, false));
		items.Add(new Item(12, "Machinegun Arm R", "Fires bullets very quickly at foes.", ItemLoc.rightArm, ItemType.range, GetSprite("Secondary/armMachineGun"), 10, 1, 0, 0, false));
		items.Add(new Item(13, "Rollerblades", "Increased movement speed, also makes you look cool.", ItemLoc.legs, ItemType.speed, GetSprite("Secondary/leg"), 20, 0, 3, 0, false));
		items.Add(new Item(14, "Backpack", "Increases your max battery capacity.", ItemLoc.back, ItemType.aesthetic, GetSprite("GreenGuy/head"), 20, 0, 0, 0, false));
		items.Add(new Item(15, "Hacking AI", "Allows you to hack doors and computer systems.", ItemLoc.core, ItemType.aesthetic, GetSprite("GreenGuy/head"), 0, 0, 0, 0, false));
	}
	void SetupRangedWeapons()
	{
		rangedWeapons.Add(new RangedWeapon(0, 8, 10, 0, 1, 1, 0, 0.075f));
		rangedWeapons.Add(new RangedWeapon(1, 11, 10, 0.2f, 1, 1, 0, 0.075f));
		rangedWeapons.Add(new RangedWeapon(2, 12, 10, 0.2f, 1, 1, 0, 0.075f));
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

	public Item SudoRandomItemOut(ItemLoc loc, int[] allowed)
	{
		Item randItem = new Item();		
		List<int> locItems = new List<int>();
		for (int i = 0; i < items.Count; i++) // for all items
		{
			if (items[i].itemLoc == loc) // if they have the provided location
			{
				for (int j = 0; j < allowed.Length; j++)
				{
					if (items[i].itemID == allowed[j]) // if the id is allowed
					{
						locItems.Add(items[i].itemID);
						if (items[i].itemID < 8) // if it's basic add it two more times.
						{
							locItems.Add(items[i].itemID);
							locItems.Add(items[i].itemID);
						}
					}
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

	Sprite[] GetSprite(string name)
	{
		var newSprites = Resources.LoadAll(name, typeof(Sprite)).Cast<Sprite>().ToArray();
		return newSprites;

	}

	public RangedWeapon ItemsRangedWeapon(Item item)
	{
		RangedWeapon rw = new RangedWeapon();
		for (int i = 0; i < rangedWeapons.Count; i++)
		{
			if (rangedWeapons[i].rangedWeaponItemID == item.itemID)
			{
				rw = rangedWeapons[i];
				break;
			}
		}
		return rw;
	}
}
