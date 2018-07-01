using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueConversation : MonoBehaviour {

	DialogueTrigger[] dialogues;
	int currentDialogue;
	bool trigger;

	// Use this for initialization
	void Start () {
		dialogues = GetComponents<DialogueTrigger>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!DialogueManager.dialogueRunning)
		{
			Invoke("RunNextDialogue", 1);
			DialogueManager.dialogueRunning = true;
		}
	}

	void RunNextDialogue()
	{
		if (currentDialogue < dialogues.Length)
		{
			dialogues[currentDialogue].TriggerDialogue();
			currentDialogue++;
		}
	}
}
