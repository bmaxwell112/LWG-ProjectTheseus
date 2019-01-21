using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.ProGen;

namespace Theseus.Character {
	public class RangeShortEnemy : MonoBehaviour {

		Transform player;
		bool knockback, attacking, noPower, melee, walkTo, active;
		[SerializeField] float rateOfFireOffset = 1, healthOffset = 0.5f;
		[SerializeField] GameObject drop, leftArm, rightArm, bulletPrefab;
		[SerializeField] Transform firingArc;
		RoomGeneration parentRoom;
		RobotLoadout roLo;
		int attack;

		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			roLo = GetComponent<RobotLoadout> ();
			BasicEnemySetup ();
			for (int i = 0; i < roLo.loadout.Length; i++) {
				roLo.hitPoints[i] = Mathf.RoundToInt (roLo.hitPoints[i] / 2);
			}
			parentRoom = GetComponentInParent<RoomGeneration> ();
		}
		void Update () {
			if (parentRoom.roomActive && !active && RoomManager.gameSetupComplete) {
				EnemyTrackingAndFiring ();
				active = true;
			}
			if (!parentRoom.roomActive && active) {
				StopAllCoroutines ();
				active = false;
			}
		}

		private void EnemyTrackingAndFiring () {
			StartCoroutine (SpawnBullets (roLo.loadout[2], leftArm));
			StartCoroutine (SpawnBullets (roLo.loadout[3], rightArm));
			if (player) {
				StartCoroutine (DefineRotation ());
			}
		}

		private void BasicEnemySetup () {
			Database db = Database.instance;
			roLo.InitializeLoadout (
				db.RandomItemOut (ItemLoc.head),
				db.SudoRandomItemOut (ItemLoc.body, new int[] { 1, 8 }),
				db.SudoRandomItemOut (ItemLoc.leftArm, new int[] { 2, 13, 15, 17 }),
				db.SudoRandomItemOut (ItemLoc.rightArm, new int[] { 14, 16, 18 }),
				db.SudoRandomItemOut (ItemLoc.legs, new int[] { 4 }),
				db.RandomItemOut (ItemLoc.back),
				db.RandomItemOut (ItemLoc.core)
			);
			if (roLo.loadout[2].itemID != 2) {
				roLo.loadout[3] = db.items[3];
			}
			roLo.hitPoints[1] = Mathf.RoundToInt (roLo.hitPoints[1] * healthOffset);
		}

		public void EnemyUpdate () {
			if (RoomManager.gameSetupComplete) {
				if (player) {
					if (noPower && !walkTo) {
						GetComponent<EnemyController> ().stoppingDistance = 0.5f;
						walkTo = true;
					}
				} else {
					player = GameObject.FindGameObjectWithTag ("Player").transform;
				}
			}
		}

		IEnumerator DefineRotation () {
			while (true) {
				Vector3 diff = player.transform.position - transform.position;
				diff.Normalize ();
				// delay to rotate in seconds
				yield return new WaitForSeconds (0.5f);
				Quaternion toLoc = Quaternion.Euler (MovementFunctions.LookAt2D (transform, diff.x, diff.y));
				Quaternion fromLoc = firingArc.rotation;
				// Speed of rotation
				while (firingArc.rotation != toLoc) {
					if (RoomManager.gameSetupComplete) {
						firingArc.rotation = Quaternion.Lerp (fromLoc, toLoc, Time.time * 0.5f);
						float angle = Quaternion.Angle (firingArc.rotation, toLoc);
						// if angle diffence is less than 5 set it to end location to break loop.
						if (angle < 5) {
							firingArc.rotation = toLoc;
						}
					}
					yield return null;
				}
				yield return null;
			}
		}

		IEnumerator SpawnBullets (Item weapon, GameObject startLocation) {
			RangedWeapon rw = Database.instance.ItemsRangedWeapon (weapon);
			if (rw.rangedWeaponItemID != -1) {
				while (roLo.power[(int) weapon.itemLoc] > 0 && !roLo.dead) {
					for (int i = 0; i < rw.rangedWeaponSpread; i++) {
						GameObject bullet = Instantiate (bulletPrefab) as GameObject;
						bullet.GetComponent<BulletWeapon> ().damageOffset = 0.5f;
						bullet.GetComponent<BulletWeapon> ().BulletSetup (rw, startLocation.transform.position, firingArc);
					}
					roLo.power[(int) weapon.itemLoc] -= rw.rangeWeaponPowerUse * 2;
					yield return new WaitForSeconds (rw.rangeWeaponRateOfFire * rateOfFireOffset);
				}
				noPower = true;
				GetComponent<EnemyController> ().stoppingDistance = 0.25f;
			}
		}
	}
}