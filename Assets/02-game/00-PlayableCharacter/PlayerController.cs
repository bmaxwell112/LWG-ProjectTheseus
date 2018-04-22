using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] LayerMask drop, enemyMask;
	[SerializeField] GameObject bullets;
	[SerializeField] Transform leftArm, rightArm;
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
		roLo.InitializeLoadout(db.items[0], db.items[1], db.items[2], db.items[3], db.items[4], db.items[5], db.items[6]);
	}

	// Update is called once per frame
	void Update () {
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
					roLo.ReplaceDropPart(drop);
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
		if (InputCapture.fireLeftDown)
		{
			PlayerAttack(roLo.loadout[2]);
		}
		if (InputCapture.fireRightDown)
		{
			PlayerAttack(roLo.loadout[3]);
		}
		if (!InputCapture.firingLeft && fireLeft)
		{
			fireLeft = false;
		}
		if (!InputCapture.firingRight && fireRight)
		{
			fireRight = false;
		}
	}

	private void PlayerAttack(Item item)
	{
		Transform fireFrom;
		if (item.itemLoc == ItemLoc.leftArm)
		{
			fireFrom = leftArm;
		}
		else
		{
			fireFrom = rightArm;
		}
		SpriteRenderer arm = fireFrom.GetComponent<SpriteRenderer>();
		StartCoroutine(roLo.ChangeColor(arm, Color.white, 0));
		StartCoroutine(roLo.ChangeColor(arm, Color.blue, 0.25f));
		if (item.itemType == ItemType.melee)
		{			
			RaycastHit2D enemy = Physics2D.CircleCast(
					new Vector2(fireFrom.transform.position.x, leftArm.transform.position.y),
					0.25f,
					Vector2.up,
					0.25f,
					enemyMask);
			// TODO this will need to be more universal.
			if (enemy.collider != null)
			{
				enemy.collider.gameObject.GetComponent<RobotLoadout>().TakeDamage(item.itemValue, Color.red, new Color(0.82f, 0.55f, 0.16f), true);
			}
		}
		else if (item.itemType == ItemType.range)
		{
			print(InputCapture.firingRight);
			if (item.itemLoc == ItemLoc.leftArm && !fireLeft)
			{
				fireLeft = true;
				StartCoroutine(SpawnBullets(item, fireFrom));				
			}
			if (item.itemLoc == ItemLoc.rightArm && !fireRight)
			{
				fireRight = true;
				StartCoroutine(SpawnBullets(item, fireFrom));				
			}
		}
		else
		{
			Debug.LogWarning("Could not attack with ItemType " + item.itemType);
		}
	}

	public IEnumerator SpawnBullets(Item item, Transform fireFrom)
	{
		bool fire = true;
		while (fire)
		{
			GameObject tempBullet = Instantiate(bullets, fireFrom.transform.position, transform.rotation);
			tempBullet.GetComponent<PlayerRangeWeapon>().BulletSetup(item.itemValue, item.itemValueTwo, item.itemFltValueTwo);
			if (item.itemLoc == ItemLoc.leftArm)
			{
				fire = fireLeft;
			}
			if (item.itemLoc == ItemLoc.rightArm)
			{
				fire = fireRight;
			}
			yield return new WaitForSeconds(item.itemFltValue);
		}
	}

	private void MovementCheck()
	{		
		// Sets players speed
		float xSpeed = InputCapture.hThrow * roLo.loadout[(int)ItemLoc.legs].itemValue * Time.deltaTime;
		float ySpeed = InputCapture.vThrow * roLo.loadout[(int)ItemLoc.legs].itemValue * Time.deltaTime;
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
