using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
	public class DropSpawner : MonoBehaviour {

		[SerializeField] GameObject drop;

		void Start () {
			GameObject dropInst = Instantiate (drop, transform) as GameObject;
			int rand = Random.Range (7, 16);
			dropInst.GetComponent<Drops> ().databaseItemID = rand;
			//dropInst.GetComponent<Drops>().RenameAndReset();
		}
	}
}