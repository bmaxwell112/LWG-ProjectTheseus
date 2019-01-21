using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.DatabaseSystem {
	[System.Serializable]
	public class RangedWeapon {

		public int rangedWeaponID;
		public int rangedWeaponItemID;
		public string rangedWeaponName;
		public int rangedWeaponDamage;
		public float rangedWeaponSpeed;
		public float rangedWeaponStartOffset;
		public float rangedWeaponLife;
		public int rangedWeaponSpread;
		public float rangedWeaponDirection;
		public float rangeWeaponRateOfFire;
		public float rangeWeaponPowerUse;

		public RangedWeapon (int id, int itemID, float speed, float offset, float life, int spread, float direction, float rof, float powerUse) {
			rangedWeaponID = id;
			rangedWeaponItemID = itemID;
			rangedWeaponName = Database.instance.items[itemID].itemName;
			rangedWeaponDamage = Database.instance.items[itemID].itemDamage;
			rangedWeaponSpeed = speed;
			rangedWeaponStartOffset = offset;
			rangedWeaponLife = life;
			rangedWeaponSpread = spread;
			rangedWeaponDirection = direction;
			rangeWeaponRateOfFire = rof;
			rangeWeaponPowerUse = powerUse;
		}

		public RangedWeapon () {
			rangedWeaponID = -1;
		}
	}
}