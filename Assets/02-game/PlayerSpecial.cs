using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpecial : MonoBehaviour {

	public void ActivateSpecialPassive(Item item)
	{
		SpecialItems special = Database.instance.ItemSpecialItem(item);
		for (int i = 0; i < special.specialProps.Length; i++)
		{
			switch (special.specialProps[i])
			{
				case SpecialProp.shield:
					CreateShield();
					break;
				default:
					Debug.LogWarning(item.itemName + " does not have a passive special");
					break;
			}
		}
	}
	public void ActivateSpecialActive1()
	{
		print("Call works: ");
	}
	public void ActivateSpecialActive(Item item, GameObject enemy)
	{
		print("RAN THIS");
		SpecialItems special = Database.instance.ItemSpecialItem(item);
		for (int i = 0; i < special.specialProps.Length; i++)
		{
			switch (special.specialProps[i])
			{
				case SpecialProp.stun:
					StunEnemy(special, enemy);
					break;
				case SpecialProp.bleed:
					// Damage over time
					break;
				case SpecialProp.cleave:
					// break off parts
					break;
				default:
					Debug.LogWarning(item.itemName + " does not have an active special");
					break;
			}
		}
	}
	void CreateShield()
	{
		// Make a Shield
	}
	void StunEnemy(SpecialItems special, GameObject enemyGO)
	{
		try
		{
			EnemyController enemy = enemyGO.GetComponent<EnemyController>();
			if (!enemy.stunned)
			{
				StartCoroutine(UnStunEnemy(special, enemy));
			}
		}
		catch
		{
			Debug.LogWarning("Enemy Controller not found");
			return;
		}

	}
	IEnumerator UnStunEnemy(SpecialItems special, EnemyController enemy)
	{
		enemy.stunned = true;
		yield return new WaitForSeconds(special.specialDuration);
		enemy.stunned = false;
	}
}
