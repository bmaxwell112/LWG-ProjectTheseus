using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO look at later
using Theseus.Core.DialogueSystem;
using Theseus.ProGen;

namespace Theseus.Core {
	public class TutorialFunctions : MonoBehaviour {

		public static TutorialFunctions instance = null;
		public QuestEvent tutorialEv;
		public QuestController qController;
		DialogueTrigger[] dialogueTriggers;
		bool levelLoaded = false;
		int onDeck = 0;

		void Awake () {
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy (gameObject);
			DontDestroyOnLoad (gameObject);
		}
		void Start () {
			dialogueTriggers = GetComponents<DialogueTrigger> ();
		}
		public void LevelLoaded () {
			DialogueTriggerValue (0);
			levelLoaded = true;
			TutorialSetup ();
		}
		void Update () {
			if (onDeck > 0 && !DialogueManager.dialogueRunning) {
				dialogueTriggers[onDeck].TriggerDialogue ();
				onDeck = 0;
			}
		}
		public void DialogueTriggerValue (int value) {
			if (PlayerPrefsManager.GetTutorial (value) != 1) {
				if (!DialogueManager.dialogueRunning) {
					dialogueTriggers[value].TriggerDialogue ();
					PlayerPrefsManager.SetTutorialComplete (value);
				} else {
					onDeck = value;
				}
			}
		}

		public void TutorialSetup () {
			qController = FindObjectOfType<QuestController> ();
			tutorialEv = QuestFunctions.instance.questEvents[0];
			QuestController.PullQuest ();
			QuestController.currentQuest = tutorialEv;
			//QuestList.lastEvent = tutorialEv;
			qController.BeginQuest ();
		}
	}
}