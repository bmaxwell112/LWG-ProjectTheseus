using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {

	public struct AbilityUseParams
	{
		public IDamageable target;
		public float baseDamage;

		public AbilityUseParams(IDamageable target, float baseDamage)
		{
			this.target = target;
			this.baseDamage = baseDamage;
		}
	}

	public abstract class RobotArm : RobotParts {

		[Header ("Arm General")]
		[SerializeField] AnimatorOverrideController[] meleeAnim = new AnimatorOverrideController[2];
		[SerializeField] AudioClip[] meleeAudio;

		protected SpecialBehaviour behaviour;
		
		public abstract SpecialBehaviour GetBehaviourComponent(GameObject gameObjectToAttachTo);

		public void AttachAbilityTo(GameObject gameObjectToAttachTo)
		{
			SpecialBehaviour behaviourComponent = GetBehaviourComponent(gameObjectToAttachTo);
			behaviourComponent.SetConfig(this);
			behaviour = behaviourComponent;
		}	

		public void Attack()
		{
			behaviour.Attack();
		}
	}
}