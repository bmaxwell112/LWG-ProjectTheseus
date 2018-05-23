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
				EnemyMovement();
			}
			if (!roLo.attackLeft && RoomManager.allActive)
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
		RaycastHit2D hit = Physics2D.Raycast(firingArc.transform.position, firingArc.transform.up, 0.35f, playerMask);
		if (hit.collider != null && !roLo.attackLeft)
		{
			roLo.attackLeft = true;
			roLo.attackRight = true;
			Invoke("EnemyAttack", 0.5f);
		}
	}
	private void EnemyAttack()
	{
		RaycastHit2D hit = Physics2D.Raycast(firingArc.transform.position, firingArc.transform.up, 0.35f, playerMask);
		if (hit.collider != null)
		{
			RobotFunctions.DealDamage(attack, hit.collider.gameObject);
		}
		roLo.attackLeft = false;
		roLo.attackRight = false;
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();
		firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
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
