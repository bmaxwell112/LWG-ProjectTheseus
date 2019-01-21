using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class MovementFunctions {
		public static Vector3 LookAt2D (Transform trans, float x, float y) {
			Vector3 rotation = new Vector3 (
				trans.eulerAngles.x,
				trans.eulerAngles.y,
				Mathf.Atan2 (-x, y) * Mathf.Rad2Deg
			);
			return rotation;
		}
	}
}