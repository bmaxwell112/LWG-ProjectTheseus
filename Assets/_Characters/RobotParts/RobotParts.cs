using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {

	public abstract class RobotParts : ScriptableObject {
		public enum Part
		{
			head,
			body,
			arm,
			leg,
			back,
			core
		}
		[Header ("Robot Part General")]		
		[SerializeField] Part partType;
		[SerializeField] string partName;
		[SerializeField] string partDesc;
		[SerializeField] int partRarity;
		[SerializeField] float partHitPoints;
		[SerializeField] Sprite[] partSprites;
		float currentHitPoints;		

		public void TakeDamage(float damage)
		{
			currentHitPoints = Mathf.Clamp(currentHitPoints - damage, 0, partHitPoints);
		}
		public float GetCurrentHitPoints()
		{
			return currentHitPoints;
		}
		public Part GetPartType()
		{
			return partType;
		}
	}
}