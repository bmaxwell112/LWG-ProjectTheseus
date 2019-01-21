using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO look at this later
using Theseus.Character;
using Theseus.DatabaseSystem;

namespace Theseus.Core {
	public class ItemWheel : MonoBehaviour {

		public static bool active;

		Image[] newArm;
		Image[] leftArm;
		Image[] rightArm;
		Drops itemToSwitch;
		Transform ring;
		RobotLoadout playerLo;
		Animator anim;
		NotificationsPanel np;
		bool disableNotification;
		int location = 0;

		// Use this for initialization
		void Start () {
			ring = transform.Find ("Ring");
			leftArm = ring.Find ("LeftWeapon").GetComponentsInChildren<Image> ();
			rightArm = ring.Find ("RightWeapon").GetComponentsInChildren<Image> ();
			newArm = ring.Find ("Weapon").GetComponentsInChildren<Image> ();
			if (FindObjectOfType<PlayerController> ())
				playerLo = FindObjectOfType<PlayerController> ().GetComponent<RobotLoadout> ();
			else
				Destroy (gameObject);
			anim = GetComponent<Animator> ();
			np = FindObjectOfType<NotificationsPanel> ();
			ring.gameObject.SetActive (false);
		}

		// Update is called once per frame
		void Update () {
			if (ring.gameObject.activeSelf) {
				TrackNewItem ();
			}
		}

		private void TrackNewItem () {
			if (!np) {
				np = FindObjectOfType<NotificationsPanel> ();
			}
			if (InputCapture.hAim <= -0.7) {
				location = -1;
				// Move left	
				np.NotificationsPanelSetEnable ("Switch left " + playerLo.loadout[2].itemName + " out for " + itemToSwitch.thisItem.itemName + "?");
				print ("Moved Left");
			} else if (InputCapture.hAim >= 0.7) {
				location = 1;
				// Move right	
				np.NotificationsPanelSetEnable ("Switch right " + playerLo.loadout[3].itemName + " out for " + itemToSwitch.thisItem.itemName + "?");
				print ("Moved Right");
			}
			if (InputCapture.pickupUp) {
				if (location == 1) {
					itemToSwitch.thisItem = Database.instance.GetArmByLocation (itemToSwitch.thisItem, ItemLoc.rightArm);
					itemToSwitch.PerformTheItemSwitch (playerLo);
					EndTacking ();
				}
				print ("Location " + location);
				if (location == -1) {
					print ("Running This");
					itemToSwitch.thisItem = Database.instance.GetArmByLocation (itemToSwitch.thisItem, ItemLoc.leftArm);
					itemToSwitch.PerformTheItemSwitch (playerLo);
					EndTacking ();
				}
			}
			if (InputCapture.back) {
				EndTacking ();
			}
			anim.SetInteger ("location", location);
		}

		private void EndTacking () {
			//		np.NotificationsPanelDisabled();
			itemToSwitch = null;
			ring.gameObject.SetActive (false);
			active = false;
			GameManager.DisableSlowMotion ();
		}

		public void PickupWeaponWheel (Item newItem, Drops drops) {
			active = true;
			itemToSwitch = drops;
			location = 0;
			GameManager.EnableSlowMotion (0.1f);
			ring.gameObject.SetActive (true);
			SetupWeapon (leftArm, playerLo.loadout[2].itemSprite);
			SetupWeapon (rightArm, playerLo.loadout[3].itemSprite);
			SetupWeapon (newArm, newItem.itemSprite);
		}

		void SetupWeapon (Image[] images, Sprite[] sprites) {
			for (int i = 0; i < images.Length; i++) {

				switch (i) {
					case 0:
						images[i].sprite = sprites[0];
						break;
					case 1:
						images[i].sprite = sprites[2];
						break;
					case 2:
						images[i].sprite = sprites[1];
						break;
					default:
						Debug.Log ("Image legnth is out of range");
						break;
				}
			}
		}
	}
}