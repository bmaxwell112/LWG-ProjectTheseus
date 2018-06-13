using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotAttack : MonoBehaviour {

	[SerializeField] LayerMask enemyMask;
	[SerializeField] Transform FiringArc;
	[SerializeField] int armLocation;
	public bool meleeAttacking;
	RobotLoadout roLo;
	//[SerializeField] GameObject bulletPrefab;

	bool firing, localCooldown;
	PlayerSpecial special;
	bool canSpecial;

	void Start()
	{
		special = GetComponentInParent<PlayerSpecial>();
		canSpecial = GetComponentInParent<PlayerSpecial>();
		roLo = GetComponentInParent<RobotLoadout>();
		StartMovement();
	}

	public void MeleeAttack()
	{
		if (!meleeAttacking)
		{
			MeleeWeapon mw = Database.instance.ItemsMeleeWeapon(roLo.loadout[armLocation]);
			// Stop movement if applicable			
			RaycastHit2D enemy = Physics2D.CircleCast(
						new Vector2(transform.position.x, transform.position.y),
						0.15f,
						Vector2.up,
						0.15f,
						enemyMask);
			// TODO this will need to be more universal.
			if (enemy.collider != null)
			{
				if (canSpecial)
				{
					if (roLo.loadout[armLocation].itemSpecial && roLo.power[(int)roLo.loadout[armLocation].itemLoc] > 0)
					{
						roLo.power[(int)roLo.loadout[armLocation].itemLoc] -= Database.instance.ItemSpecialItem(roLo.loadout[armLocation]).specialPowerUse;
						special.ActivateSpecialActive(roLo.loadout[armLocation], enemy.collider.gameObject);
					}
				}
				RobotFunctions.DealDamage(Damage(), enemy.collider.gameObject, mw.meleeWeaponStopTarget);
			}
			meleeAttacking = true;
		}
	}

	public void StopMovementCheck()
	{

        bool blockDodge = Input.GetButton("BlockDodge");

        MeleeWeapon mw = Database.instance.ItemsMeleeWeapon(roLo.loadout[armLocation]);
		if (armLocation == 2 && !blockDodge)
		{
			roLo.stopWhileAttackingLeft = mw.meleeWeaponStopMovement;
			print(roLo.stopWhileAttackingLeft);
		}
		else
		{
			roLo.stopWhileAttackingRight = mw.meleeWeaponStopMovement;
			print(roLo.stopWhileAttackingRight);
		}
	}

	private int Damage()
	{
		int dmg = Mathf.RoundToInt(roLo.loadout[armLocation].itemDamage * roLo.damageOffset);
		return dmg;
	}

	public void RangedAttack(Item weapon)
	{
        bool blockDodge = Input.GetButton("BlockDodge");

        if (!localCooldown && !blockDodge)
		{
			StartCoroutine(SpawnBullets(weapon));
		}
	}

	IEnumerator SpawnBullets(Item weapon)
	{		
		RangedWeapon rw = Database.instance.ItemsRangedWeapon(weapon);
		RobotLoadout roLo = FindObjectOfType<PlayerController>().GetComponent<RobotLoadout>();
		int itemLoc = (int)weapon.itemLoc;		
		localCooldown = true;
		firing = true;
		// loop breaks if you stop firing, run out of power or run out of health
		while (firing && roLo.power[itemLoc] > 0 && roLo.hitPoints[itemLoc] > 0)
		{
			for (int i = 0; i < rw.rangedWeaponSpread; i++)
			{
				GameObject bullet = Instantiate(Resources.Load("bulletFriendly", typeof(GameObject))) as GameObject;
				bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, FiringArc);					
			}
			roLo.power[itemLoc] -= rw.rangeWeaponPowerUse;
			yield return new WaitForSeconds(rw.rangeWeaponRateOfFire);
		}	
		localCooldown = false;
	}	

	public void FiringCheck(bool playerFiring)
	{
		firing = playerFiring;
	}

	public void StartMovement()
	{
		if (armLocation == 2)
		{
			roLo.stopWhileAttackingLeft = false;
		}
		else
		{
			roLo.stopWhileAttackingRight = false;
		}
	}
}
