using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Facing { upperLeft, left, lowerLeft, down, lowerRight, right, upperRight, up }

public class RobotFunctions {

	/* This function will take a value for damage to be dealt from the attacker
	 * as well as the hitpoints and loadout of the affected object and a bool for 
	 * if the effected object should be knocked back and one for if it drops an item. 
	 */
	public static void DealDamage(int damage, GameObject target, bool stopActions)
	{
		if (target.GetComponent<RobotLoadout>())
		{
			target.GetComponent<RobotLoadout>().TakeDamage(damage, stopActions);
		}
	}

	public static void ReplaceDropPart(Drops drop, RobotLoadout player)
	{
		Item tempItem = IdentifyReplacePart(drop.thisItem, player);
		for (int i = 0; i < player.loadout.Length; i++)
		{
			if (player.loadout[i].itemLoc == drop.thisItem.itemLoc)
			{
				player.loadout[i] = drop.thisItem;
				// switch players items heath with drops health
				drop.thisItem = tempItem;
				int tempHP = player.hitPoints[i];
				player.hitPoints[i] = drop.hitPoints;
				drop.hitPoints = tempHP;
				// switch players items power with drops power
				float tempPower = player.power[i];
				player.power[i] = drop.power;
				drop.power = tempPower;
				if (player.loadout[i].itemType == ItemType.melee)
				{
					MeleeAnimationSwap(player, i);
				}
				else if (player.loadout[i].itemType == ItemType.range)
				{
					RangeAnimationSwap(player, i);
				}
				if (player.loadout[i].itemSpecial)
				{
					Debug.Log("Checking Specials");
					player.GetComponent<PlayerSpecial>().ActivateSpecialPassive(player.loadout[i]);
				}				
				break;
			}
		}
	}
	public static void ReplacePart(Item item, RobotLoadout robot)
	{		
		for (int i = 0; i < robot.loadout.Length; i++)
		{
			if (robot.loadout[i].itemLoc == item.itemLoc)
			{
				robot.loadout[i] = item;
				robot.hitPoints[i] = item.itemHitpoints;
				// switch players items power with drops power				
				robot.power[i] = item.itemPower;
				if (robot.loadout[i].itemType == ItemType.melee)
				{
					MeleeAnimationSwap(robot, i);
				}
				break;
			}
		}
	}

	public static void MeleeAnimationSwap(RobotLoadout robot, int i)
	{
		MeleeWeapon mw = Database.instance.ItemsMeleeWeapon(robot.loadout[i]);
		RobotArmsAnim[] anim = robot.GetComponentsInChildren<RobotArmsAnim>();
		if (robot.loadout[i].itemLoc == ItemLoc.leftArm)
		{
			anim[0].SwapWeapons(mw.meleeWeaponAnim);			
		}
		else if (robot.loadout[i].itemLoc == ItemLoc.rightArm)
		{
			anim[1].SwapWeapons(mw.meleeWeaponAnim);			
		}
	}
	public static void RangeAnimationSwap(RobotLoadout robot, int i)
	{
		RobotArmsAnim[] anim = robot.GetComponentsInChildren<RobotArmsAnim>();
		if (robot.loadout[i].itemLoc == ItemLoc.leftArm)
		{
			anim[0].SwapWeapons(Database.instance.leftRange);
		}
		else if (robot.loadout[i].itemLoc == ItemLoc.rightArm)
		{
			anim[1].SwapWeapons(Database.instance.rightRange);
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

	public static int DropByID(RobotLoadout roLo, int dropOffset)
	{
		GameManager.RandomDropModifier += 5;		
		int dropItemID = -1;
		int rand = Random.Range(0, 100);
		if (rand <= 35 + GameManager.RandomDropModifier + dropOffset)
		{
			GameManager.RandomDropModifier = 0;
			List<int> avalibleItems = new List<int>();
			//Item[] playerInv = player.GetComponent<RobotLoadout>().loadout;
			for (int i = 0; i < roLo.loadout.Length; i++)
			{
				// If the item is not one of the basics.
				if (roLo.loadout[i].itemID > 6)
				{
					// If the player doesn't have the item TODO remove this maybe. 
					//if (roLo.loadout[i].itemID != playerInv[i].itemID)
					//{
						// add that items ID to a List
						avalibleItems.Add(roLo.loadout[i].itemID);
					//}
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

	public static Facing FacingDirection(Transform transformFacing)
	{
		if (transformFacing.eulerAngles.z >= 22.5f && transformFacing.eulerAngles.z < 67.5f)
		{
			// facing upper left
			return Facing.upperLeft;
		}
		else if (transformFacing.eulerAngles.z >= 67.5f && transformFacing.eulerAngles.z < 112.5f)
		{
			// facing left
			return Facing.left;
		}
		else if (transformFacing.eulerAngles.z >= 112.5f && transformFacing.eulerAngles.z < 157.5f)
		{
			// facing lower left
			return Facing.lowerLeft;
		}
		else if (transformFacing.eulerAngles.z >= 157.5f && transformFacing.eulerAngles.z < 202.5f)
		{
			// facing down
			return Facing.down;
		}
		else if (transformFacing.eulerAngles.z >= 202.5f && transformFacing.eulerAngles.z < 247.5f)
		{
			// facing lower right
			return Facing.lowerRight;
		}
		else if (transformFacing.eulerAngles.z >= 247.5f && transformFacing.eulerAngles.z < 292.5f)
		{
			// facing right
			return Facing.right;
		}
		else if (transformFacing.eulerAngles.z >= 292.5f && transformFacing.eulerAngles.z < 337.5f)
		{
			return Facing.upperRight;
		}
		else if (transformFacing.eulerAngles.z >= 337.5f || transformFacing.eulerAngles.z < 22.5f)
		{
			// facing up
			return Facing.up;
		}
		else
		{
			return Facing.up;
		}
	}
}


