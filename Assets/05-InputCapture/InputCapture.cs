using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCapture {

	public static float hThrow, vThrow, hAim, vAim;
	public static bool pickup, back, fireLeftDown, fireRightDown, firingLeft, firingRight, pause, block;

	public static void InputCheck()
	{
		hThrow = Input.GetAxis("Horizontal");
		vThrow = Input.GetAxis("Vertical");
		fireLeftDown = Input.GetButtonDown("Fire1");
		fireRightDown = Input.GetButtonDown("Fire2");
		firingLeft = Input.GetButton("Fire1");
		firingRight = Input.GetButton("Fire2");
		pickup = Input.GetButtonDown("Pickup");
		back = Input.GetButtonDown("Submit");
		pause = Input.GetButtonDown("Cancel");
        block = Input.GetButtonDown("BlockDodge");
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
