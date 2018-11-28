using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSetup : MonoBehaviour
{

    Text newButtonText;
    public QuestEvent refEvent;
	QuestDisplay display;
	
	public void SetupQuestButton(QuestEvent quest)
	{		
		refEvent = quest;
		newButtonText = GetComponentInChildren<Text>();
		display = FindObjectOfType<QuestDisplay>();
		newButtonText.text = refEvent.eventName;
	}

	public void ActivateButton()
	{
		display.UpdateDisplay(refEvent);
		QuestController.currentQuest = GetComponent<ButtonSetup>().refEvent;
	}
}
