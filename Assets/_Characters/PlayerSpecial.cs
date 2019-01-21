using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class PlayerSpecial : MonoBehaviour {

		RobotLoadout playerLo;

		void Start () {
			playerLo = GetComponent<RobotLoadout> ();
		}

		// Activates in drop of RobotFunctions
		public void ActivateSpecialPassive (Item item) {
			print ("special Check");
			SpecialItems special = Database.instance.ItemSpecialItem (item);
			for (int i = 0; i < special.specialProps.Length; i++) {
				switch (special.specialProps[i]) {
					case SpecialProp.shield:
						CreateShield (item, special);
						break;
					case SpecialProp.heal:
						StartCoroutine (HealOverTime (item, special));
						break;
					case SpecialProp.powerBoost:
						StartCoroutine (PowerOverTime (item, special));
						break;
					default:
						Debug.LogWarning (item.itemName + " does not have a passive special");
						break;
				}
			}
		}
		public void ActivateSpecialActive1 () {
			print ("Call works: ");
		}
		public void ActivateSpecialActive (Item item, GameObject enemy) {
			SpecialItems special = Database.instance.ItemSpecialItem (item);
			for (int i = 0; i < special.specialProps.Length; i++) {
				switch (special.specialProps[i]) {
					case SpecialProp.stun:
						StunEnemy (special, enemy);
						break;
					case SpecialProp.bleed:
						BleedEnemy (special, enemy);
						break;
					case SpecialProp.cleave:
						CleaveEnemy (enemy);
						break;
					default:
						Debug.LogWarning (item.itemName + " does not have an active special");
						break;
				}
			}
		}
		void CreateShield (Item item, SpecialItems special) {
			// Make a Shield
			playerLo.shield = special;
		}

		void StunEnemy (SpecialItems special, GameObject enemyGO) {
			try {
				EnemyController enemy = enemyGO.GetComponent<EnemyController> ();
				if (!enemy.GetComponent<RobotLoadout> ().stopped) {
					StartCoroutine (UnStunEnemy (special, enemy));
				}
			} catch {
				Debug.LogWarning ("Enemy Controller not found");
				return;
			}

		}
		IEnumerator UnStunEnemy (SpecialItems special, EnemyController enemy) {
			enemy.stunned = true;
			yield return new WaitForSeconds (special.specialDuration);
			enemy.stunned = false;
		}

		void CleaveEnemy (GameObject enemy) {
			enemy.GetComponent<RobotLoadout> ().dropOffset += 5;
		}

		void BleedEnemy (SpecialItems special, GameObject enemy) {
			RobotLoadout enemyLo = enemy.GetComponent<RobotLoadout> ();
			StartCoroutine (Bleed (special, enemyLo));

		}
		IEnumerator Bleed (SpecialItems special, RobotLoadout enemyLo) {
			List<int> liveParts = new List<int> ();
			for (int i = 0; i < enemyLo.hitPoints.Length; i++) {
				if (enemyLo.hitPoints[i] > 0) {
					liveParts.Add (i);
					// twice as likely to hit everything but the head
					if (enemyLo.loadout[i].itemLoc != ItemLoc.head) {
						liveParts.Add (i);
					}
				}
			}
			int rand = Random.Range (0, liveParts.Count);
			while (enemyLo.hitPoints[liveParts[rand]] > 0 || enemyLo) {
				enemyLo.hitPoints[liveParts[rand]] -= special.specialDamage;
				yield return new WaitForSeconds (special.specialDuration);
			}
		}

		IEnumerator HealOverTime (Item item, SpecialItems special) {
			int thisItem = item.itemID;
			while (thisItem == item.itemID) {
				while (playerLo.power[(int) item.itemLoc] > 0) {
					bool used = false;
					for (int i = 0; i < playerLo.loadout.Length; i++) {
						if (playerLo.hitPoints[i] <= playerLo.loadout[i].itemHitpoints - special.specialDefence) {
							playerLo.hitPoints[i] += special.specialDefence;
							used = true;
						}
					}
					if (used) {
						playerLo.power[(int) item.itemLoc] -= special.specialPowerUse;
					}
					yield return new WaitForSeconds (special.specialDuration);
				}
				yield return null;
			}
		}
		IEnumerator PowerOverTime (Item item, SpecialItems special) {
			int thisItem = item.itemID;
			while (thisItem == item.itemID) {
				while (playerLo.power[(int) item.itemLoc] > 0) {
					int used = 0;
					for (int i = 0; i < playerLo.loadout.Length; i++) {
						if (playerLo.power[i] <= playerLo.loadout[i].itemPower - special.specialPowerUse && playerLo.loadout[i].itemLoc != item.itemLoc) {
							playerLo.power[i] += special.specialPowerUse;
							print ("added " + special.specialDefence + " to " + playerLo.hitPoints[i]);
							used++;
							Debug.Log ("power added");
						}
					}
					if (used > 0) {
						playerLo.power[(int) item.itemLoc] -= special.specialPowerUse * used;
					}
					yield return new WaitForSeconds (special.specialDuration);
				}
				yield return null;
			}
		}
	}
}