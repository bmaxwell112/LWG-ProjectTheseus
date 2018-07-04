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
	[SerializeField] float distanceOffset = 0.435f;
	ItemPanel itemPanel;
	NotificationsPanel np;
	[SerializeField] SpriteRenderer[] sprites;
	[SerializeField] LayerMask playerMask;

	void Start()
	{
		Database database = Database.instance;
		IdentifyItem(database.items[databaseItemID], database.items[databaseItemID].itemHitpoints, database.items[databaseItemID].itemPower);
		itemPanel = FindObjectOfType<ItemPanel>();
		np = FindObjectOfType<NotificationsPanel>();
		//Invoke("DestroyDrop", 10);
	}
	void Update()
	{
		Collider2D playerInRange = Physics2D.OverlapCircle(transform.position, 0.55f, playerMask);
		if (playerInRange)
		{
			if (PlayerFacingItem(playerInRange))
			{
				if (!panelEnabled)
				{
					var newText = thisItem.itemName + "\n" + thisItem.itemDesc;
					itemPanel.ItemPanelSetEnable(newText);
					panelEnabled = true;
				}
				bool pickup = Input.GetButtonDown("Pickup");
				if(pickup)
				{
					PlayerPickupDrop(playerInRange.gameObject);
				}
			}
			else
			{
				if (panelEnabled)
				{
					itemPanel.ItemPanelDisabled();
					panelEnabled = false;
				}
			}
		}
		else
		{
			if (panelEnabled)
			{
				itemPanel.ItemPanelDisabled();
				panelEnabled = false;
			}
		}
	}

	private void PlayerPickupDrop(GameObject player)
	{
		RobotLoadout playerLo = player.GetComponent<RobotLoadout>();
		string newText = playerLo.loadout[(int)thisItem.itemLoc].itemName + "\nSwitched for\n" + thisItem.itemName;
		np.NotificationsPanelSetEnable(newText);
		if (TutorialFunctions.instance)
		{
			if (thisItem.itemType == ItemType.range)
			{
				TutorialFunctions.instance.DialogueTriggerValue(4);
			}
			if (thisItem.itemLoc == ItemLoc.core || thisItem.itemLoc == ItemLoc.back)
			{
				TutorialFunctions.instance.DialogueTriggerValue(5);
			}
		}
		RobotFunctions.ReplaceDropPart(this, playerLo);
		RenameAndReset();		
	}

	private bool PlayerFacingItem(Collider2D col)
	{
		var player = col.GetComponent<PlayerController>();
		// Above and to the right of Item
		if (player.transform.position.x > transform.position.x && player.transform.position.y - distanceOffset > transform.position.y)
		{

			if (player.firingArc.eulerAngles.z > 80 && player.firingArc.eulerAngles.z <= 190)
			{
				return true;
			}
		}
		// Above and to the left of Item
		if (player.transform.position.x <= transform.position.x && player.transform.position.y - distanceOffset > transform.position.y)
		{

			if (player.firingArc.eulerAngles.z > 170 && player.firingArc.eulerAngles.z <= 280)
			{
				return true;
			}
		}
		// Below and to the right of Item
		if (player.transform.position.x > transform.position.x && player.transform.position.y - distanceOffset <= transform.position.y)
		{

			if (player.firingArc.eulerAngles.z >= 0 && player.firingArc.eulerAngles.z <= 100)
			{
				return true;
			}
			if (player.firingArc.eulerAngles.z > 350 && player.firingArc.eulerAngles.z <= 360)
			{
				return true;
			}
		}
		// Below and to the left of Item
		if (player.transform.position.x <= transform.position.x && player.transform.position.y - distanceOffset <= transform.position.y)
		{

			if (player.firingArc.eulerAngles.z > 260 && player.firingArc.eulerAngles.z <= 360)
			{
				return true;
			}
			if (player.firingArc.eulerAngles.z >= 0 && player.firingArc.eulerAngles.z <= 10)
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
		if (TutorialFunctions.instance)
		{
			TutorialFunctions.instance.DialogueTriggerValue(3);
		}
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
