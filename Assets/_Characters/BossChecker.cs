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
			string newText = "Enemies can now drop Periphetes' Bronze Pipe!";
            notifPanel.NotificationsPanelSetEnable(newText);
			Theseus.Core.PlayerPrefsManager.SetBossBeaten(1);
			StartCoroutine(WaitAndEndScene());
		
			}
		}

		IEnumerator WaitAndEndScene(){
			yield return new WaitForSeconds(5);
			GameManager.LoadLevelInGame ("03c Subscribe");
		}
	}
}