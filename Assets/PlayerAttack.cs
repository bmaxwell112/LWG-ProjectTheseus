using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {

	[SerializeField] LayerMask enemyMask;
	[SerializeField] Transform FiringArc;
	//[SerializeField] GameObject bulletPrefab;

	bool firing, localCooldown;

	public void MeleeAttack(Item weapon)
	{
		RaycastHit2D enemy = Physics2D.CircleCast(
					new Vector2(transform.position.x, transform.position.y),
					0.5f,
					Vector2.up,
					0.5f,
					enemyMask);
		// TODO this will need to be more universal.
		if (enemy.collider != null)
		{
			enemy.collider.gameObject.GetComponent<RobotLoadout>().TakeDamage(weapon.itemDamage, true);
		}
	}
	public void RangedAttack(Item weapon)
	{
		print("Ranged Attack");
		if (!localCooldown)
		{
			print("Cool down false");
			StartCoroutine(SpawnBullets(weapon));
		}
	}

	IEnumerator SpawnBullets(Item weapon)
	{
		print("Start Co-routine");
		RangedWeapon rw = Database.instance.ItemsRangedWeapon(weapon);
		localCooldown = true;
		firing = true;
		while (firing)
		{
			for (int i = 0; i < rw.rangedWeaponSpread; i++)
			{
				GameObject bullet = Instantiate(Resources.Load("bulletFriendly", typeof(GameObject))) as GameObject;
				bullet.GetComponent<BulletWeapon>().BulletSetup(rw, transform.position, FiringArc);
			}
			yield return new WaitForSeconds(rw.rangeWeaponRateOfFire);
		}
		print("End Co-routine");
		localCooldown = false;
	}

	public void FiringCheck(bool playerFiring)
	{
		firing = playerFiring;
	}	
}
