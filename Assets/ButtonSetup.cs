using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour {

    [SerializeField] Text newButtonText;
    QuestEvent refEvent;
    QuestController qController;

    // Use this for initialization
    void Start () {
        refEvent = QuestController.currentQuest;
        newButtonText.text = QuestController.currentQuest.eventName;
        transform.SetAsFirstSibling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
