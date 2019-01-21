using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Theseus.ProGen;

namespace Theseus.Core {
	public class QuestDisplay : MonoBehaviour {

		Text[] text;

		// Use this for initialization
		void Start () {
			text = GetComponentsInChildren<Text> ();
		}

		public void UpdateDisplay (QuestEvent quest) {
			text[0].text = quest.eventName;
			text[1].text = quest.eventDesc;
		}

		public void DefaultDisplay () {
			text = GetComponentsInChildren<Text> ();
			text[0].text = "No current quest";
			text[1].text = "When you have an active quest, information will appear here.";
		}
	}
}