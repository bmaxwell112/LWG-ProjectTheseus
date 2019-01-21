using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
	public class Loader : MonoBehaviour {

		[SerializeField] GameObject database, gameManager, levelManager, musicManager, questFunctions, itemWheel;

		void Awake () {
			if (Database.instance == null) {
				Instantiate (database);
			}
			if (GameManager.instance == null) {
				Instantiate (gameManager);
			}
			if (MusicManager.instance == null) {
				Instantiate (musicManager);
			}
			if (QuestFunctions.instance == null) {
				Instantiate (questFunctions);
			}
			if (!FindObjectOfType<LevelManager> ()) {
				Instantiate (levelManager);
			}
			if (!FindObjectOfType<ItemWheel> ()) {
				Instantiate (itemWheel);
			}
		}
	}
}