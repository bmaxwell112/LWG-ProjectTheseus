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

	public List<Item> items;// = new List<Item>();
	public List<RangedWeapon> rangedWeapons = new List<RangedWeapon>();
	public List<SpecialItems> specialItems = new List<SpecialItems>();
	public List<MeleeWeapon> meleeWeapons = new List<MeleeWeapon>();
	public AnimatorOverrideController leftRange, rightRange;
	[SerializeField] TextAsset itemsJson;

	// Use this for initialization
	void Start() {
		items = jsonManager.ParseJsonToItems(itemsJson);
		SetupRangedWeapons();
		SetupSpecialItems();
		SetupMeleeWeapons();
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
		specialItems.Add(new SpecialItems(0, 20, new SpecialProp[] { SpecialProp.shield }, 0, 0, 5, 0.025f));
		specialItems.Add(new SpecialItems(1, 21, new SpecialProp[] { SpecialProp.shield }, 0, 0, 5, 0.025f));
		specialItems.Add(new SpecialItems(2, 24, new SpecialProp[] { SpecialProp.stun }, 1f, 0, 0, 0.25f));
		specialItems.Add(new SpecialItems(3, 25, new SpecialProp[] { SpecialProp.stun }, 1f, 0, 0, 0.25f));
		specialItems.Add(new SpecialItems(4, 26, new SpecialProp[] { SpecialProp.bleed, SpecialProp.cleave }, 2f, 1, 0, 0.25f));
		specialItems.Add(new SpecialItems(5, 27, new SpecialProp[] { SpecialProp.bleed, SpecialProp.cleave }, 2f, 1, 0, 0.25f));
		specialItems.Add(new SpecialItems(6, 30, new SpecialProp[] { SpecialProp.powerBoost }, 0.5f, 0, 0, 0.05f));
		specialItems.Add(new SpecialItems(7, 33, new SpecialProp[] { SpecialProp.heal }, 1.5f, 1, 1, 0.05f));
		specialItems.Add(new SpecialItems(8, 31, new SpecialProp[] { SpecialProp.shield }, 0, 0, 5, 0.05f));
	}
	void SetupMeleeWeapons()
	{
		meleeWeapons.Add(new MeleeWeapon(0, 2, 0.3f, 0.5f, false, false));
		meleeWeapons.Add(new MeleeWeapon(1, 3, 0.3f, 0.5f, false, false));
		meleeWeapons.Add(new MeleeWeapon(2, 22, 1.5f, 2, true, true));
		meleeWeapons.Add(new MeleeWeapon(3, 23, 1.5f, 2, true, true));
		meleeWeapons.Add(new MeleeWeapon(4, 24, 0.5f, 1, false, true));
		meleeWeapons.Add(new MeleeWeapon(5, 25, 0.5f, 1, false, true));
		meleeWeapons.Add(new MeleeWeapon(6, 26, 0.5f, 1, true, false));
		meleeWeapons.Add(new MeleeWeapon(7, 27, 0.5f, 1, true, false));
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

	AudioClip GetSound(string name)
	{
		var newSound = Resources.Load(name) as AudioClip;
		return newSound;
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

	public Item ItemFromJSON(jsonManager.StingItems jsonItem)
	{
		ItemLoc newLoc = (ItemLoc)System.Enum.Parse(typeof(ItemLoc), jsonItem.itemLoc);
		ItemType newType = (ItemType)System.Enum.Parse(typeof(ItemType), jsonItem.itemType);
		Item item;
		if (jsonItem.itemAnim != "" && jsonItem.itemSound != "")
		{
			item = new Item(
			jsonItem.itemID,
			jsonItem.itemName,
			jsonItem.itemDesc,
			newLoc,
			newType,
			jsonItem.itemRarity,
			GetSprite(jsonItem.itemSprite),
			jsonItem.itemHitpoints,
			jsonItem.itemDamage,
			jsonItem.itemSpeed,
			jsonItem.itemPower,
			jsonItem.itemSpecial,
			GetAnim(jsonItem.itemAnim),
			GetSound(jsonItem.itemSound));
		}
		else if (jsonItem.itemAnim != "" && jsonItem.itemSound == "")
		{
			item = new Item(
			jsonItem.itemID,
			jsonItem.itemName,
			jsonItem.itemDesc,
			newLoc,
			newType,
			jsonItem.itemRarity,
			GetSprite(jsonItem.itemSprite),
			jsonItem.itemHitpoints,
			jsonItem.itemDamage,
			jsonItem.itemSpeed,
			jsonItem.itemPower,
			jsonItem.itemSpecial,
			GetAnim(jsonItem.itemAnim));
		}
		else
		{
			item = new Item(
			jsonItem.itemID,
			jsonItem.itemName,
			jsonItem.itemDesc,
			newLoc,
			newType,
			jsonItem.itemRarity,
			GetSprite(jsonItem.itemSprite),
			jsonItem.itemHitpoints,
			jsonItem.itemDamage,
			jsonItem.itemSpeed,
			jsonItem.itemPower,
			jsonItem.itemSpecial,
			GetAnim(jsonItem.itemAnim),
			GetSound(jsonItem.itemSound));
		}
		return item;
	}

	public bool ArmIsambidextrous(Item item)
	{
		if (item.itemID + 1 <= items.Count && item.itemID - 1 >= 0)
		{
			if (item.itemName == items[item.itemID - 1].itemName || item.itemName == items[item.itemID + 1].itemName)
				return true;
			else
				return false;
		}
		else
			return false;		
	}

	public Item GetArmByLocation(Item item, ItemLoc loc)
	{
		if (item.itemLoc == loc)
		{
			return item;
		}		
		else
		{
			if (ArmIsambidextrous(item))
			{
				if (item.itemName == items[item.itemID - 1].itemName)
				{
					return (items[item.itemID - 1]);
				}
				else
				{
					return (items[item.itemID + 1]);
				}
			}
			else
			{
				return new Item();
			}
		}
	}
}
