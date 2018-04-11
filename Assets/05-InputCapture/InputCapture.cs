using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputCapture : MonoBehaviour {

	public static float hThrow, vThrow, hAim, vAim;
	public static bool pickup, pause, fireLeft, fireRight;

	public static void InputCheck()
	{
		hThrow = Input.GetAxis("Horizontal");
		vThrow = Input.GetAxis("Vertical");
		hAim = Input.GetAxis("HorizontalAim");
		vAim = Input.GetAxis("VerticalAim");
		fireLeft = Input.GetButtonDown("Fire1");
		fireRight = Input.GetButtonDown("Fire2");
		pickup = Input.GetButtonDown("Pickup");
	}
}
