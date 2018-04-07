using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	Database database;
	public Item thisItem = new Item();

	void Start()
	{
		database = FindObjectOfType<Database>();
		IdentifyItem(database.items[11]);
	}

	public void IdentifyItem(Item item)
	{
		thisItem = item;
	}
}
