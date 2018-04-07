using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	Database database;
	public Item thisItem = new Item();
	[SerializeField] int databaseItemID;
	[SerializeField] TextMesh text;


	void Start()
	{
		database = FindObjectOfType<Database>();
		IdentifyItem(database.items[databaseItemID]);
	}

	void Update()
	{
		text.text = thisItem.itemName;
	}

	public void IdentifyItem(Item item)
	{
		thisItem = item;
	}
}
