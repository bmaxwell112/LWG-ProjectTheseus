using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeShortEnemy : MonoBehaviour {
	
	Transform player;
	bool knockback, attacking;
	[SerializeField] float knockbackDistance, rateOfFireOffset=1;
	[SerializeField] GameObject drop, leftArm, rightArm, bulletPrefab;
	[SerializeField] LayerMask playerMask;
	[SerializeField] Transform firingArc;
	RobotLoadout roLo;
	int attack;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player").transform;
		roLo = GetComponent<RobotLoadout>();
		BasicEnemySetup();
		for (int i = 0; i < roLo.loadout.Length; i++)
		{
			roLo.hitPoints[i] = Mathf.RoundToInt(roLo.hitPoints[i] / 2);
		}
		StartCoroutine(SpawnBullets(roLo.loadout[2], leftArm));
		StartCoroutine(SpawnBullets(roLo.loadout[3], rightArm));
		if (player)
		{
			StartCoroutine(DefineRotation());
		}
	}

	private void BasicEnemySetup()
	{
		Database db = Database.instance;
		roLo.InitializeLoadout(
			db.RandomItemOut(ItemLoc.head),
			db.RandomItemOut(ItemLoc.body),
			db.SudoRandomItemOut(ItemLoc.leftArm, new int[] { 2, 13, 15, 17 }),
			db.SudoRandomItemOut(ItemLoc.rightArm, new int[] { 14, 16, 18 }),
			db.SudoRandomItemOut(ItemLoc.legs, new int[] { 4 }),
			db.RandomItemOut(ItemLoc.back),
			db.RandomItemOut(ItemLoc.core)
			);
		if (roLo.loadout[2].itemID != 2)
		{
			roLo.loadout[3] = db.items[3];
		}
	}

	// Update is called once per frame
	void Update()
	{
		if (RoomManager.allActive)
		{
			if (player)
			{				
				EnemyMovement();
			}
		}
	}

	private void EnemyMovement()
	{
		float dist = Vector3.Distance(transform.position, player.transform.position);
		if (!knockback && dist > 2)
		{
			roLo.walk = true;
			transform.position = Vector3.MoveTowards(transform.position, player.transform.position, (roLo.loadout[(int)ItemLoc.legs].itemSpeed - 0.5f) * Time.deltaTime);
		}
		else
		{
			roLo.walk = false;
		}
	}
	
	IEnumerator DefineRotation()
	{
		while (true)
		{			
			Vector3 diff = player.transform.position - transform.position;
			diff.Normalize();
			// delay to rotate in seconds
			yield return new WaitForSeconds(0.5f);
			Quaternion toLoc = Quaternion.Euler(MovementFunctions.LookAt2D(transform, diff.x, diff.y));
			Quaternion fromLoc = firingArc.rotation;
			// Speed of rotation
			while (firingArc.rotation != toLoc)
			{
				if (RoomManager.allActive)
				{
					firingArc.rotation = Quaternion.Lerp(fromLoc, toLoc, Time.time * 0.5f);
					float angle = Quaternion.Angle(firingArc.rotation, toLoc);
					// if angle diffence is less than 5 set it to end location to break loop.
					if (angle < 5)
					{
						firingArc.rotation = toLoc;
					}
				}
				yield return null;
			}			
			yield return null;
		}
	}

	IEnumerator SpawnBullets(Item weapon, GameObject startLocation)
	{
		RangedWeapon rw = Database.instance.ItemsRangedWeapon(weapon);
		if (rw.rangedWeaponItemID != -1)
		{
			while (true)
			{
				for (int i = 0; i < rw.rangedWeaponSpread; i++)
				{
					GameObject bullet = Instantiate(bulletPrefab) as GameObject;
					bullet.GetComponent<BulletWeapon>().damageOffset = 0.5f;
					bullet.GetComponent<BulletWeapon>().BulletSetup(rw, startLocation.transform.position, firingArc);
				}
				yield return new WaitForSeconds(rw.rangeWeaponRateOfFire * rateOfFireOffset);
			}
		}
	}
}