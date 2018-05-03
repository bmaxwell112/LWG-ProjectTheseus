using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour {

	int damage;
	float speed;
	float direction;

	void Update()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		print("collided");
		if (coll.gameObject.tag == "Enemy")
		{
			coll.gameObject.GetComponent<RobotLoadout>().TakeDamage(damage, Color.red, new Color(0.82f, 0.55f, 0.16f), false);
			Destroy(gameObject);
		}
	}

	public void BulletSetup(RangedWeapon weapon, Vector3 startLocation, Transform fireArc)
	{		
		transform.position = startLocation;
		transform.rotation = fireArc.rotation;
		if (weapon.rangedWeaponStartOffset != 0)
		{
			// New offset position
			float rand = Random.Range(-weapon.rangedWeaponStartOffset, weapon.rangedWeaponStartOffset);
			transform.position += new Vector3(rand, 0, 0);
		}
		speed = weapon.rangedWeaponSpeed;
		damage = weapon.rangedWeaponDamage;
		Invoke("EndOfLife", weapon.rangedWeaponLife);
		direction = weapon.rangedWeaponDirection;
		BulletWeapon[] bullets = FindObjectsOfType<BulletWeapon>();
		if (weapon.rangedWeaponSpread > 1 && bullets.Length <= weapon.rangedWeaponSpread)
		{
			// Spawn More Bullets			
		}
	}

	void EndOfLife()
	{
		Destroy(gameObject);
	}
}
