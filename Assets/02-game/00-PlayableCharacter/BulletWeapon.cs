using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletWeapon : MonoBehaviour {

	int damage;
	float speed;
	float direction;
	string target, origin;	

	void Update()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == target)
		{
			coll.gameObject.GetComponent<RobotLoadout>().TakeDamage(damage, Color.red, new Color(0.82f, 0.55f, 0.16f), false);
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
		transform.rotation = fireArc.rotation;
		print("arc: " + fireArc.rotation + "   rotation: " + transform.rotation);
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
