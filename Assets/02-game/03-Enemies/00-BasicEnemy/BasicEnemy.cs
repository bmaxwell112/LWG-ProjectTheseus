using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	Transform player;
	bool knockback;
	[SerializeField] GameObject drop;
	[SerializeField] LayerMask playerMask;
	[SerializeField] Transform firingArc;
	RobotLoadout roLo;
	UniveralActions actions;
	int attack;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		actions = GetComponent<UniveralActions>();
		roLo = GetComponent<RobotLoadout>();
		BasicEnemySetup();
		attack = Mathf.RoundToInt((roLo.loadout[(int)ItemLoc.rightArm].itemDamage + roLo.loadout[(int)ItemLoc.leftArm].itemDamage) / 2);
		for (int i = 0; i < roLo.loadout.Length; i++)
		{
			roLo.hitPoints[i] = Mathf.RoundToInt(roLo.hitPoints[i] / 2);
		}
	}

	private void BasicEnemySetup()
	{
		Database db = Database.instance;
		roLo.InitializeLoadout(
			db.RandomItemOut(ItemLoc.head),
			db.RandomItemOut(ItemLoc.body),
			db.SudoRandomItemOut(ItemLoc.leftArm, new int[] { 2, 22, 24, 26 }),
			db.SudoRandomItemOut(ItemLoc.rightArm, new int[] { 3, 23, 25, 27 }),
			db.SudoRandomItemOut(ItemLoc.legs, new int[] { 4, 9, 29 } ),
			db.RandomItemOut(ItemLoc.back),
			db.RandomItemOut(ItemLoc.core)
			);
	}

	public void EnemyUpdate()
	{
		if (RoomManager.allActive)
		{
			if (player)
			{
				DefineRotation();
			}
			if (RoomManager.allActive)
			{
				EnemyAttackCheck();
			}
		}
	}

	private void EnemyMovement()
	{
		float dist = Vector3.Distance(transform.position, player.transform.position);
		if (!knockback && dist > 0.35f)
		{
			roLo.walk = true;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (roLo.loadout[(int)ItemLoc.legs].itemSpeed - 0.5f) * Time.deltaTime);
		}
		else
		{
			roLo.walk = false;
		}
	}

	private void EnemyAttackCheck()
	{
		RaycastHit2D hit = Physics2D.CircleCast(firingArc.transform.position, 0.45f, firingArc.transform.up, 0.45f, playerMask);
		if (hit.collider != null && !roLo.attackLeft && !roLo.stopped)
		{
			print("Hit " + hit.collider.gameObject.name);
			roLo.attackLeft = true;
			roLo.attackRight = true;
			Invoke("EndAttack", 2);
		}
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();
		firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
	}

	private void EndAttack()
	{	
		roLo.attackLeft = false;
		roLo.attackRight = false;
	}
}
