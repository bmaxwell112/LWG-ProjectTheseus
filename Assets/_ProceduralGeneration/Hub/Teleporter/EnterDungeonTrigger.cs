using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.Core;

public class EnterDungeonTrigger : MonoBehaviour {

	[SerializeField] int dungeon;
	bool loading;
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D collision)
	{
		if (!loading)
		{			
			StartCoroutine(LoadDungeon());
			loading = true;
		}
	}
	IEnumerator LoadDungeon()
	{
		GameManager.paused = true;
		yield return new WaitForSeconds(0.5f);
		GameManager.paused = false;
		GameManager.LoadLevelInGame("02a Game-" + dungeon);		
	}
}
