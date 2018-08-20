using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour {

    Text newButtonText;
    public QuestEvent refEvent;
    QuestController qController;
    QuestList qList;

    // Use this for initialization
    void Start () {
        refEvent = QuestController.currentQuest;
        qList = FindObjectOfType<QuestList>();
        qList.lastEvent = refEvent;
        newButtonText = GetComponentInChildren<Text>();
        newButtonText.text = QuestController.currentQuest.eventName;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
