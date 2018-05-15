using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

	BoxCollider2D bc2D;
	bool touchingMe;
	// Use this for initialization
	void Start () {
		bc2D = GetComponent<BoxCollider2D>();		
	}

	void OnCollisionStay2D(Collision2D collision)
	{
		if (InputCapture.pickup && !GameManager.gamePaused && collision.gameObject.CompareTag("Player"))
		{
			UserInterface ui = FindObjectOfType<UserInterface>();
			ui.loadoutCanBeChanged = true;
			ui.PauseGame();
		}
	}

}
