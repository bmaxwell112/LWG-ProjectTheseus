using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class RobotLoadout : MonoBehaviour {

	public int[] hitPoints;
	public bool attackLeft, attackRight, walk;
	int basicDamage = 5;

	public Item[] loadout = new Item[7];

	// Resets the player to basic loadout.
	public void InitializeLoadout(Item head, Item body, Item leftArm, Item rightArm, Item legs, Item back, Item core)
	{
		hitPoints = new int[7];
		loadout[(int)ItemLoc.head] = head;
		loadout[(int)ItemLoc.body] = body;
		loadout[(int)ItemLoc.leftArm] = leftArm;
		loadout[(int)ItemLoc.rightArm] = rightArm;
		loadout[(int)ItemLoc.legs] = legs;
		loadout[(int)ItemLoc.back] = back;
		loadout[(int)ItemLoc.core] = core;
		for (int i = 0; i < loadout.Length; i++)
		{
			hitPoints[i] = loadout[i].itemHitpoints;
		}
		
	}

	public void TakeDamage(int damage, bool kb)
	{
		// Gets a list of live parts to see what can be damaged
		List<int> liveParts = new List<int>();
		for(int i=0; i< hitPoints.Length;i++)
		{
			if (hitPoints[i] > 0)
			{
				liveParts.Add(i);
				// twice as likely to hit everything but the head
				if (loadout[i].itemLoc != ItemLoc.head)
				{
					liveParts.Add(i);
				}
			}
		}
		int rand = Random.Range(0, liveParts.Count);

		hitPoints[liveParts[rand]] -= damage;
		if (hitPoints[liveParts[rand]] < 0)
		{
			hitPoints[liveParts[rand]] = 0;
		}
		DeathCheck();
		if ((GetComponent<BasicEnemy>() && kb))
		{
			StartCoroutine(GetComponent<BasicEnemy>().EnemyKnockback());
		}
		if ((GetComponent<RangeShortEnemy>() && kb))
		{
			StartCoroutine(GetComponent<RangeShortEnemy>().EnemyKnockback());
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
		if ((hitPoints[0] <= 0 && loadout[0].itemID == -1)|| hitPoints[1] <= 0)
		{
			hitPoints[0] = 0;
			hitPoints[1] = 0;
			if (GetComponent<BasicEnemy>())
			{
				GetComponent<BasicEnemy>().EnemyDrop();
			}
			if (GetComponent<RangeShortEnemy>())
			{
				GetComponent<RangeShortEnemy>().EnemyDrop();
			}
			if (GetComponent<PlayerController>())
			{
				print("Loaded this");
				LevelManager.LOADLEVEL("02a Game");
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
				int tempHP = hitPoints[i];
				hitPoints[i] = drop.hitPoints;
				drop.hitPoints = tempHP;
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
