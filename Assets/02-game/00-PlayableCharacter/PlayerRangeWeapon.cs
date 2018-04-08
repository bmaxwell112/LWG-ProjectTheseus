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
			coll.gameObject.GetComponent<BasicEnemy>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}

	public void BulletSetup(int dmg, int spd, float life)
	{
		damage = dmg;
		speed = spd;
		Invoke("EndOfLife", life);
	}

	void EndOfLife()
	{
		Destroy(gameObject);
	}
}
