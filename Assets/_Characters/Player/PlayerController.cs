using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.Core;
using Theseus.ProGen;
using Theseus.DatabaseSystem;

namespace Theseus.Character {
	public class PlayerController : MonoBehaviour, IDamageable {

		[SerializeField] RobotParts head;
		[SerializeField] RobotParts body;
		[SerializeField] RobotArm leftArm;
		[SerializeField] RobotArm rightArm;
		[SerializeField] RobotLegs legs;
		[SerializeField] RobotParts back;
		[SerializeField] RobotParts core;
		[SerializeField] LayerMask drop, enemyMask;
		[SerializeField] GameObject bullets;
		// DETERMINE HOW THIS IS SET
		[SerializeField] float dodgeCooldown = 1;		
		NotificationsPanel np;
		public Transform firingArc;
		Vector3 rotation;
		bool fireLeft, fireRight, blockDodge, stationary, dodgeAvailable, playerDead;
		public bool activeDodge, activeBlock;
		Rigidbody2D rb;		

		void Start () {
			np = FindObjectOfType<NotificationsPanel> ();
			rb = GetComponent<Rigidbody2D> ();
			PlayerSpawn ();
			dodgeAvailable = true;
			leftArm.AttachAbilityTo(this.gameObject);
			rightArm.AttachAbilityTo(this.gameObject);
		}

		private void PlayerSpawn () {

		}

		// Update is called once per frame
		void Update () {
			if (RoomManager.gameSetupComplete) {
				if (GameManager.mouseInput) {
					InputCapture.MouseAim (MouseDistanceFromPlayer ());
				} else {
					InputCapture.ControllerAim ();
				}

				if (!GameManager.paused && !ItemWheel.active) {
					BlockDodgeCheck ();
					if (!activeBlock && !activeDodge) {
						MovementCheck ();
						AimAndFireCheck ();
					}

				}
			}
			// END GAME ON DEATH
			//if (roLo.dead && !playerDead) {
			//	Invoke ("LoadToHub", 2);
			//	playerDead = true;
			//}
		}

		public void TakeDamage(float damage)
		{
			// Pick Random Thing to Take Damage later
			body.TakeDamage(damage);
		}

		// Movement
		public void BlockDodgeCheck () {
			float xDodge = InputCapture.hThrow;
			float yDodge = InputCapture.vThrow;

			bool blockDodge = InputCapture.block;

			activeDodge = false;
			activeBlock = false;

			//stationary is found in the movement check
			if (blockDodge && stationary) {
				activeBlock = true;
				if (xDodge != 0 || yDodge != 0) {
					activeBlock = false;
					activeDodge = true;
					print ("block into dodge");
				}
			}

			if (blockDodge && !stationary && !activeDodge) {
				//tie running dodge into animation
			}

			if (activeDodge) {

				if (dodgeAvailable && InputCapture.JoystickOverThreshold (0.5f)) {
					CalcDodge ();
					dodgeAvailable = false;
					activeDodge = false;
					print ("Dodging");
					//set this to have a proper cooldown
				}
			}
		}
		public void CalcDodge () {
			Facing moveDir = InputCapture.JoystickDirection ();

			float dodgeRoll = 200f;
			print (moveDir);
			if (moveDir == Facing.right) {
				//right dodge
				StartCoroutine (PerformDodge (new Vector2 (dodgeRoll, 0)));
			}
			if (moveDir == Facing.upperRight) {
				//upright dodge
				StartCoroutine (PerformDodge (new Vector2 (dodgeRoll, dodgeRoll)));
			}
			if (moveDir == Facing.lowerRight) {
				//downright dodge
				StartCoroutine (PerformDodge (new Vector2 (dodgeRoll, -dodgeRoll)));
			}
			if (moveDir == Facing.left) {
				//left dodge
				StartCoroutine (PerformDodge (new Vector2 (-dodgeRoll, 0)));
			}
			if (moveDir == Facing.upperLeft) {
				//upleft dodge
				StartCoroutine (PerformDodge (new Vector2 (-dodgeRoll, dodgeRoll)));
			}
			if (moveDir == Facing.lowerLeft) {
				//downleft dodge
				StartCoroutine (PerformDodge (new Vector2 (-dodgeRoll, -dodgeRoll)));
			}
			if (moveDir == Facing.up) {
				//up dodge
				StartCoroutine (PerformDodge (new Vector2 (0, dodgeRoll)));
			}
			if (moveDir == Facing.down) {
				//down dodge
				StartCoroutine (PerformDodge (new Vector2 (0, -dodgeRoll)));
			}
		}
		IEnumerator PerformDodge (Vector2 dodgeForce) {
			rb.AddForce (dodgeForce);
			yield return new WaitForSeconds (0.25f);
			rb.velocity = Vector2.zero;
			dodgeAvailable = true;
			//yield return new WaitForSeconds(dodgeCooldown);
		}
		private void MovementCheck () {
			float speed = 1.5f;
			// Sets players speed
			if(legs.GetCurrentHitPoints() > 0)
			{
				speed = legs.GetSpeed();
			}
			// applies movement
			float xSpeed = InputCapture.hThrow * speed * Time.deltaTime;

			float ySpeed = InputCapture.vThrow * speed * Time.deltaTime;
			transform.position += new Vector3 (xSpeed, ySpeed, transform.position.z);

			if (xSpeed == 0 && ySpeed == 0) {
				stationary = true;
			} else {
				stationary = false;
			}
		}

		// Attacking
		private void AimAndFireCheck () {
			if ((InputCapture.hAim > 0.5f || InputCapture.hAim < -0.5f) || (InputCapture.vAim > 0.5f || InputCapture.vAim < -0.5f)) {
				rotation = MovementFunctions.LookAt2D (transform, InputCapture.hAim, InputCapture.vAim);
			}
			firingArc.eulerAngles = rotation;
			// ATTACKING LEFT ARM
			if (InputCapture.triggerLeft)
			{
				leftArm.Attack();
			}			
			// ATTACKING RIGHT ARM
			if (InputCapture.triggerRight)
			{
				rightArm.Attack();
			}
		}

		// For Mouse Controls
		Vector2 MouseDistanceFromPlayer () {
			float mouseX = Input.mousePosition.x;
			float mouseY = Input.mousePosition.y;
			float distanceFromCamera = 10f;

			Vector3 weirdTriplet = new Vector3 (mouseX, mouseY, distanceFromCamera);
			Camera cam = FindObjectOfType<Camera> ();
			Vector2 worldPos = cam.ScreenToWorldPoint (weirdTriplet);

			Vector2 distFromPlayer = new Vector2 (transform.position.x - worldPos.x, transform.position.y - worldPos.y);
			return distFromPlayer;
		}
	}
}