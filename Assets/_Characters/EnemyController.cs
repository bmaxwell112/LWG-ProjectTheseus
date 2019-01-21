using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.ProGen;
using Theseus.DatabaseSystem;

namespace Theseus.Character {
	public class EnemyController : MonoBehaviour {

		[SerializeField] int enemyType;
		[SerializeField] LayerMask walls;
		[SerializeField] Transform firingArc;
		[SerializeField] float attackDelay, timeBetweenAttacks;
		[SerializeField] LayerMask playerMask;
		public float stoppingDistance;
		BasicEnemy basic;
		RangeShortEnemy rangeShort;
		PlayerController player;
		Pathfinding pathfinding;
		CustomNavMesh cNavMesh;
		RoomGeneration myRoom;
		RobotLoadout roLo;
		Collider2D coll;
		bool recalculate, startMovement, reStartMovement, attacking;
		Vector3 tracking;
		Vector3 heightOffset = new Vector3 (0, 0.45f, 0);
		Animator[] anims;
		public bool stunned;

		// Use this for initialization
		void Start () {
			coll = GetComponent<CapsuleCollider2D> ();
			tracking = transform.position;
			player = FindObjectOfType<PlayerController> ();
			pathfinding = GetComponent<Pathfinding> ();
			cNavMesh = GetComponentInParent<CustomNavMesh> ();
			myRoom = GetComponentInParent<RoomGeneration> ();
			roLo = GetComponent<RobotLoadout> ();
			anims = GetComponentsInChildren<Animator> ();
			if (enemyType == 0) {
				basic = GetComponent<BasicEnemy> ();
			}
			if (enemyType == 1) {
				rangeShort = GetComponent<RangeShortEnemy> ();
			}
		}

		// Update is called once per frame
		void Update () {
			if (RoomManager.gameSetupComplete) {
				EnemyUpdate ();
			}
		}

		void EnemyUpdate () {
			if (!stunned && player && !roLo.stopped) {
				if (enemyType == 0) {
					basic.EnemyUpdate ();
				}
				if (enemyType == 1) {
					rangeShort.EnemyUpdate ();
				}
				EnemyAttackCheck ();
			}
			if (!player) {
				player = FindObjectOfType<PlayerController> ();
			}
			if (myRoom.GetActive () && !startMovement && RoomManager.gameSetupComplete) {
				StopAllCoroutines ();
				StartCoroutine (UpdateMovement ());
				StartCoroutine (RecalculateTime ());
				startMovement = true;
				print ("Started enemy");
				attacking = false;
			}
			if (!myRoom.GetActive () && startMovement) {
				StopAllCoroutines ();
				startMovement = false;
			}
			if (roLo.stopped && !reStartMovement) {
				StopAllCoroutines ();
				reStartMovement = true;
				attacking = false;
			}
			if (!roLo.stopped && reStartMovement) {

				reStartMovement = false;
				StartCoroutine (UpdateMovement ());
				StartCoroutine (RecalculateTime ());
			}
		}

		IEnumerator UpdateMovement () {
			Waypoint currentNodePos = pathfinding.TransformToWaypoint (transform);
			while (true) {
				recalculate = false;
				if (player && cNavMesh && RoomManager.gameSetupComplete && !roLo.dead) {
					List<Waypoint> path = pathfinding.GetPath (player.transform, cNavMesh, currentNodePos);
					if (path.Count > 1) {
						foreach (Waypoint node in path) {
							if (node == null) {
								break;
							}
							Vector3 pos = new Vector3 (node.position.x, node.position.y + 0.45f);
							float checkTime = Time.time;
							while (Vector3.Distance (pos, transform.position) > 0.2f) {
								currentNodePos = node;
								transform.position = Vector3.MoveTowards (transform.position, pos, (roLo.loadout[(int) ItemLoc.legs].itemSpeed + roLo.loadout[(int) ItemLoc.body].itemSpeed - 0.5f) * Time.deltaTime);
								roLo.walk = true;
								if (Time.time > (checkTime + 5)) {
									path = pathfinding.GetPath (player.transform, cNavMesh, node);
									break;
								}
								// Stop movement while attacking
								while (roLo.AreYouStopped () || roLo.dead) {
									yield return null;
								}
								yield return null;
							}
							roLo.walk = false;
							if (recalculate || Vector3.Distance (player.transform.position, transform.position) < stoppingDistance) {
								break;
							}
						}
					}
				}
				roLo.walk = false;
				yield return null;
			}
		}
		IEnumerator RecalculateTime () {
			while (true) {
				yield return new WaitForSeconds (1);
				recalculate = true;
			}
		}

		public IEnumerator EnemyKnockback () {
			print ("this");
			Vector3 endLocation = firingArc.position - transform.up;
			float dist = Vector3.Distance (endLocation, transform.position);
			float startTime = Time.time;
			while (dist > 0.2f && !coll.IsTouchingLayers (walls)) {
				transform.position = Vector3.Lerp (transform.position, endLocation, (Time.time - startTime) / 1f);
				dist = Vector3.Distance (endLocation, transform.position);
				yield return null;
			}
			StartCoroutine (UpdateMovement ());
			StartCoroutine (RecalculateTime ());
		}

		private void EnemyAttackCheck () {
			if (!attacking) {
				Vector2 checkFrom = new Vector2 (firingArc.transform.position.x, firingArc.transform.position.y - 0.45f);
				RaycastHit2D hit = Physics2D.CircleCast (checkFrom, 0.45f, firingArc.transform.up, 0.45f, playerMask);
				if (hit.collider != null && anims[1].GetInteger ("action") != 2 && !roLo.stopped) {
					StartCoroutine (EnemyAttack ());
					attacking = true;
				}
			}
		}

		private IEnumerator EnemyAttack () {
			yield return new WaitForSeconds (attackDelay);
			anims[1].SetInteger ("action", 1);
			anims[2].SetInteger ("action", 1);
			yield return new WaitForSeconds (timeBetweenAttacks - attackDelay);
			attacking = false;
		}
	}
}