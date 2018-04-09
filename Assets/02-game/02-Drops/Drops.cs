using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	Database database;
	public Item thisItem = new Item();
	public int databaseItemID;
	public int hitPoints;
	[SerializeField] TextMesh text;

	void Start()
	{
		database = FindObjectOfType<Database>();
		IdentifyItem(database.items[databaseItemID], database.items[databaseItemID].itemHitpoints);		
	}

	void Update()
	{
		text.text = thisItem.itemName;
	}

	public void IdentifyItem(Item item, int hp)
	{
		thisItem = item;
		hitPoints = hp;
		CancelInvoke();
		Invoke("DestroyDrop", 15);
	}

	void DestroyDrop()
	{
		Destroy(gameObject);
	}
}
