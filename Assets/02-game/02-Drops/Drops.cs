using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {

	public Item thisItem = new Item();
	public int databaseItemID;
	public int hitPoints;
	public float power;
	public bool playerCanPickup;
	bool panelEnabled;
	ItemPanel itemPanel;
	[SerializeField] SpriteRenderer[] sprites;
	[SerializeField] LayerMask playerMask;

	void Start()
	{
		Database database = Database.instance;
		IdentifyItem(database.items[databaseItemID], database.items[databaseItemID].itemHitpoints, database.items[databaseItemID].itemPower);
		itemPanel = FindObjectOfType<ItemPanel>();
		//Invoke("DestroyDrop", 10);
	}
	void Update()
	{
		Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, 0.4f, playerMask);
		if (playerInRange)
		{
			print(thisItem.itemName + " can pickup " + PlayerFacingItem(playerInRange));
			if (PlayerFacingItem(playerInRange))
			{
				if (!panelEnabled)
				{
					var newText = thisItem.itemName + "\n" + thisItem.itemDesc;
					itemPanel.ItemPanelSetEnable(newText);
					panelEnabled = true;
				}
				playerCanPickup = true;
			}
			else
			{
				if (panelEnabled)
				{
					itemPanel.ItemPanelDisabled();
					panelEnabled = false;
				}
				playerCanPickup = false;
			}
		}
		else
		{
			if (panelEnabled)
			{
				itemPanel.ItemPanelDisabled();
				panelEnabled = false;
			}
			playerCanPickup = false;
		}
	}

	private bool PlayerFacingItem(Collider2D col)
	{
		var player = col.GetComponent<PlayerController>();
		// Above and to the right of Item
		if (player.transform.position.x > transform.position.x && player.transform.position.y - 0.45f > transform.position.y)
		{
			if (player.firingArc.eulerAngles.z > 80 && player.firingArc.eulerAngles.z <= 190)
			{
				return true;
			}
		}
		// Above and to the left of Item
		if (player.transform.position.x <= transform.position.x && player.transform.position.y - 0.45f > transform.position.y)
		{
			if (player.firingArc.eulerAngles.z > 170 && player.firingArc.eulerAngles.z <= 280)
			{
				return true;
			}
		}
		// Below and to the right of Item
		if (player.transform.position.x > transform.position.x && player.transform.position.y - 0.45f <= transform.position.y)
		{
			if (player.firingArc.eulerAngles.z >= 0 && player.firingArc.eulerAngles.z <= 100 ||
				player.firingArc.eulerAngles.z > 350 && player.firingArc.eulerAngles.z <= 360)
			{
				return true;
			}
		}
		// Below and to the left of Item
		if (player.transform.position.x <= transform.position.x && player.transform.position.y - 0.45f <= transform.position.y)
		{
			if (player.firingArc.eulerAngles.z > 260 && player.firingArc.eulerAngles.z <= 360 ||
				player.firingArc.eulerAngles.z >= 0 && player.firingArc.eulerAngles.z <= 10)
			{
				return true;
			}
		}
		return false;
	}

	void IdentifyItem(Item item, int hp, float pwr)
	{
		thisItem = item;
		hitPoints = hp;
		power = pwr;
		SpriteSetter(item);
	}

	private void SpriteSetter(Item item)
	{
		sprites[1].sprite = null;
		sprites[2].sprite = null;
		switch (item.itemLoc)
		{
			case ItemLoc.leftArm:
				sprites[0].sprite = item.itemSprite[0];				
				sprites[1].sprite = item.itemSprite[1];
				break;
			case ItemLoc.rightArm:
				sprites[0].sprite = item.itemSprite[0];				
				sprites[1].sprite = item.itemSprite[1];
				break;
			case ItemLoc.legs:
				sprites[0].sprite = item.itemSprite[0];				
				sprites[2].sprite = item.itemSprite[0];
				break;
			default:
				sprites[0].sprite = item.itemSprite[0];				
				break;

		}
	}

	public void RenameAndReset()
	{
		CancelInvoke();
		//Invoke("DestroyDrop", 10);
		SpriteSetter(thisItem);
		var newText = thisItem.itemName + "\n" + thisItem.itemDesc;
		itemPanel.ItemPanelSetEnable(newText);
	}

	void DestroyDrop()
	{
		Destroy(gameObject);
	}
}
