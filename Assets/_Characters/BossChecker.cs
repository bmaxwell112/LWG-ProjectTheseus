using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theseus.Core;

namespace Theseus.Character {
	public class BossChecker : MonoBehaviour {

		Theseus.ProGen.QuestController questController;
		NotificationsPanel notifPanel;

		// Use this for initialization
		void Start () {
			
		}

		// Update is called once per frame
		void Update () {
			EndOfGame();
		}

		void EndOfGame()
		{
		if (!FindObjectOfType<BossBehavior> ()) {
			notifPanel = FindObjectOfType<NotificationsPanel>();
			string newText = "You can now find Periphetes' Bronze Pipe!";
            notifPanel.NotificationsPanelSetEnable(newText);
			Theseus.Core.PlayerPrefsManager.SetBossBeaten(1);
			GameManager.LoadLevelInGame ("03c Subscribe");
		
			}
		}
	}
}