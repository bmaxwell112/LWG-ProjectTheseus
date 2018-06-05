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
            //PlayerPrefsManager.SetPointValue(1);
            PlayerPrefsManager.ResetPointValue();

			UserInterface ui = FindObjectOfType<UserInterface>();
            if(PlayerPrefsManager.GetPointValue() > 0)
            {
                ui.pointsAvailable = true;
                print("Points Available!");
            }
            else
            {
                ui.loadoutCanBeChanged = true;
            }

			ui.PauseGame();
		}
	}

}
