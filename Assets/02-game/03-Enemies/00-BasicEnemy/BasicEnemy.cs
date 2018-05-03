using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	Transform player;
	bool knockback, attacking;
	[SerializeField] float knockbackDistance;
	[SerializeField] GameObject drop;
	[SerializeField] LayerMask playerMask;
	[SerializeField] Transform firingArc;
	RobotLoadout roLo;
	int attack;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		roLo = GetComponent<RobotLoadout>();
		BasicEnemySetup();
		attack = Mathf.RoundToInt((roLo.loadout[(int)ItemLoc.rightArm].itemDamage + roLo.loadout[(int)ItemLoc.leftArm].itemDamage) / 2);
		roLo.hitPoints = Mathf.RoundToInt(roLo.hitPoints / 2);
	}

	private void BasicEnemySetup()
	{
		Database db = Database.instance;
		roLo.InitializeLoadout(
			db.RandomItemOut(ItemLoc.head),
			db.RandomItemOut(ItemLoc.body),
			db.SudoRandomItemOut(ItemLoc.leftArm, new int[] { 2, 9 }),
			db.SudoRandomItemOut(ItemLoc.rightArm, new int[] { 3, 10 }),
			db.SudoRandomItemOut(ItemLoc.legs, new int[] { 4 } ),
			db.RandomItemOut(ItemLoc.back),
			db.RandomItemOut(ItemLoc.core)
			);
	}

	// Update is called once per frame
	void Update ()
	{
		if (RoomManager.SpawningComplete)
		{			
			if (player)
			{
				DefineRotation();
				EnemyMovement();
			}
		}
	}
	void FixedUpdate()
	{
		if (!attacking)
		{
			EnemyAttackCheck();
		}
	}

	private void EnemyMovement()
	{
		if (!knockback)
		{		
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, roLo.loadout[(int)ItemLoc.legs].itemSpeed * Time.deltaTime);
		}
	}

	private void EnemyAttackCheck()
	{
		RaycastHit2D hit = Physics2D.Raycast(firingArc.transform.position, transform.right, 1f, playerMask);
		if (hit.collider != null)
		{
			attacking = true;
			Invoke("EnemyAttack", 0.5f);
		}
	}
	private void EnemyAttack()
	{
		RaycastHit2D hit = Physics2D.Raycast(firingArc.transform.position, transform.right, 1f, playerMask);
		if (hit.collider != null)
		{
			hit.collider.gameObject.GetComponent<RobotLoadout>().TakeDamage(attack, Color.red, Color.white, false);
		}
		attacking = false;
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();

		firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
	}

	public void EnemyDrop()
	{
		int rand = Random.Range(0, 100);
		if (rand <= 33)
		{
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
				GameObject tempDrop = Instantiate(drop, transform.position, Quaternion.identity) as GameObject;
				tempDrop.GetComponent<Drops>().databaseItemID = avalibleItems[rand2];
			}
		}
	}

	public IEnumerator EnemyKnockback()
	{
		Vector3 endLocation = transform.position - transform.up;
		float dist = Vector3.Distance(endLocation, transform.position);
		float startTime = Time.time;
		knockback = true;
		while (dist > 0.2f)
		{
			transform.position = Vector3.Lerp(transform.position, endLocation, (Time.time - startTime) / 1f);
			dist = Vector3.Distance(endLocation, transform.position);
			yield return null;
		}
		knockback = false;
	}
}
