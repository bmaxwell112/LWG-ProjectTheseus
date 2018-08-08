using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MeleeWeapon {

	public int meleeWeaponID;
	public int meleeWeaponItemID;
	public float meleeWeaponAnticipation;
	public float meleeWeaponAttackTime;
	public bool meleeWeaponStopMovement;
	public bool meleeWeaponStopTarget;	

	public MeleeWeapon(int id, int itemID, float timeInSecondsToHit, float attackTimeInSeconds, bool stopMovement, bool stopTarget)
	{
		meleeWeaponID = id;
		meleeWeaponItemID = itemID;
		meleeWeaponAnticipation = timeInSecondsToHit;
		meleeWeaponAttackTime = attackTimeInSeconds;
		meleeWeaponStopMovement = stopMovement;
		meleeWeaponStopTarget = stopTarget;		
	}
	public MeleeWeapon()
	{
		meleeWeaponID = -1;
	}
}
