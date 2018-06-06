using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToGame : MonoBehaviour {

    private UserInterface ui;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Unpause()
    {
        ui = FindObjectOfType<UserInterface>();
        ui.ResumeGame();
    }
}
