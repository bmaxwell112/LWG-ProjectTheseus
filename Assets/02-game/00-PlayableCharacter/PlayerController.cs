using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] LayerMask drop, enemyMask;
	[SerializeField] GameObject bullets;
	Transform leftArm, rightArm;
	RobotLoadout roLo;
	Vector3 rotation;

	void Start()
	{		
		leftArm = transform.Find("LeftArm");
		rightArm = transform.Find("RightArm");
		roLo = GetComponent<RobotLoadout>();
		PlayerSpawn();
	}

	private void PlayerSpawn()
	{
		Database db = FindObjectOfType<Database>();
		roLo.InitializeLoadout(db.items[0], db.items[1], db.items[2], db.items[3], db.items[4], db.items[5], db.items[6]);
	}

	// Update is called once per frame
	void Update () {
		MovementCheck();
		AimAndFireCheck();
		PickupItemCheck();
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
				}
			}
		}
	}

	private void AimAndFireCheck()
	{
		float horizontalThrow = Input.GetAxis("HorizontalAim");
		float verticalThrow = Input.GetAxis("VerticalAim");
		bool fire = Input.GetButtonDown("Fire1");
		bool fire2 = Input.GetButtonDown("Fire2");		
		if ((horizontalThrow > 0.5f || horizontalThrow < -0.5f) || (verticalThrow > 0.5f || verticalThrow < -0.5f))
		{
			rotation = MovementFunctions.LookAt2D(transform, horizontalThrow, verticalThrow);
		}
		transform.eulerAngles = rotation;
		if (fire)
		{
			PlayerAttack(roLo.loadout[2]);
		}
		if (fire2)
		{
			PlayerAttack(roLo.loadout[3]);
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
				enemy.collider.gameObject.GetComponent<RobotLoadout>().TakeDamage(item.itemValue, Color.red, new Color(0.82f, 0.55f, 0.16f));
			}
		}
		else if (item.itemType == ItemType.range)
		{
			SpawnBullets(item, fireFrom, 3);
		}
		else
		{
			Debug.LogWarning("Could not attack with ItemType " + item.itemType);
		}
	}

	public void SpawnBullets(Item item, Transform fireFrom, float life)
	{
		GameObject tempBullet = Instantiate(bullets, fireFrom.transform.position, transform.rotation);
		print(item.itemValue + ", " + item.itemValueTwo);
		tempBullet.GetComponent<PlayerRangeWeapon>().BulletSetup(item.itemValue, item.itemValueTwo, life);
	}

	private void MovementCheck()
	{
		float horizontalThrow = Input.GetAxis("Horizontal");
		float verticalThrow = Input.GetAxis("Vertical");
		// Sets players speed
		float xSpeed = horizontalThrow * roLo.loadout[(int)ItemLoc.legs].itemValue * Time.deltaTime;
		float ySpeed = verticalThrow * roLo.loadout[(int)ItemLoc.legs].itemValue * Time.deltaTime;
		// applies movement
		transform.position += new Vector3(xSpeed, ySpeed, transform.position.z);
	}

	private void AnimationChangeCheck()
	{
		// This will be used to change the animation based on direction.
	}
}
