using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFunctions : MonoBehaviour {

	public static TutorialFunctions instance = null;
	DialogueTrigger[] dialogueTriggers;
	bool levelLoaded = false;
	int onDeck = 0;

	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	void Start()
	{
		dialogueTriggers = GetComponents<DialogueTrigger>();
	}
	public void LevelLoaded()
	{		
		DialogueTriggerValue(0);
		levelLoaded = true;
	}
	void Update()
	{
		if (onDeck > 0 && !DialogueManager.dialogueRunning)
		{
			dialogueTriggers[onDeck].TriggerDialogue();
			onDeck = 0;
		}
	}
	public void DialogueTriggerValue(int value)
	{
		if (PlayerPrefsManager.GetTutorial(value) != 1)
		{
			if (!DialogueManager.dialogueRunning)
			{
				dialogueTriggers[value].TriggerDialogue();
				PlayerPrefsManager.SetTutorialComplete(value);
			}
			else
			{
				onDeck = value;
			}
		}
	}
}
