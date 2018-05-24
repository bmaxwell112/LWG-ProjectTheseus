using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class RobotLoadout : MonoBehaviour {

	[Tooltip("This bool indicates whether the robot drops items")]
	[SerializeField] bool doesItDrop;
	public int[] hitPoints;
	public float[] power;
	[HideInInspector]
	public bool attackLeft, attackRight, walk;
	[HideInInspector]
	public int dropOffset = 0;
	int basicDamage = 5;
	int basicSpeed = 5;
	bool isPlayer;

	void Start()
	{
		isPlayer = GetComponent<PlayerController>();
	}

	public Item[] loadout = new Item[7];

	// Resets the player to basic loadout.
	public void InitializeLoadout(Item head, Item body, Item leftArm, Item rightArm, Item legs, Item back, Item core)
	{
		hitPoints = new int[7];
		power = new float[7];
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
		for (int i = 0; i < loadout.Length; i++)
		{
			power[i] = loadout[i].itemPower;
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
        RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();
        print("enemy died and checked");
        // Add if player later
        if (doesItDrop)
		{
			DropItem(RobotFunctions.DropByID(FindObjectOfType<PlayerController>().gameObject, this, dropOffset));
		}
		if (!isPlayer)
		{
			parentRoom.Invoke("CheckEnemies", 0.1f);
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
		if (dropItemInt != -1)
		{			
			Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;

			GameObject tempDrop = Instantiate(Resources.Load("Drops"), transform.position, Quaternion.identity, currentRoom) as GameObject;
			tempDrop.GetComponent<Drops>().databaseItemID = dropItemInt;
		}
	}
}
