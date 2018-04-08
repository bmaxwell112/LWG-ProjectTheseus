using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRangeWeapon : MonoBehaviour {

	int damage, speed;	

	void Update()
	{
		transform.position += transform.up * speed * Time.deltaTime;
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		print("collided");
		if (coll.gameObject.tag == "Enemy")
		{
			coll.gameObject.GetComponent<RobotLoadout>().TakeDamage(damage, Color.red, new Color(0.82f, 0.55f, 0.16f));
			Destroy(gameObject);
		}
	}

	public void BulletSetup(int dmg, int spd)
	{
		damage = dmg;
		speed = spd;
	}
}
