using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {

	public Item thisItem = new Item();
	public int databaseItemID;
	public int hitPoints;
	bool playerCanPickup;
	[SerializeField] SpriteRenderer sprite;
	[SerializeField] Text text;
	[SerializeField] Transform canvas;
	[SerializeField] LayerMask playerMask;

	void Start()
	{
		Database database = Database.instance;
		IdentifyItem(database.items[databaseItemID], database.items[databaseItemID].itemHitpoints);
		Invoke("DestroyDrop", 10);
	}

	void Update()
	{		
		if (playerCanPickup && !canvas.gameObject.activeSelf)
		{
			canvas.gameObject.SetActive(true);
		}
		if (!playerCanPickup && canvas.gameObject.activeSelf)
		{
			canvas.gameObject.SetActive(false);
		}
	}
	void FixedUpdate()
	{
		Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, 1, playerMask);
		if (playerInRange)
		{
			DescPosition(playerInRange.transform);
			playerCanPickup = true;
		}
		else
		{
			playerCanPickup = false;
		}
	}

	void IdentifyItem(Item item, int hp)
	{		
		thisItem = item;
		hitPoints = hp;
		sprite.sprite = item.itemSprite[0];
		text.text = thisItem.itemName + "\n" + thisItem.itemDesc;
	}
	public void RenameAndReset()
	{
		CancelInvoke();
		Invoke("DestroyDrop", 10);
		text.text = thisItem.itemName + "\n" + thisItem.itemDesc;
		sprite.sprite = thisItem.itemSprite[0];
	}

	void DestroyDrop()
	{
		Destroy(gameObject);
	}

	void DescPosition(Transform player)
	{
		if (player.transform.position.x < transform.position.x)
		{
			canvas.transform.localPosition = new Vector3(2.5f, 1.25f, 0);
		}
		else
		{
			canvas.transform.localPosition = new Vector3(-2.5f, 1.25f, 0);
		}
	}
}
