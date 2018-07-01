using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionTEMP : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
            //change this to Hub
			GameManager.LoadLevelInGame("03c Subscribe");
		}
	}
}
