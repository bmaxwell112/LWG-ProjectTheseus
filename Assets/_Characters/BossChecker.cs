using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Theseus.Core;

namespace Theseus.Character {
	public class BossChecker : MonoBehaviour {

		// Use this for initialization
		void Start () {

		}

		// Update is called once per frame
		void Update () {
			if (!FindObjectOfType<BossBehavior> ()) {
				GameManager.LoadLevelInGame ("03c Subscribe");
			}
		}
	}
}