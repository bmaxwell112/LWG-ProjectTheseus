using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{

    Text newButtonText;
    public QuestEvent refEvent;
    QuestController qController;
    public int thisQuestID;

    // Use this for initialization
    void Start()
    {
        refEvent = QuestController.currentQuest;
        QuestList.lastEvent = refEvent;
        newButtonText = GetComponentInChildren<Text>();
        newButtonText.text = QuestController.currentQuest.eventName;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
