using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour {

    [SerializeField] Text newButtonText;
    QuestController qController;

    // Use this for initialization
    void Start () {
        print(QuestController.currentQuest.eventName);
        //newButtonText.text = QuestController.currentQuest.eventName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
