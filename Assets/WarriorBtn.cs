using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarriorBtn : MonoBehaviour {

    private TechTree ui;

    // Use this for initialization
    void Start () {
        ui = FindObjectOfType<TechTree>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WarriorTree()
    {
        ui.abilitySet = 0;
    }
}
