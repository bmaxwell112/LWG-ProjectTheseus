using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionTEMP : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Player")
		{
			LevelManager.LOADLEVEL("01a Start");
		}
	}
}
