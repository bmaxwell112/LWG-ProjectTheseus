using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Database : MonoBehaviour {

	public static Database instance = null;   //Static instance of Database which allows it to be accessed by any other script.

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
	public List<SpecialItems> specialItems = new List<SpecialItems>();
	public List<MeleeWeapon> meleeWeapons = new List<MeleeWeapon>();
	public AnimatorOverrideController leftRange, rightRange;

	// Use this for initialization
	void Start () {
		SetupItems();
		SetupRangedWeapons();
		SetupSpecialItems();
		SetupMeleeWeapons();
	}
	
	private void SetupItems()
	{
		items.Add(new Item(0, "Basic Head", "A head taken from the only schematics you have left.", ItemLoc.head, ItemType.aesthetic, 1, GetSprite("GreenGuy/head"), 10, 0, 0, 1, false));
		items.Add(new Item(1, "Basic Torso", "A torso taken from the only schematics you have left.", ItemLoc.body, ItemType.aesthetic, 1, GetSprite("GreenGuy/torso"), 50, 0, 0, 1, false));
		items.Add(new Item(2, "Basic Left Arm", "A left arm taken from the only schematics you have left.", ItemLoc.leftArm, ItemType.melee, 1, GetSprite("GreenGuy/arm"), 10, 5, 0, 1, false));
		items.Add(new Item(3, "Basic Right Arm", "A right arm taken from the only schematics you have left.", ItemLoc.rightArm, ItemType.melee, 1, GetSprite("GreenGuy/arm"), 10, 5, 0, 1, false));
		items.Add(new Item(4, "Basic Legs", "A pair of legs taken from the only schematics you have left.", ItemLoc.legs, ItemType.speed, 1, GetSprite("GreenGuy/leg"), 10, 0, 2, 1, false));
		items.Add(new Item(5, "Basic Back", "A back taken from the only schematics you have left.", ItemLoc.back, ItemType.aesthetic, 1, GetSprite("basicBackpack"), 20, 0, 0, 1, false));
		items.Add(new Item(6, "Basic AI Core", "An AI Core taken from the only schematics you have left.", ItemLoc.core, ItemType.aesthetic, 1, GetSprite("aiCore"), 0, 0, 0, 1, false));
		items.Add(new Item(7, "Reinforced Head", "A head created from modifying basic schematics.", ItemLoc.head, ItemType.aesthetic, 2, GetSprite("GreenGuy/head"), 20, 0, 0, 1, false));
		items.Add(new Item(8, "Reinforced Torso", "A torso created from modifying basic schematics.", ItemLoc.body, ItemType.aesthetic, 2, GetSprite("GreenGuy/torso"), 80, 0, 0, 1, false));
		items.Add(new Item(9, "Reinforced Legs", "A pair of legs created from modifying basic schematics.", ItemLoc.legs, ItemType.speed, 2, GetSprite("GreenGuy/leg"), 20, 0, 2, 1, false));
		items.Add(new Item(10, "Reinforced Back", "A back created from modifying basic schematics.", ItemLoc.back, ItemType.aesthetic, 2, GetSprite("basicBackpack"), 10, 0, 0, 1, false));
		items.Add(new Item(11, "Head Vulcans", "A head with mounted vulcans that fire as you attack.", ItemLoc.head, ItemType.range, 2, GetSprite("GreenGuy/head"), 10, 1, 0, 1, true));
		items.Add(new Item(12, "Heavy Torso", "A bulkier, armored body that is heavy and harder to move in.", ItemLoc.body, ItemType.aesthetic, 4, GetSprite("GreenGuy/torso"), 120, 0, 0, 1, true));
		items.Add(new Item(13, "Mounted Gun L", "A basic semi-automatic firearm for ranged combat.", ItemLoc.leftArm, ItemType.range, 2, GetSprite("machineGun"), 10, 3, 0, 1, false));
		items.Add(new Item(14, "Mounted Gun R", "A basic semi-automatic firearm for ranged combat.", ItemLoc.rightArm, ItemType.range, 2, GetSprite("machineGun"), 10, 3, 0, 1, false));
		items.Add(new Item(15, "Shotgun L", "A powerful firearm with restricted range and a cooldown between shots.", ItemLoc.leftArm, ItemType.range, 3, GetSprite("Secondary/armMachineGun"), 10, 2, 0, 1, false));
		items.Add(new Item(16, "Shotgun R", "A powerful firearm with restricted range and a cooldown between shots.", ItemLoc.rightArm, ItemType.range, 3,GetSprite("Secondary/armMachineGun"), 10, 2, 0, 1, false));
		items.Add(new Item(17, "Machinegun L", "A ranged weapon with a rapid rate of fire.", ItemLoc.leftArm, ItemType.range, 3, GetSprite("Secondary/armMachineGun"), 10, 1, 0, 1, false));
		items.Add(new Item(18, "Machinegun R", "A ranged weapon with a rapid rate of fire.", ItemLoc.rightArm, ItemType.range, 3, GetSprite("Secondary/armMachineGun"), 10, 1, 0, 1, false));
		items.Add(new Item(19, "Chair Flintlock", "A failed capsule launcher prototype, capsules expand into chairs immediately after firing. Does an unusual amount of damage.", ItemLoc.rightArm, ItemType.range, 5, GetSprite("Secondary/armMachineGun"), 10, 20, 0, 1, false));
		items.Add(new Item(20, "Shield L", "A defensive item that guards you from damage in the front.", ItemLoc.leftArm, ItemType.aesthetic, 3, GetSprite("GreenGuy/arm"), 20, 0, 0, 1, true));
		items.Add(new Item(21, "Shield R", "A defensive item that guards you from damage in the front.", ItemLoc.rightArm, ItemType.aesthetic, 3, GetSprite("GreenGuy/arm"), 20, 0, 0, 1, true));
		items.Add(new Item(22, "Blade L", "A melee weapon that deals decent damage in a swinging arc.", ItemLoc.leftArm, ItemType.melee, 2, GetSprite("armBlade/sprites"), 20, 10, 0, 1, false));
		items.Add(new Item(23, "Blade R", "A melee weapon that deals decent damage in a swinging arc.", ItemLoc.rightArm, ItemType.melee, 2, GetSprite("armBlade/sprites"), 20, 10, 0, 1, false));
		items.Add(new Item(24, "Stun Baton L", "A melee weapon that stuns enemies that get too close.", ItemLoc.leftArm, ItemType.melee, 3, GetSprite("stunBaton/sprites"), 20, 5, 0, 1, true));
		items.Add(new Item(25, "Stun Baton R", "A melee weapon that stuns enemies that get too close.", ItemLoc.rightArm, ItemType.melee, 3, GetSprite("stunBaton/sprites"), 20, 5, 0, 1, true));
		items.Add(new Item(26, "Chainsaw L", "A melee weapon that deals damage over time and cleaves parts.", ItemLoc.leftArm, ItemType.melee, 4, GetSprite("Secondary/arm"), 20, 6, 0, 1, true));
		items.Add(new Item(27, "Chainsaw R", "A melee weapon that deals damage over time and cleaves parts.", ItemLoc.rightArm, ItemType.melee, 4, GetSprite("Secondary/arm"), 20, 6, 0, 1, true));
		items.Add(new Item(28, "Bronze Club R", "A slow but massively powerful melee weapon.", ItemLoc.rightArm, ItemType.melee, 5,GetSprite("Secondary/arm"), 30, 30, 0, 1, false));
		items.Add(new Item(29, "Rollerblades", "A radical set of legs that let you move faster and look cool.", ItemLoc.legs, ItemType.speed, 3, GetSprite("Secondary/leg"), 10, 0, 3, 1, false));
		items.Add(new Item(30, "Energy Pack", "A backpack system that increases your energy capacity.", ItemLoc.back, ItemType.aesthetic, 3, GetSprite("basicBackpack"), 10, 0, 0, 1, true));
		items.Add(new Item(31, "Barrier Pack", "A backpack system that generates a weak, regenerating forcefield.", ItemLoc.back, ItemType.aesthetic, 4,GetSprite("basicBackpack"), 5, 0, 0, 1, true));
		items.Add(new Item(32, "Hacking Core", "An AI core that enables the hacking of doors and chests.", ItemLoc.core, ItemType.aesthetic, 3, GetSprite("hackingCore"), 0, 0, 0, 1, true));
		items.Add(new Item(33, "Self-Repair Core", "An AI core that utilizes nanobots to repair chassis damage.", ItemLoc.core, ItemType.aesthetic, 4, GetSprite("aiCore"), 0, 0, 0, 1, true));
	}
	void SetupRangedWeapons()
	{
		// id, itemID, speed, offset, life, spread, direction in degrees, rate of fire, powerUse
		rangedWeapons.Add(new RangedWeapon(0, 11, 10, 0, 1, 1, 0, 0.075f, 0.01f));
		rangedWeapons.Add(new RangedWeapon(1, 13, 10, 0.1f, 1, 1, 0, 0.2f, 0.01f));
		rangedWeapons.Add(new RangedWeapon(2, 14, 10, 0.1f, 1, 1, 0, 0.2f, 0.01f));
		rangedWeapons.Add(new RangedWeapon(3, 15, 10, 0.2f, 0.25f, 8, 20, 0.75f, 0.1f));
		rangedWeapons.Add(new RangedWeapon(4, 16, 10, 0.2f, 0.25f, 8, 20, 0.75f, 0.1f));
		rangedWeapons.Add(new RangedWeapon(5, 17, 15, 0.2f, 2, 1, 0, 0.075f, 0.01f));
		rangedWeapons.Add(new RangedWeapon(6, 18, 15, 0.2f, 2, 1, 0, 0.075f, 0.01f));
		rangedWeapons.Add(new RangedWeapon(6, 19, 10, 0.2f, 2, 1, 0, 1f, 0.01f));
	}
	void SetupSpecialItems()
	{
		specialItems.Add(new SpecialItems(0, 20, new SpecialProp[] { SpecialProp.shield }, 0, 0, 10, 0.25f));
		specialItems.Add(new SpecialItems(1, 21, new SpecialProp[] { SpecialProp.shield }, 0, 0, 10, 0.25f));
		specialItems.Add(new SpecialItems(2, 24, new SpecialProp[] { SpecialProp.stun }, 1f, 0, 0, 0.25f));
		specialItems.Add(new SpecialItems(3, 25, new SpecialProp[] { SpecialProp.stun }, 1f, 0, 0, 0.25f));
		specialItems.Add(new SpecialItems(4, 26, new SpecialProp[] { SpecialProp.bleed, SpecialProp.cleave }, 2f, 1, 0, 0.25f));
		specialItems.Add(new SpecialItems(5, 27, new SpecialProp[] { SpecialProp.bleed, SpecialProp.cleave }, 2f, 1, 0, 0.25f));
		specialItems.Add(new SpecialItems(6, 30, new SpecialProp[] { SpecialProp.powerBoost }, 0.5f, 0, 0, 0.05f));
		specialItems.Add(new SpecialItems(7, 33, new SpecialProp[] { SpecialProp.heal }, 1.5f, 1, 1, 0.05f));
	}
	void SetupMeleeWeapons()
	{
		meleeWeapons.Add(new MeleeWeapon(0, 2, 0.3f, 0.5f, false, false, GetAnim("GreenGuy/armLeft")));
		meleeWeapons.Add(new MeleeWeapon(1, 3, 0.3f, 0.5f, false, false, GetAnim("GreenGuy/armRight")));
		meleeWeapons.Add(new MeleeWeapon(2, 22, 1.5f, 2, true, true, GetAnim("armBlade/anim/BladeLeft")));
		meleeWeapons.Add(new MeleeWeapon(3, 23, 1.5f, 2, true, true, GetAnim("armBlade/anim/BladeRight")));
		meleeWeapons.Add(new MeleeWeapon(4, 24, 0.5f, 1, false, true, GetAnim("GreenGuy/armRight")));
		meleeWeapons.Add(new MeleeWeapon(5, 25, 0.5f, 1, false, true, GetAnim("GreenGuy/armLeft")));
		meleeWeapons.Add(new MeleeWeapon(6, 26, 0.5f, 1, true, false, GetAnim("GreenGuy/armRight")));
		meleeWeapons.Add(new MeleeWeapon(7, 27, 0.5f, 1, true, false, GetAnim("GreenGuy/armRight")));
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
	AnimatorOverrideController GetAnim(string name)
	{
		AnimatorOverrideController newAnim = Resources.Load(name) as AnimatorOverrideController;
		return newAnim;
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
	public MeleeWeapon ItemsMeleeWeapon(Item item)
	{
		MeleeWeapon mw = new MeleeWeapon();
		for (int i = 0; i < meleeWeapons.Count; i++)
		{
			if (meleeWeapons[i].meleeWeaponItemID == item.itemID)
			{
				mw = meleeWeapons[i];
				break;
			}
		}
		return mw;
	}
	public SpecialItems ItemSpecialItem(Item item)
	{
		SpecialItems si = new SpecialItems();
		for (int i = 0; i < specialItems.Count; i++)
		{
			if (specialItems[i].specialItemID == item.itemID)
			{
				si = specialItems[i];
				break;
			}
		}
		return si;
	}

	public List<Item> ItemsByLocation(ItemLoc loc)
	{
		List<Item> itemsOfLoc = new List<Item>();
		foreach (Item item in items)
		{
			if (item.itemLoc == loc)
			{
				itemsOfLoc.Add(item);
			}
		}
		return itemsOfLoc;
	}
}
