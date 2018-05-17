using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotFunctions {

	/* This function will take a value for damage to be dealt from the attacker
	 * as well as the hitpoints and loadout of the affected object and a bool for 
	 * if the effected object should be knocked back and one for if it drops an item. 
	 */
	public static void DealDamage(int damage, GameObject target)
	{
		if (target.GetComponent<RobotLoadout>())
		{
			target.GetComponent<RobotLoadout>().TakeDamage(damage);
		}
	}
	//public static IEnumerator CauseKnockback(int value, GameObject target, Transform origin)
	//{
	//
	//}
	
	public static void ReplaceDropPart(Drops drop, RobotLoadout player)
	{
		Item tempItem = IdentifyReplacePart(drop.thisItem, player);
		for (int i = 0; i < player.loadout.Length; i++)
		{
			if (player.loadout[i].itemLoc == drop.thisItem.itemLoc)
			{
				player.loadout[i] = drop.thisItem;
				drop.thisItem = tempItem;
				int tempHP = player.hitPoints[i];
				player.hitPoints[i] = drop.hitPoints;
				drop.hitPoints = tempHP;
				break;
			}
		}
	}
	static Item IdentifyReplacePart(Item item, RobotLoadout player)
	{
		Item tempItem = new Item();
		for (int i = 0; i < player.loadout.Length; i++)
		{
			if (player.loadout[i].itemLoc == item.itemLoc)
			{
				tempItem = player.loadout[i];
				RobotAnimationController.UpdatePlayerSprites = true;
				return tempItem;
			}
		}
		return tempItem;
	}

	public static int DropByID(GameObject player, RobotLoadout roLo)
	{
		GameManager.RandomDropModifier += 5;
		int dropItemID = -1;
		int rand = Random.Range(0, 100);
		if (rand <= 27 + GameManager.RandomDropModifier)
		{
			GameManager.RandomDropModifier = 0;
			List<int> avalibleItems = new List<int>();
			Item[] playerInv = player.GetComponent<RobotLoadout>().loadout;
			for (int i = 0; i < roLo.loadout.Length; i++)
			{
				// If the item is not one of the basics.
				if (roLo.loadout[i].itemID > 6)
				{
					// If the player doesn't have the item
					if (roLo.loadout[i].itemID != playerInv[i].itemID)
					{
						// add that items ID to a List
						avalibleItems.Add(roLo.loadout[i].itemID);
					}
				}
			}
			if (avalibleItems.Count > 0)
			{
				int rand2 = Random.Range(0, avalibleItems.Count);
				dropItemID = avalibleItems[rand2];
			}
		}
		return dropItemID;
	}	
}
