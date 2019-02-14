using System.Collections;
using System.Collections.Generic;
using Theseus.Core;
using UnityEngine;

namespace Theseus.ProGen {
	public class Terminal : MonoBehaviour {

		BoxCollider2D bc2D;

		QuestController qController;
		bool touchingMe;
		// Use this for initialization
		void Start () {
			bc2D = GetComponent<BoxCollider2D> ();
			qController = FindObjectOfType<QuestController>();
		}

		void OnCollisionStay2D (Collision2D collision) {
			if (InputCapture.pickup && !GameManager.paused && collision.gameObject.CompareTag ("Player") && !touchingMe) {
				//PlayerPrefsManager.SetPointValue(1);
				PlayerPrefsManager.ResetPointValue ();
				// TODO Fix points
				//if(PlayerPrefsManager.GetPointValue() > 0)
				//{
				//    ui.pointsAvailable = true;
				//}
				//else
				//{
				//    ui.loadoutCanBeChanged = true;
				//}

				if (QuestController.currentQuest.eventID == 0) {
					print ("Completed tutorial");
					qController.CompleteCurrentQuest ();
				}
				touchingMe = true;
			}
			if (GameManager.paused && touchingMe) {
				touchingMe = false;
			}
		}

	}
}