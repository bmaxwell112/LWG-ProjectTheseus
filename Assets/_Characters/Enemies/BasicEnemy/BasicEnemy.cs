using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.ProGen;
using Theseus.DatabaseSystem;

namespace Theseus.Character {
	public class BasicEnemy : MonoBehaviour {

		Transform player;
		bool knockback;
		[SerializeField] GameObject drop;
		[SerializeField] Transform firingArc;
		RobotLoadout roLo;
		int attack;

		void Start () {
			player = GameObject.FindGameObjectWithTag ("Player").transform;
			roLo = GetComponent<RobotLoadout> ();
			BasicEnemySetup ();
			attack = Mathf.RoundToInt ((roLo.loadout[(int) ItemLoc.rightArm].itemDamage + roLo.loadout[(int) ItemLoc.leftArm].itemDamage) / 2);
			for (int i = 0; i < roLo.loadout.Length; i++) {
				roLo.hitPoints[i] = Mathf.RoundToInt (roLo.hitPoints[i] / 2);
			}
		}

		private void BasicEnemySetup () {
			Database db = Database.instance;
			roLo.InitializeLoadout (
				db.RandomItemOut (ItemLoc.head),
				db.SudoRandomItemOut (ItemLoc.body, new int[] { 1, 8 }),
				db.SudoRandomItemOut (ItemLoc.leftArm, new int[] { 2, 22, 24, 26 }),
				db.SudoRandomItemOut (ItemLoc.rightArm, new int[] { 3, 23, 25, 27 }),
				db.SudoRandomItemOut (ItemLoc.legs, new int[] { 4, 9, 29 }),
				db.RandomItemOut (ItemLoc.back),
				db.RandomItemOut (ItemLoc.core)
			);
		}

		public void EnemyUpdate () {
			if (RoomManager.gameSetupComplete) {
				if (player) {
					DefineRotation ();
				} else {
					player = GameObject.FindGameObjectWithTag ("Player").transform;
				}
			}
		}

		private void DefineRotation () {
			Vector3 diff = player.transform.position - transform.position;
			diff.Normalize ();
			firingArc.eulerAngles = MovementFunctions.LookAt2D (transform, diff.x, diff.y);
		}
	}
}