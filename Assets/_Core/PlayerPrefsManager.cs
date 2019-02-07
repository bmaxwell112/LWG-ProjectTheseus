/*	
==========================================================
	Manages the Player Prefs(stored data)
==========================================================
This script contains functions that can be called by any
other scripts to set or get stored game data such as levels
and players unlocked, high scores, and setting preferences.
*/

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Theseus.Core {
	public class PlayerPrefsManager : MonoBehaviour {

		const string GAME_CHECK = "game_check";
		const string MASTER_VOLUME_KEY = "master_volume";
		const string SFX_VOLUME_KEY = "SFX_volume";

		const string POINT_VALUE = "point_value";
		const string TREE_KEY = "techtree_unlocked_";

		const string EVENT_COMPLETED = "event_completed_";

		const string BOSS_BEATEN = "boss_beaten_";

		public static void SetGameCheck (int playedBool) {
			PlayerPrefs.SetInt (GAME_CHECK, playedBool);
		}
		public static int GetGameCheck () {
			return PlayerPrefs.GetInt (GAME_CHECK);
		}

		public static void SetMasterVolume (float volume) {
			if (volume >= 0f && volume <= 1f) {
				PlayerPrefs.SetFloat (MASTER_VOLUME_KEY, volume);
			} else {
				Debug.LogError ("Master volume out of range");
			}
		}
		public static float GetMasterVolume () {
			return PlayerPrefs.GetFloat (MASTER_VOLUME_KEY);
		}

		public static void SetSFXVolume (float volume) {
			if (volume >= 0f && volume <= 1f) {
				PlayerPrefs.SetFloat (SFX_VOLUME_KEY, volume);
			} else {
				Debug.LogError ("SFX volume out of range");
			}
		}
		public static float GetSFXVolume () {
			return PlayerPrefs.GetFloat (SFX_VOLUME_KEY);
		}

		public static void SetPointValue (int value) {
			PlayerPrefs.SetInt (POINT_VALUE, value);
		}

		public static int GetPointValue () {
			return PlayerPrefs.GetInt (POINT_VALUE);
		}

		public static void ResetPointValue () {
			PlayerPrefs.SetInt (POINT_VALUE, 0);
		}

		// ATTN:CODY is there a reason you are providing the int as an argument
		// If you are setting to complete it would always be set to 1, 
		// therefore 1 can go into the PlayerPreds set function.
		public static void SetEventComplete (int eventID, int completedBool) {
			PlayerPrefs.SetInt (EVENT_COMPLETED + eventID, completedBool);
		}

		// ATTN:CODY the two functions below are the same the first one is just a dead end.
		public static void GetEventComplete (int eventID) {
			PlayerPrefs.GetInt (EVENT_COMPLETED + eventID);
		}

		public static int ReturnEventComplete (int eventID) {
			return PlayerPrefs.GetInt (EVENT_COMPLETED + eventID);
		}

		public static void ResetEventComplete (int eventID) {
			PlayerPrefs.GetInt (EVENT_COMPLETED + eventID, 0);
		}

		const string TUTORIAL = "tutorial_";

		public static void SetTutorialComplete (int id) {
			PlayerPrefs.SetInt (TUTORIAL + id, 1);
		}
		public static int GetTutorial (int id) {
			if (PlayerPrefs.HasKey (TUTORIAL + id)) {
				return PlayerPrefs.GetInt (TUTORIAL + id);
			} else {
				return 0;
			}
		}

		public static void SetBossBeaten (int id)
		{
			PlayerPrefs.SetInt (BOSS_BEATEN + id, 1);
		}

		public static int GetBossBeaten (){
				return PlayerPrefs.GetInt (BOSS_BEATEN);
		}
		
		public static void ResetBossBeaten (int id) {
			PlayerPrefs.GetInt (BOSS_BEATEN + id, 0);
		}
	}
}