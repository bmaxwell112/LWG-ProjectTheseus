using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core.DialogueSystem {
	public class UserInterfaceDialogueTrigger : MonoBehaviour {

		[SerializeField] Dialogue dialogue;

		public void TriggerDialogue () {
			UserInterfaceDialogue.instance.StartDialogue (dialogue);
		}
	}
}