using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDungeonTrigger : MonoBehaviour {

	[SerializeField] int dungeon;
	bool loading;
	// Use this for initialization
	void OnTriggerStay2D(Collider2D collision)
	{
		if (InputCapture.pickup && !GameManager.paused && collision.gameObject.CompareTag("Player") && !loading)
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
