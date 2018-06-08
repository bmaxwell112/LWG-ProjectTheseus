using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drops : MonoBehaviour {

	public Item thisItem = new Item();
	public int databaseItemID;
	public int hitPoints;
	public float power;
	bool playerCanPickup, panelEnabled;
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
	void FixedUpdate()
	{
		Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, 0.25f, playerMask);
		if (playerInRange)
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
	}

	void DestroyDrop()
	{
		Destroy(gameObject);
	}
}
