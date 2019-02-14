using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public abstract class SpecialBehaviour : MonoBehaviour {
		RobotArm arm;

		public abstract void SetConfig(RobotArm config);
				
		public abstract void Attack();
	}
}