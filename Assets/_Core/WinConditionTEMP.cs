using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
	public class WinConditionTEMP : MonoBehaviour {

		void OnCollisionEnter2D (Collision2D coll) {
			if (coll.gameObject.tag == "Player") {
				//change this to Next in Load Order
				GameManager.LoadLevelInGame ("02a Periphetes");
			}
		}
	}
}