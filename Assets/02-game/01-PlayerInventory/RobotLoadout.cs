using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class RobotLoadout : MonoBehaviour {

	public int hitPoints = 50;

	public Item[] loadout = new Item[7];

	// Resets the player to basic loadout.
	public void InitializeLoadout(Item head, Item body, Item leftArm, Item rightArm, Item legs, Item back, Item core)
	{
		loadout[(int)ItemLoc.head] = head;
		loadout[(int)ItemLoc.body] = body;
		loadout[(int)ItemLoc.leftArm] = leftArm;
		loadout[(int)ItemLoc.rightArm] = rightArm;
		loadout[(int)ItemLoc.legs] = legs;
		loadout[(int)ItemLoc.back] = back;
		loadout[(int)ItemLoc.core] = core;
		hitPoints = body.itemHitpoints;
	}

	public void TakeDamage(int damage, Color color1, Color color2, bool kb)
	{
		hitPoints -= damage;
		SpriteRenderer body = transform.Find("Body").GetComponent<SpriteRenderer>();
		StartCoroutine(ChangeColor(body, color1, 0));
		StartCoroutine(ChangeColor(body, color2, 0.25f));
		DeathCheck();
		if (GetComponent<BasicEnemy>() && kb)
		{
			StartCoroutine(GetComponent<BasicEnemy>().EnemyKnockback());
		}
	}

	// TODO remove this later
	public IEnumerator ChangeColor(SpriteRenderer sr, Color color, float t)
	{
		yield return new WaitForSeconds(t);
		sr.color = color;
	}

	public void DeathCheck()
	{
		if (hitPoints <= 0)
		{
			hitPoints = 0;
			if (GetComponent<BasicEnemy>())
			{
				GetComponent<BasicEnemy>().EnemyDrop();
			}
			if (GetComponent<PlayerController>())
			{
				LevelManager.LOADLEVEL("02a Hub");
			}
			Destroy(gameObject);
		}		
	}

	public void ReplaceDropPart(Drops drop)
	{
		Item tempItem = IdentifyReplacePart(drop.thisItem);
		for (int i = 0; i < loadout.Length; i++)
		{
			if (loadout[i].itemLoc == drop.thisItem.itemLoc)
			{				
				loadout[i] = drop.thisItem;
				drop.thisItem = tempItem;
				if (loadout[i].itemSpecial)
				{
					//GetComponent<PlayerSpecial>().SpecialCheck(loadout[i]);
				}
				switch (loadout[i].itemLoc)
				{
					case ItemLoc.body:
						int tempHP = hitPoints;
						hitPoints = drop.hitPoints;
						drop.hitPoints = tempHP; 
						break;
					default:
						break;
				}
				break;
			}
		}
	}

	Item IdentifyReplacePart(Item item)
	{
		Item tempItem = new Item();
		for (int i = 0; i < loadout.Length; i++)
		{
			if (loadout[i].itemLoc == item.itemLoc)
			{
				tempItem = loadout[i];
				RobotAnimationController.UpdatePlayerSprites = true;
				return tempItem;
			}
		}
		return tempItem;
	}	
}
