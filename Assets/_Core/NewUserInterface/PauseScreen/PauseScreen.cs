using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {
	public class PauseScreen : MonoBehaviour {

		[SerializeField] GameObject header, loadOut, questLog;
		QuestListRE questList;
		LoadoutScreen loScreen;
		[SerializeField] Button loadout;
		[SerializeField] Text headerTxt;

		// Use this for initialization
		void Start () {
			questList = GetComponentInChildren<QuestListRE> ();
			loScreen = GetComponentInChildren<LoadoutScreen> ();
			header.SetActive (false);
			loadOut.SetActive (false);
			questLog.SetActive (false);
		}
		void Update () {
			if (InputCapture.pause && !GameManager.paused) {
				PauseScreenActive ();
				//GameManager.GamePause(true);
			} else if (InputCapture.pause && GameManager.paused) {
				BackButton ();
			}
		}

		public void LoudoutButton () {
			ActiveScreen (true, false);
			if (loScreen == null)
				loScreen = GetComponentInChildren<LoadoutScreen> ();
			loScreen.LoadLoadoutScreen ();
			headerTxt.text = "LOADOUT";
		}
		public void QuestButton () {
			ActiveScreen (false, true);
			if (questList == null)
				questList = GetComponentInChildren<QuestListRE> ();
			questList.LoadActiveQuests ();
			headerTxt.text = "QUEST LOG";
		}
		public void BackButton () {
			header.SetActive (false);
			ActiveScreen (false, false);
			GameManager.GamePause (false);
		}
		public void ExitButton () {
			GameManager.GamePause (false);
			GameManager.instance.playerAlive = false;
			LevelManager.LOADLEVEL ("01a Start");
		}
		public void PauseScreenActive () {
			header.SetActive (true);
			questLog.SetActive (true);
			loadout.Select ();
			LoudoutButton ();
			GameManager.GamePause (true);
		}

		public void ActiveScreen (bool lo, bool ql) {
			loadOut.SetActive (lo);
			questLog.SetActive (ql);
		}
	}
}