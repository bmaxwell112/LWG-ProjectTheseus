using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType{
	head,
	body,
	leftArm,
	rightArm,
	back,
	core,
	leftLeg,
	rightLeg,
	consumable
}

[System.Serializable]
public class Item {

	public int itemID;
	public string itemName;
	public string itemDesc;
	public ItemType itemType;
	//public Sprite itemSprite;
	public int itemHitpoints;
	public int itemAbilityValue;
	public bool itemSpecial;

	public Item(int id, string name, string desc, ItemType type, int hp, int value, bool special)
	{
		itemID = id;
		itemName = name;
		itemDesc = desc;
		itemType = type;
		//itemSprite = sprite;
		itemHitpoints = hp;
		itemAbilityValue = value;
		itemSpecial = special;
	}

	public Item()
	{
		itemID = -1;
	}
}
