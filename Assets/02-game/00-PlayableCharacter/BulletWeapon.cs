using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour {

	int damage;
	float speed;
	string target, origin;

	public float damageOffset = 1;

	void Update()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == target)
		{
			coll.gameObject.GetComponent<RobotLoadout>().TakeDamage(damage, false);
			Destroy(gameObject);
		}
		if (coll.gameObject.tag == origin)
		{
			Physics2D.IgnoreCollision(coll.collider, GetComponent<Collider2D>());
		}
	}

	public void BulletSetup(RangedWeapon weapon, Vector3 startLocation, Transform fireArc, string targetTag, string originTag)
	{
		target = targetTag;
		origin = originTag;
		transform.position = startLocation;
		float randDir = 0;
		if (weapon.rangedWeaponDirection != 0)
		{
			randDir = Random.Range(-weapon.rangedWeaponDirection, weapon.rangedWeaponDirection);
		}			
		transform.rotation = Quaternion.Euler(fireArc.eulerAngles.x, fireArc.eulerAngles.y, fireArc.eulerAngles.z + randDir);
		if (weapon.rangedWeaponStartOffset != 0)
		{
			// New offset position
			float rand = Random.Range(-weapon.rangedWeaponStartOffset, weapon.rangedWeaponStartOffset);
			transform.position += new Vector3(rand, 0, 0);
		}
		speed = weapon.rangedWeaponSpeed;
		damage = Mathf.RoundToInt(weapon.rangedWeaponDamage * damageOffset);
		if (damage == 0)
		{
			damage = 1;
		}
		Invoke("EndOfLife", weapon.rangedWeaponLife);
	}

	void EndOfLife()
	{
		Destroy(gameObject);
	}
}
