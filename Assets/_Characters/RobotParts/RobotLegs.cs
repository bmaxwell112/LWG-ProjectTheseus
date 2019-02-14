using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	[CreateAssetMenu (menuName = "Theseus/RobotParts/Legs")]
	public class RobotLegs : RobotParts {
		[Header ("Leg Specific")]
		[SerializeField] float speed;

		public float GetSpeed()
		{
			return speed;
		}
	}
}