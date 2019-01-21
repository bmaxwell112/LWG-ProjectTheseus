using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.DatabaseSystem {
	public enum SpecialProp {
		shield,
		stun,
		bleed,
		cleave,
		heal,
		powerBoost
	}

	[System.Serializable]
	public class SpecialItems {

		public int specialID;
		public int specialItemID;
		public SpecialProp[] specialProps;
		public float specialDuration;
		public int specialDamage;
		public int specialDefence;
		public float specialPowerUse;

		public SpecialItems (int id, int ItemID, SpecialProp[] properties, float duration, int damage, int defense, float powerUse) {
			specialID = id;
			specialItemID = ItemID;
			specialProps = properties;
			specialDuration = duration;
			specialDamage = damage;
			specialDefence = defense;
			specialPowerUse = powerUse;
		}

		public SpecialItems () {
			specialID = -1;
		}
	}
}