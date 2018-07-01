using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFunctions : MonoBehaviour {

	public static TutorialFunctions instance = null;
	DialogueTrigger[] dialogueTriggers;
	bool levelLoaded = false;
	int onDeck = 0;
	bool[] triggersTripped;

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
		triggersTripped = new bool[dialogueTriggers.Length];
	}
	public void LevelLoaded()
	{		
		DialogueTriggerValue(0);
		levelLoaded = true;
	}
	void Update()
	{
		if (!DialogueManager.dialogueRunning && !triggersTripped[1] && levelLoaded)
		{
			DialogueTriggerValue(1);
		}
		if (onDeck > 0 && !DialogueManager.dialogueRunning)
		{
			dialogueTriggers[onDeck].TriggerDialogue();
			onDeck = 0;
		}
	}
	public void DialogueTriggerValue(int value)
	{
		if (!triggersTripped[value])
		{
			if (!DialogueManager.dialogueRunning)
			{
				dialogueTriggers[value].TriggerDialogue();
				triggersTripped[value] = true;
			}
			else
			{
				onDeck = value;
			}
		}
	}
}
