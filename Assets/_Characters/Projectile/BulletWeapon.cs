using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class BulletWeapon : MonoBehaviour {

		int damage;
		float speed;
		[SerializeField] LayerMask layersToHit;

		public float damageOffset = 1;

		void Update () {
			transform.position += transform.up * speed * Time.deltaTime;
		}

		void OnTriggerEnter2D (Collider2D coll) {
			RobotFunctions.DealDamage (damage, coll.gameObject, false);
			Destroy (gameObject);
		}

		public void BulletSetup (RangedWeapon weapon, Vector3 startLocation, Transform fireArc) {
			transform.position = startLocation;
			float randDir = 0;
			if (weapon.rangedWeaponDirection != 0) {
				randDir = Random.Range (-weapon.rangedWeaponDirection, weapon.rangedWeaponDirection);
			}
			transform.rotation = Quaternion.Euler (fireArc.eulerAngles.x, fireArc.eulerAngles.y, fireArc.eulerAngles.z + randDir);
			if (weapon.rangedWeaponStartOffset != 0) {
				// New offset position
				float rand = Random.Range (-weapon.rangedWeaponStartOffset, weapon.rangedWeaponStartOffset);
				transform.position += new Vector3 (rand, 0, 0);
			}
			speed = weapon.rangedWeaponSpeed;
			damage = Mathf.RoundToInt (weapon.rangedWeaponDamage * damageOffset);
			if (damage == 0) {
				damage = 1;
			}
			Invoke ("EndOfLife", weapon.rangedWeaponLife);
		}

		void EndOfLife () {
			Destroy (gameObject);
		}
	}
}