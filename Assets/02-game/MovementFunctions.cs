using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementFunctions {
	public static Vector3 LookAt2D(Transform trans, float x, float y)
	{
		Vector3 rotation = new Vector3(
				trans.eulerAngles.x,
				trans.eulerAngles.y,
				Mathf.Atan2(-x, y) * Mathf.Rad2Deg
				);
		return rotation;
	}
	public static Vector3 Knockback2D(Vector3 startLocation, float knockbackDistance, float time)
	{
		Vector3 knockback = Vector3.Slerp(
			startLocation,
			startLocation - (Vector3.down * knockbackDistance),
			time
			);
		return knockback;
	}
}
