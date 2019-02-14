using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class MeleeAttackBehaviour : SpecialBehaviour {
		
		RobotArm config;

		public override void SetConfig(RobotArm configToSet)
		{
			this.config = configToSet;
		}

		public override void Attack(){			
			// Do Melee attacking things here
			Debug.Log("Attacking");
		}
	}
}