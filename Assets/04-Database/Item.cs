using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemLoc{
	head = 0,
	body = 1,
	leftArm = 2,
	rightArm = 3,
	back = 5,
	core = 6,
	legs = 4,
	consumable = 7
}
public enum ItemType
{
	aesthetic,
	melee,
	range,
	speed
}

[System.Serializable]
public class Item {

	public int itemID;
	public string itemName;
	public string itemDesc;
	public ItemLoc itemLoc;
	public ItemType itemType;
	//public Sprite itemSprite;
	public int itemHitpoints;
	public int itemValue;
	public int itemValueTwo;
	public bool itemSpecial;

	public Item(int id, string name, string desc, ItemLoc location, ItemType type, int hp, int value, int valueTwo, bool special)
	{
		itemID = id;
		itemName = name;
		itemDesc = desc;
		itemLoc = location;
		itemType = type;
		//itemSprite = sprite;
		itemHitpoints = hp;
		itemValue = value;
		itemValueTwo = valueTwo;
		itemSpecial = special;
	}

	public Item()
	{
		itemID = -1;
	}
}
