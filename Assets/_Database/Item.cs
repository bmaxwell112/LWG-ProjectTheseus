using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.DatabaseSystem {
	public enum ItemLoc {
		head = 0,
		body = 1,
		leftArm = 2,
		rightArm = 3,
		back = 5,
		core = 6,
		legs = 4,
		consumable = 7
	}
	public enum ItemType {
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
		public int itemRarity;
		public Sprite[] itemSprite;
		public int itemHitpoints;
		public int itemDamage;
		public float itemSpeed;
		public float itemPower;
		public bool itemSpecial;
		public AnimatorOverrideController itemAnim;
		public AudioClip[] itemSound;

		public Item (int id, string name, string desc, ItemLoc location, ItemType type, int rarity, Sprite[] sprite, int hp, int damage, float speed, float power, bool special, AnimatorOverrideController anim, AudioClip[] sound) {
			itemID = id;
			itemName = name;
			itemDesc = desc;
			itemLoc = location;
			itemType = type;
			itemRarity = rarity;
			itemSprite = sprite;
			itemHitpoints = hp;
			itemDamage = damage;
			itemSpeed = speed;
			itemPower = power;
			itemSpecial = special;
			itemAnim = anim;
			itemSound = sound;
		}
		public Item (int id, string name, string desc, ItemLoc location, ItemType type, int rarity, Sprite[] sprite, int hp, int damage, float speed, float power, bool special, AnimatorOverrideController anim) {
			itemID = id;
			itemName = name;
			itemDesc = desc;
			itemLoc = location;
			itemType = type;
			itemRarity = rarity;
			itemSprite = sprite;
			itemHitpoints = hp;
			itemDamage = damage;
			itemSpeed = speed;
			itemPower = power;
			itemSpecial = special;
			itemAnim = anim;
			itemSound = new AudioClip[3];
		}
		public Item (int id, string name, string desc, ItemLoc location, ItemType type, int rarity, Sprite[] sprite, int hp, int damage, float speed, float power, bool special) {
			itemID = id;
			itemName = name;
			itemDesc = desc;
			itemLoc = location;
			itemType = type;
			itemRarity = rarity;
			itemSprite = sprite;
			itemHitpoints = hp;
			itemDamage = damage;
			itemSpeed = speed;
			itemPower = power;
			itemSpecial = special;
			itemSound = new AudioClip[3];
		}
		public Item () {
			itemID = -1;
			itemSprite = null;
		}
	}
}