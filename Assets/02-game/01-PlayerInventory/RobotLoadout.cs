using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// NEEDS DATABASE PREFAB IN SCENE TO FUNCTION
public class RobotLoadout : MonoBehaviour {

	[Tooltip("This bool indicates whether the robot drops items")]
	[SerializeField] bool doesItDrop;
	[SerializeField] Color damageColor;
	enum RobotType { player, turret, enemy }
	[SerializeField] RobotType robotType;
	public float damageOffset = 1;
	public int[] hitPoints;
	public float[] power;
	public bool dead = false;
	public int dropOffset = 0;
	public Item[] loadout = new Item[7];

	[HideInInspector]
	public bool stopped, walk, stopWhileAttackingLeft, stopWhileAttackingRight;
	[HideInInspector]
	int basicDamage = 5;
	int basicSpeed = 5;
	bool dropped;
	
    PlayerController player;
	

	void Start()
	{
        player = FindObjectOfType<PlayerController>();
	}
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
		if(loadout[(int)ItemLoc.leftArm].itemType == ItemType.melee)
			RobotFunctions.MeleeAnimationSwap(this, (int)ItemLoc.leftArm);
		if (loadout[(int)ItemLoc.rightArm].itemType == ItemType.melee)
			RobotFunctions.MeleeAnimationSwap(this, (int)ItemLoc.rightArm);

		// Check for specials		
	}

	public void TakeDamage(int damage, bool stopAction)
	{
		stopped = stopAction;
		if (robotType == RobotType.player)
		{
			List<int> liveParts = new List<int>();
			//StartCoroutine(ChangeColor(transform.Find("Body").GetComponent<SpriteRenderer>()));
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
            if(!player.activeBlock && !player.activeDodge)
            {
                hitPoints[liveParts[rand]] -= damage;
            }
            else
            {
                //assigns a new damage value for damageText, CHANGE THIS ONCE WE IMPLEMENT ARMOR / SHIELD ARM
                damage = (damage / 3);
                hitPoints[liveParts[rand]] -= damage;
                print("damage blocked");
            }

			if (hitPoints[liveParts[rand]] < 0)
			{
				hitPoints[liveParts[rand]] = 0;
			}
		}
		else
		{
			hitPoints[(int)ItemLoc.body] -= damage;
		}
		if ((hitPoints[0] <= 0 && loadout[0].itemID != -1) || hitPoints[1] <= 0)
		{
			print("running this");
			StartDeath();
        }
		if (stopAction || stopped)
		{
			RobotArmsAnim[] anims = GetComponentsInChildren<RobotArmsAnim>();
			foreach (RobotArmsAnim a in anims)
			{
				a.DoneAttacking();
				a.StartHitStall();
			}
			RobotAnimationController roAn = GetComponent<RobotAnimationController>();
			if (robotType != RobotType.turret)
			{
				roAn.StartHitStall();
			}
		}
		// Creates damage text
		GameObject damageText = Instantiate(Resources.Load("DamageText"), transform.position, Quaternion.identity) as GameObject;
		damageText.GetComponent<DamageText>().DamageSetup(damage, damageColor, transform.position);		
	}

	private void StartDeath()
	{        
        // Add if player later
        if (doesItDrop && !dropped)
		{
			int itemID = RobotFunctions.DropByID(this, dropOffset);
			DropItem(itemID);
			if (itemID != -1)
			{
				int itemLoc = (int)Database.instance.items[itemID].itemLoc;
				GetComponent<RobotAnimationController>().RemoveSprite(itemLoc);
			}
			dropped = true;
		}			
		else
		{
			GameManager.instance.playerAlive = false;
		}
		Collider2D[] colliders = GetComponents<Collider2D>();
		foreach (Collider2D col in colliders)
		{
			col.enabled = false;
		}
		dead = true;
    }

	public static IEnumerator ChangeColor(SpriteRenderer sr)
	{
		Color currentColor = sr.color;
		sr.color = Color.red;
		yield return new WaitForSeconds(0.5f);
		sr.color = currentColor;
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

	public bool AreYouStopped()
	{
		if (stopWhileAttackingLeft)
		{
			return true;
		}
		if (stopWhileAttackingRight)
		{
			return true;
		}
		return false;
	}

	public void DestroyRobot()
	{
		if (robotType == RobotType.enemy)
		{
			RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();
			parentRoom.Invoke("CheckEnemies", 0.1f);
		}
		Destroy(gameObject);
	}
}
