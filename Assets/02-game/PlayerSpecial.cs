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
		SpecialItems special = Database.instance.ItemSpecialItem(item);
		for (int i = 0; i < special.specialProps.Length; i++)
		{
			switch (special.specialProps[i])
			{
				case SpecialProp.stun:
					StunEnemy(special, enemy);
					break;
				case SpecialProp.bleed:
					BleedEnemy(special, enemy);
					break;
				case SpecialProp.cleave:
					CleaveEnemy(enemy);
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

	void CleaveEnemy(GameObject enemy)
	{
		enemy.GetComponent<RobotLoadout>().dropOffset += 5;
	}

	void BleedEnemy(SpecialItems special, GameObject enemy)
	{
		RobotLoadout enemyLo = enemy.GetComponent<RobotLoadout>();
		StartCoroutine(Bleed(special, enemyLo));

	}
	IEnumerator Bleed(SpecialItems special, RobotLoadout enemyLo)
	{
		List<int> liveParts = new List<int>();
		for (int i = 0; i < enemyLo.hitPoints.Length; i++)
		{
			if (enemyLo.hitPoints[i] > 0)
			{
				liveParts.Add(i);
				// twice as likely to hit everything but the head
				if (enemyLo.loadout[i].itemLoc != ItemLoc.head)
				{
					liveParts.Add(i);
				}
			}
		}
		int rand = Random.Range(0, liveParts.Count);		
		while (enemyLo.hitPoints[liveParts[rand]] > 0 || enemyLo)
		{
			enemyLo.hitPoints[liveParts[rand]] -= special.specialDamage;
			yield return new WaitForSeconds(special.specialDuration);
		}
	}
}
