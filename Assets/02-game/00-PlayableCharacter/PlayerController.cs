using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;
	[SerializeField] LayerMask drop;
	PlayerInventory inv;
	Vector3 rotation;

	void Start()
	{
		inv = GetComponent<PlayerInventory>();
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
			print("Attempt Pickup");
			Collider2D itemInRange = Physics2D.OverlapCircle(transform.position, 1, drop);
			if (itemInRange)
			{
				if (itemInRange.gameObject.GetComponent<Drops>())
				{
					Drops drop = itemInRange.gameObject.GetComponent<Drops>();
					// Identify replacement part and store it in temp
					Item tempItem = inv.IdentifyReplacePart(drop.thisItem);
					// replace inventory item with drop
					inv.ReplacePart(drop.thisItem);
					print("replaced " + tempItem.itemName + " with " + drop.thisItem.itemName);
					// replace drop instance with temp item
					if (tempItem.itemID != -1)
					{
						drop.IdentifyItem(tempItem);
					}
					else
					{
						Debug.LogWarning("No item returned from pickup");
					}					
				}
			}
		}
	}

	private void AimAndFireCheck()
	{
		float horizontalThrow = Input.GetAxis("HorizontalAim");
		float verticalThrow = Input.GetAxis("VerticalAim");
		bool fire = Input.GetButton("Fire1");
		bool fire2 = Input.GetButton("Fire2");
		if ((horizontalThrow > 0.5f || horizontalThrow < -0.5f) || (verticalThrow > 0.5f || verticalThrow < -0.5f))
		{
			rotation = new Vector3(
				transform.eulerAngles.x,
				transform.eulerAngles.y,
				Mathf.Atan2(-horizontalThrow, verticalThrow) * Mathf.Rad2Deg
				);
			
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
		int attackPower = item.itemValue;
		if (item.itemType == ItemType.melee)
		{
			print("Melee Attack: " + item.itemName);
		}
		else if (item.itemType == ItemType.range)
		{
			print("Range Attack: " + item.itemName);
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
}
