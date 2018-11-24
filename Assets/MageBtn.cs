using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBtn : MonoBehaviour {

    private TechTree ui;

    // Use this for initialization
    void Start()
    {
        ui = FindObjectOfType<TechTree>();
    }

    // Update is called once per frame
    void Update () {
		
	}

    public void MageTree()
    {
        ui.abilitySet = 1;
    }
}
