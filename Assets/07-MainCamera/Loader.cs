using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour {

	[SerializeField] GameObject database, gameManager, levelManager, musicManager;

	void Awake () {
		if (Database.instance == null)
		{
			Instantiate(database);
		}
		if (GameManager.instance == null)
		{
			Instantiate(gameManager);
		}
		if (MusicManager.instance == null)
		{
			Instantiate(musicManager);
		}
		if (!FindObjectOfType<LevelManager>())
		{
			Instantiate(levelManager);
		}
	}	
}
