using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core.DialogueSystem {
	public class DialogueTrigger : MonoBehaviour {

		[SerializeField] Dialogue dialogue;

		public void TriggerDialogue () {
			DialogueManager.instance.StartDialogue (dialogue);
		}
	}
}