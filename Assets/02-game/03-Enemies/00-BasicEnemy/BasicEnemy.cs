using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

	Transform player;
	bool knockback;
	[SerializeField] GameObject drop;
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
		if (RoomManager.gameSetupComplete)
		{
			if (player)
			{
				DefineRotation();
			}
		}
	}

	private void DefineRotation()
	{
		Vector3 diff = player.transform.position - transform.position;
		diff.Normalize();
		firingArc.eulerAngles = MovementFunctions.LookAt2D(transform, diff.x, diff.y);
	}

}
