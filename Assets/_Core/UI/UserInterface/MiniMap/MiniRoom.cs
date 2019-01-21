using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {
	public class MiniRoom : MonoBehaviour {

		[SerializeField] Image room;
		[SerializeField] Image[] doors;
		bool found;
		bool[] doorsOpen;
		bool quest;

		public void SpawnConfig (bool argQuest) {
			found = false;
			room.enabled = false;
			quest = argQuest;
			foreach (Image door in doors) {
				door.gameObject.SetActive (false);
			}

		}

		public void UpdateActive (bool current) {
			if (current) {
				// Set discovered rooms to active.
				if (!found) {
					room.enabled = true;
					foreach (Image door in doors) {
						door.enabled = true;
					}
					found = true;

				}
				room.color = Color.green;
			} else if (!current && room.enabled && quest) {
				room.color = Color.blue;
			} else {
				room.color = Color.white;
			}
		}

		public void CheckRoomForOpenDoors (RoomGeneration roomGen) {
			doorsOpen = roomGen.ReturnOpenDoors ();
			for (int i = 0; i < doorsOpen.Length; i++) {
				if (doorsOpen[i]) {
					doors[i].gameObject.SetActive (true);
					doors[i].enabled = false;
				}
			}
		}

		public bool GetFound () {
			return found;
		}
	}
}