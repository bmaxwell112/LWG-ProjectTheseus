using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.DatabaseSystem;

public class jsonManager : MonoBehaviour {

	List<StingItems> newItems;

	public static List<Item> ParseJsonToItems(TextAsset json)
	{
		var wrappedjsonArray = JsonUtility.FromJson<Items>(json.text);
		List<Item> newItems = new List<Item>();
		foreach (StingItems item in wrappedjsonArray.objects)
		{
			newItems.Add(Database.instance.ItemFromJSON(item));
		}
		return newItems;
	}

	[System.Serializable]
	public class Items
	{
		public List<StingItems> objects;
	}

	[System.Serializable]
	public class StingItems
	{
		public int itemID;
		public string itemName;
		public string itemDesc;
		public string itemLoc;
		public string itemType;
		public int itemRarity;
		public string itemSprite;
		public int itemHitpoints;
		public int itemDamage;
		public float itemSpeed;
		public float itemPower;
		public bool itemSpecial;
		public string itemAnim;
		public string itemSound;
	}
}
