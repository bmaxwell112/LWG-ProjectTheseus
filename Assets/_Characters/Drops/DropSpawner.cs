using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class DropSpawner : MonoBehaviour {

		[SerializeField] GameObject drop;

		void Start () {
			DropRoller();

			//dropInst.GetComponent<Drops>().RenameAndReset();
		}

		void DropRoller()
		{
			bool dropped = false;
			GameObject dropInst = Instantiate (drop, transform) as GameObject;
			while(dropped == false)
			{
			dropped = true;
			int rand = Random.Range (7, 34);
			dropInst.GetComponent<Drops> ().databaseItemID = rand;
			//checking to make sure you don't get the Bronze Pipe without having beaten the boss
			if(Theseus.Core.PlayerPrefsManager.GetBossBeaten() == 0 && rand == 28)
			{
			dropped = false;
			}
			}
		}
	}
}