using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RogueBtn : MonoBehaviour {

    private UserInterface ui;

    // Use this for initialization
    void Start () {
        ui = FindObjectOfType<UserInterface>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RogueTree()
    {
        ui.abilitySet = 2;
    }
}
