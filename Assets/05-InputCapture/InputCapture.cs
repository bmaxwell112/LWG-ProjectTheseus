using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCapture {

	public static float hThrow, vThrow, hAim, vAim;
	public static bool pickup, back, fireLeftDown, fireRightDown, firingLeft, firingRight, pause, triggerRight, triggerLeft, block;
	static bool mouseIsFiringLeft, mouseIsFiringRight;


	public static void InputCheck()
	{
		hThrow = Input.GetAxis("Horizontal");
		vThrow = Input.GetAxis("Vertical");		
		pickup = Input.GetButtonDown("Pickup");
		back = Input.GetButtonDown("Submit");
		pause = Input.GetButtonDown("Cancel");
		block = Input.GetButtonDown("BlockDodge");
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
}
