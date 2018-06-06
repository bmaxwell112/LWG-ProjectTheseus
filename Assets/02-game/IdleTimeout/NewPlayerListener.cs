using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewPlayerListener : MonoBehaviour {

	LevelManager levelManager;
	bool loading;

	void Start()
	{
		levelManager = FindObjectOfType<LevelManager>();
		GameManager.playerAlive = false;
	}

	void Update () {
		if (Input.anyKey && !loading)
		{
			levelManager.LoadLevel("02a Game-1");
			loading = true;
		}
	}
}
