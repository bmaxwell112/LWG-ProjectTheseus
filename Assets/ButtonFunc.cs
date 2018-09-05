using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunc : MonoBehaviour
{

    Text infoText;

    // Use this for initialization
    void Start()
    {
        ReplaceText();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ReplaceText()
    {
        infoText = FindObjectOfType<InfoDummy>().GetComponent<Text>();
        infoText.text = QuestController.currentQuest.eventDesc;
    }

    public void ActivateButton()
    {
        ReplaceText();
        QuestController.currentQuest = GetComponent<ButtonSetup>().refEvent;
        print("Active quest is now " + GetComponent<ButtonSetup>().refEvent.eventName);
        print("Check via QC - " + QuestController.currentQuest.eventName);
    }
}
