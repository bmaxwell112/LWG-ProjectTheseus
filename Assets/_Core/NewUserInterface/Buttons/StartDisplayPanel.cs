using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Theseus.Core {
	public class StartDisplayPanel : MonoBehaviour {

		[SerializeField] Sprite[] images;
		[SerializeField] GameObject[] buttons;
		[Tooltip ("Delay in Second")][SerializeField] float StartDelay;
		Image image;
		bool startMonitor = false;
		GameObject selectedButton;
		// Use this for initialization
		void Start () {
			image = GetComponent<Image> ();
			selectedButton = EventSystem.current.currentSelectedGameObject;
			Invoke ("DelayStart", StartDelay);
		}

		void DelayStart () {
			startMonitor = true;
		}

		// Update is called once per frame
		void Update () {
			if (startMonitor) {
				var temp = EventSystem.current.currentSelectedGameObject;
				if (selectedButton != temp) {
					for (int i = 0; i < buttons.Length; i++) {
						if (buttons[i] == temp) {
							image.sprite = images[i];
							selectedButton = temp;
							break;
						}
					}
				}
			}
		}
	}
}