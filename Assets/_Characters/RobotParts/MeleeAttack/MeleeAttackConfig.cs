using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	[CreateAssetMenu(menuName = "Theseus/RobotParts/MeleeArm")]
	public class MeleeAttackConfig : RobotArm {
		
		[Header ("Arm Melee Specific")]
		[SerializeField] float anticipation;
		[SerializeField] float attackTime;
		[SerializeField] bool stopMovement;
		[SerializeField] bool stopTarget;
		public override SpecialBehaviour GetBehaviourComponent(GameObject gameObjectToAttachTo)
		{
			return gameObjectToAttachTo.AddComponent<MeleeAttackBehaviour>();
		}
	}
}