using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBtn : MonoBehaviour {

    private UserInterface ui;

    // Use this for initialization
    void Start () {
        ui = FindObjectOfType<UserInterface>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WarriorTree()
    {
        ui.abilitySet = 0;
    }
}
