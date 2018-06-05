using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrimaryAbility : MonoBehaviour {

    private UserInterface ui;
    [SerializeField] Text abtext;

    // Use this for initialization
    void Start () {
        ui = FindObjectOfType<UserInterface>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckSet();
	}

    void CheckSet()
    {
        if(ui.abilitySet == 0)
        {
            abtext.text = "Block";
        }
        else if(ui.abilitySet == 1)
        {
            abtext.text = "Hacking";
        }
        else if(ui.abilitySet == 2)
        {
            abtext.text = "Dodge Roll";
        }
    }
}
