using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class RobotLoadout : MonoBehaviour {

	[Tooltip("This bool indicates whether the robot drops items")]
	[SerializeField] bool doesItDrop;
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

	public void TakeDamage(int damage)
	{
		// Gets a list of live parts to see what can be damaged
		List<int> liveParts = new List<int>();
		for (int i = 0; i < hitPoints.Length; i++)
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
		if ((hitPoints[0] <= 0 && loadout[0].itemID != -1) || hitPoints[1] <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		// Add if player later
		if (doesItDrop)
		{
			DropItem(RobotFunctions.DropByID(FindObjectOfType<PlayerController>().gameObject, this));
		}
		if (!GetComponent<PlayerController>())
		{
			Destroy(gameObject);
		}
		else
		{
			GameManager.playerAlive = false;
			LevelManager.LOADLEVEL("02a Hub");
		}
	}

	public static IEnumerator ChangeColor(SpriteRenderer sr, Color color, float t)
	{
		yield return new WaitForSeconds(t);
		sr.color = color;
	}

	public void DropItem(int dropItemInt)
	{
		print("tried to drop at " + (GameManager.RandomDropModifier + 27));
		if (dropItemInt != -1)
		{
			print("Dropped");
			GameObject tempDrop = Instantiate(Resources.Load("Drops"), transform.position, Quaternion.identity) as GameObject;
			tempDrop.GetComponent<Drops>().databaseItemID = dropItemInt;
		}
	}
}
