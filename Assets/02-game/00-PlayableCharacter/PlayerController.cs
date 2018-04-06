using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField] float speed;
	
	// Update is called once per frame
	void Update () {
		MovementCheck();
		AimAndFireCheck();
	}

	private void AimAndFireCheck()
	{
		float horizontalThrow = Input.GetAxis("HorizontalAim");
		float verticalThrow = Input.GetAxis("VerticalAim");
		bool fire = Input.GetButton("Fire1");
		bool fire2 = Input.GetButton("Fire2");
		
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
