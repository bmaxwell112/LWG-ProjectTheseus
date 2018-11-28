using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDisplay : MonoBehaviour {

	Text[] text;

	// Use this for initialization
	void Start () {
		text = GetComponentsInChildren<Text>();
	}

	public void UpdateDisplay(QuestEvent quest)
	{
		text[0].text = quest.eventName;
		text[1].text = quest.eventDesc;
	}
}
