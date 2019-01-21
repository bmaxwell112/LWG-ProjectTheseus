using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO look at this later
using Theseus.Character;

public class InputCapture {

	public static float hThrow, vThrow, hAim, vAim;
	public static bool pickup, back, fireLeftDown, fireRightDown, firingLeft, firingRight, pause, triggerRight, triggerLeft, block, pickupUp;
	static bool mouseIsFiringLeft, mouseIsFiringRight;


	public static void InputCheck()
	{
		hThrow = Input.GetAxis("Horizontal");
		vThrow = Input.GetAxis("Vertical");
		pickup = Input.GetButtonDown("Pickup");
		pickupUp = Input.GetButtonUp("Pickup");
		back = Input.GetButtonDown("Submit");
		pause = Input.GetButtonDown("Cancel");
		block = Input.GetButton("BlockDodge");
		if (!GameManager.mouseInput)
		{
			fireLeftDown = Input.GetButtonDown("Fire1");
			fireRightDown = Input.GetButtonDown("Fire2");
			firingLeft = Input.GetButton("Fire1");
			firingRight = Input.GetButton("Fire2");
			triggerLeft = (Input.GetAxis("TriggerL") != 0);
			triggerRight = (Input.GetAxis("TriggerR") != 0);
		}
		else
		{
			if (!Input.GetButton("RangeToggle"))
			{
				fireLeftDown = Input.GetButtonDown("Fire1");
				fireRightDown = Input.GetButtonDown("Fire2");
				firingLeft = Input.GetButton("Fire1");
				firingRight = Input.GetButton("Fire2");
				if (mouseIsFiringLeft)
				{
					triggerLeft = Input.GetButton("Fire1");
					if (!triggerLeft)
						mouseIsFiringLeft = false;
				}
				if (mouseIsFiringRight)
				{
					triggerRight = Input.GetButton("Fire2");
					if (!triggerRight)
						mouseIsFiringRight = false;
				}
			}
			else
			{
				triggerLeft = (Input.GetAxis("Fire1") != 0);
				if (triggerLeft)
					mouseIsFiringLeft = true;
				triggerRight = (Input.GetAxis("Fire2") != 0);
				if (triggerRight)
					mouseIsFiringRight = true;
			}
		}
	}

	public static void ControllerAim()
	{
		hAim = Input.GetAxis("HorizontalAim");
		vAim = Input.GetAxis("VerticalAim");
	}

	public static void MouseAim(Vector2 mouseLocation)
	{
		hAim = -mouseLocation.x;
		vAim = -mouseLocation.y;
	}

	public static bool JoystickOverThreshold(float value)
	{
		if (hThrow > value ||
			hThrow > value * 0.75 && vThrow > value * 0.75 ||
			hThrow > value * 0.75 && vThrow < -value * 0.75 ||
			hThrow < -value ||
			hThrow < -value * 0.75 && vThrow > value * 0.75 ||
			hThrow < -value * 0.75 && vThrow < value * 0.75 ||
			vThrow > value ||
			vThrow < -value)
		{
			return true;
		}
		else
		{
			return false;
		}

	}
	public static Facing JoystickDirection()
	{
		float angle = Utilities.CalculateAngle(Vector2.zero, new Vector2(hThrow, vThrow));
		Debug.Log(angle);
		if (angle >= 22.5f && angle < 67.5f)
		{
			// facing upper left
			return Facing.upperLeft;
		}
		else if (angle >= 67.5f && angle < 112.5f)
		{
			// facing left
			return Facing.left;
		}
		else if (angle >= 112.5f && angle < 157.5f)
		{
			// facing lower left
			return Facing.lowerLeft;
		}
		else if (angle >= 157.5f && angle < 202.5f)
		{
			// facing down
			return Facing.down;
		}
		else if (angle >= 202.5f && angle < 247.5f)
		{
			// facing lower right
			return Facing.lowerRight;
		}
		else if (angle >= 247.5f && angle < 292.5f)
		{
			// facing right
			return Facing.right;
		}
		else if (angle >= 292.5f && angle < 337.5f)
		{
			return Facing.upperRight;
		}
		else if (angle >= 337.5f || angle < 22.5f)
		{
			// facing up
			return Facing.up;
		}
		else
		{
			return Facing.none;
		}
	}
}
