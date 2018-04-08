using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] LayerMask drop, enemyMask;
	[SerializeField] GameObject bullets;
	Transform leftArm, rightArm;
	PlayerInventory inv;
	Vector3 rotation;
	// TODO change to private later
	public int hitPoints;

	void Start()
	{		
		leftArm = transform.Find("LeftArm");
		rightArm = transform.Find("RightArm");
		inv = GetComponent<PlayerInventory>();
		SpeedChangeCheck();
		hitPoints = inv.inventory[1].itemHitpoints;
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
					// Identify replacement part and store it in temp
					Item tempItem = inv.IdentifyReplacePart(drop.thisItem);
					int tempHP = hitPoints;
					// replace inventory item with drop
					inv.ReplacePart(drop.thisItem);
					// Check for specials
					GetComponent<PlayerSpecial>().SpecialCheck(drop.thisItem);
					if (drop.thisItem.itemLoc == ItemLoc.body)
					{
						hitPoints = drop.hitPoints;
					}
					// replace drop instance with temp item
					if (tempItem.itemID != -1)
					{
						drop.IdentifyItem(tempItem, tempHP);
					}
					else
					{
						Debug.LogWarning("No item returned from pickup");
					}					
					SpeedChangeCheck();					
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
			PlayerAttack(inv.inventory[2]);
		}
		if (fire2)
		{
			PlayerAttack(inv.inventory[3]);
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
		StartCoroutine(ChangeColor(arm, Color.white, 0));
		StartCoroutine(ChangeColor(arm, Color.blue, 0.25f));
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
				enemy.collider.gameObject.GetComponent<BasicEnemy>().TakeDamage(item.itemValue);
			}
		}
		else if (item.itemType == ItemType.range)
		{
			GameObject tempBullet = Instantiate(bullets, fireFrom.transform.position, transform.rotation);
			tempBullet.GetComponent<PlayerRangeWeapon>().BulletSetup(item.itemValue, item.itemValueTwo);
		}
		else
		{
			Debug.LogWarning("Could not attack with ItemType " + item.itemType);
		}
	}

	private void MovementCheck()
	{
		float horizontalThrow = Input.GetAxis("Horizontal");
		float verticalThrow = Input.GetAxis("Vertical");
		// Sets players speed
		float xSpeed = horizontalThrow * speed * Time.deltaTime;
		float ySpeed = verticalThrow * speed * Time.deltaTime;
		// applies movement
		transform.position += new Vector3(xSpeed, ySpeed, transform.position.z);
	}

	private void AnimationChangeCheck()
	{
		// This will be used to change the animation based on direction.
	}

	void DeathCheck()
	{
		if (hitPoints <= 0)
		{
			Destroy(gameObject);
		}		
	}

	public void TakeDamage(int damage)
	{
		hitPoints -= damage;
		DeathCheck();
		SpriteRenderer body = transform.Find("Body").GetComponent<SpriteRenderer>();
		StartCoroutine(ChangeColor(body, Color.red, 0));
		StartCoroutine(ChangeColor(body, Color.green, 0.25f));
	}

	// TODO remove this later
	IEnumerator ChangeColor(SpriteRenderer sr, Color color, float t)
	{		
		yield return new WaitForSeconds(t);
		sr.color = color;
	}

	void SpeedChangeCheck()
	{	
		speed = inv.inventory[4].itemValue;
	}	
}
