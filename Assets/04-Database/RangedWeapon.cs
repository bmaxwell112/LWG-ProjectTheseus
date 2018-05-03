using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RangedWeapon {

	public int rangedWeaponID;
	public int rangedWeaponItemID;	
	public string rangedWeaponName;
	public int rangedWeaponDamage;
	public float rangedWeaponSpeed;
	public float rangedWeaponStartOffset;
	public int rangedWeaponLife;
	public int rangedWeaponSpread;
	public float rangedWeaponDirection;
	public float rangeWeaponRateOfFire;

	public RangedWeapon(int id, int itemID, float speed, float offset, int life, int spread, float direction, float rof)
	{
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
	}

	public RangedWeapon()
	{
		rangedWeaponID = -1;
	}
}
