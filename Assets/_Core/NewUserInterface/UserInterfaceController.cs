using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {
	public class UserInterfaceController : MonoBehaviour {

		[SerializeField] GameObject playerLoadOut, dialogue, loadScreen, pauseMenu;

		// Use this for initialization
		void Start () {
			// This Tracks Loadout, Minimap, Items and Notificaitons
			// TODO hook up loadout images
			Instantiate (playerLoadOut, transform);
			Instantiate (dialogue, transform);
			Instantiate (pauseMenu, transform);
			Instantiate (loadScreen, transform);
		}

		// Update is called once per frame
		void Update () {

		}
	}
}