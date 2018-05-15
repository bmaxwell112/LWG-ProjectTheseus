using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] LayerMask drop, enemyMask;
	[SerializeField] GameObject bullets;
	[SerializeField] PlayerAttack leftArm, rightArm;
	public Transform firingArc;
	RobotLoadout roLo;
	Vector3 rotation;
	bool fireLeft, fireRight;	

	void Start()
	{		
		roLo = GetComponent<RobotLoadout>();
		PlayerSpawn();
	}

	private void PlayerSpawn()
	{
		Database db = Database.instance;
		if (!GameManager.playerAlive)
		{
			roLo.InitializeLoadout(db.items[0], db.items[1], db.items[2], db.items[3], db.items[4], db.items[5], db.items[6]);
			GameManager.playerAlive = true;
		}
		else
		{
			roLo.InitializeLoadout(
				GameManager.playerLoadout[0],
				GameManager.playerLoadout[1],
				GameManager.playerLoadout[2],
				GameManager.playerLoadout[3],
				GameManager.playerLoadout[4],
				GameManager.playerLoadout[5],
				GameManager.playerLoadout[6]
				);
		}
	}

	// Update is called once per frame
	void Update () {
		if (RoomManager.allActive)
		{
			if (GameManager.mouseInput)
			{
				InputCapture.MouseAim(MouseDistanceFromPlayer());
			}
			else
			{
				InputCapture.ControllerAim();
			}

			if (!GameManager.gamePaused)
			{
				MovementCheck();
				AimAndFireCheck();
				PickupItemCheck();
			}
		}
	}

	private void PickupItemCheck()
	{
		bool pickup = Input.GetButtonDown("Pickup");		
		if (pickup)
		{
			Collider2D itemInRange = Physics2D.OverlapCircle(transform.position, 1, drop);
			if (itemInRange)
			{
				if (itemInRange.gameObject.GetComponent<Drops>())
				{
					Drops drop = itemInRange.gameObject.GetComponent<Drops>();
					RobotFunctions.ReplaceDropPart(drop, roLo);
					drop.RenameAndReset();
				}
			}
		}
	}

	private void AimAndFireCheck()
	{
		if ((InputCapture.hAim > 0.5f || InputCapture.hAim < -0.5f) || (InputCapture.vAim > 0.5f || InputCapture.vAim < -0.5f))
		{
			rotation = MovementFunctions.LookAt2D(transform, InputCapture.hAim, InputCapture.vAim);
		}
		firingArc.eulerAngles = rotation;
		if (InputCapture.firingLeft && !fireLeft)
		{
			if (roLo.loadout[2].itemType == ItemType.range)
			{
				leftArm.RangedAttack(roLo.loadout[2]);
			}
			else
			{
				leftArm.MeleeAttack(roLo.loadout[2]);
			}
			fireLeft = true;
		}
		else if (!InputCapture.firingLeft)
		{
			fireLeft = false;
		}
		if (InputCapture.firingRight  && !fireRight)
		{
			if (roLo.loadout[3].itemType == ItemType.range)
			{
				rightArm.RangedAttack(roLo.loadout[3]);
			}
			else
			{
				rightArm.MeleeAttack(roLo.loadout[3]);
			}
			fireRight = true;
		}
		else if (!InputCapture.firingRight)
		{
			fireRight = false;			
		}
		rightArm.GetComponent<PlayerAttack>().FiringCheck(fireRight);
		leftArm.GetComponent<PlayerAttack>().FiringCheck(fireLeft);
	}

	private void MovementCheck()
	{		
		// Sets players speed
		float xSpeed = InputCapture.hThrow * roLo.loadout[(int)ItemLoc.legs].itemSpeed * Time.deltaTime;
		float ySpeed = InputCapture.vThrow * roLo.loadout[(int)ItemLoc.legs].itemSpeed * Time.deltaTime;
		// applies movement
		transform.position += new Vector3(xSpeed, ySpeed, transform.position.z);
	}

	private void AnimationChangeCheck()
	{
		// This will be used to change the animation based on direction.
	}

	// For Mouse Controls
	Vector2 MouseDistanceFromPlayer()
	{
		float mouseX = Input.mousePosition.x;
		float mouseY = Input.mousePosition.y;
		float distanceFromCamera = 10f;

		Vector3 weirdTriplet = new Vector3(mouseX, mouseY, distanceFromCamera);
		Camera cam = FindObjectOfType<Camera>();
		Vector2 worldPos = cam.ScreenToWorldPoint(weirdTriplet);

		Vector2 distFromPlayer = new Vector2(transform.position.x - worldPos.x, transform.position.y - worldPos.y);
		return distFromPlayer;
	}	
}
