using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
	public class NewPlayerListener : MonoBehaviour {

		LevelManager levelManager;
		bool loading;

		void Start () {
			levelManager = FindObjectOfType<LevelManager> ();
			GameManager.instance.playerAlive = false;
		}

		void Update () {
			if (Input.anyKey && !loading) {
				levelManager.LoadLevel ("02a Hub");
				loading = true;
			}
		}
	}
}