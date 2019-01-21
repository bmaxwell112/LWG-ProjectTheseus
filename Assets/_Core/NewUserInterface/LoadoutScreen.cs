using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// TODO look at this later
using Theseus.Character;
using Theseus.DatabaseSystem;

namespace Theseus.Core {
	public class LoadoutScreen : MonoBehaviour {

		[SerializeField] Text[] names, stats;
		[SerializeField] Text nameTxt, infoTxt;
		[SerializeField] Image head, body;
		[SerializeField] Image[] leftArm, rightArm, leftLeg, rightLeg;
		RobotLoadout playerLo;
		public int loadoutIndex;
		int currentIndex;
		bool loadoutCanBeChanged = false;

		void Update () {
			if (GameManager.paused) {
				if (currentIndex != loadoutIndex) {
					UpdateDisplayDetails ();
					currentIndex = loadoutIndex;
				}
				if (loadoutCanBeChanged) {
					ChangeSelectedItem ();
					PauseScreenUpdate ();
				}
			}
		}

		public void LoadLoadoutScreen () {
			if (playerLo == null)
				playerLo = FindObjectOfType<PlayerController> ().GetComponent<RobotLoadout> ();
			PauseScreenUpdate ();
			UpdateDisplayDetails ();
		}

		void PauseScreenUpdate () {

			for (int i = 0; i < playerLo.loadout.Length; i++) {
				names[i].text = playerLo.loadout[i].itemName;
			}
			head.sprite = playerLo.loadout[0].itemSprite[0];
			body.sprite = playerLo.loadout[1].itemSprite[0];
			for (int i = 0; i < leftArm.Length; i++) {
				leftArm[i].sprite = playerLo.loadout[2].itemSprite[i];
			}
			for (int i = 0; i < rightArm.Length; i++) {
				rightArm[i].sprite = playerLo.loadout[3].itemSprite[i];
			}
			leftLeg[0].sprite = playerLo.loadout[4].itemSprite[3];
			leftLeg[1].sprite = playerLo.loadout[4].itemSprite[0];
			rightLeg[0].sprite = playerLo.loadout[4].itemSprite[3];
			rightLeg[1].sprite = playerLo.loadout[4].itemSprite[0];
			for (int i = 0; i < playerLo.loadout.Length; i++) {
				int currentPower = Mathf.RoundToInt (playerLo.power[i] * 100);
				stats[i].text =
					"INTEGRITY: " + playerLo.hitPoints[i] + "/" + playerLo.loadout[i].itemHitpoints + "\n" +
					"POWER: " + currentPower + "/100";
			}

		}

		void UpdatePowerLevels (int itemLoc, PowerMeter meter) {
			if (playerLo.loadout[itemLoc].itemSpecial || playerLo.loadout[itemLoc].itemType == ItemType.range) {
				meter.gameObject.SetActive (true);
				meter.value = playerLo.power[itemLoc] / playerLo.loadout[itemLoc].itemPower;
			} else {
				meter.gameObject.SetActive (false);
			}
		}

		void UpdateDisplayDetails () {
			nameTxt.text = playerLo.loadout[loadoutIndex].itemName;
			infoTxt.text = playerLo.loadout[loadoutIndex].itemDesc;
		}

		void ChangeSelectedItem () {
			if (InputCapture.fireLeftDown) {
				List<Item> items = Database.instance.ItemsByLocation (playerLo.loadout[loadoutIndex].itemLoc);
				for (int i = 0; i < items.Count; i++) {
					if (items[i].itemID == playerLo.loadout[loadoutIndex].itemID && (i - 1) >= 0) {
						playerLo.loadout[loadoutIndex] = items[i - 1];
						playerLo.hitPoints[loadoutIndex] = playerLo.loadout[loadoutIndex].itemHitpoints;
						playerLo.power[loadoutIndex] = playerLo.loadout[loadoutIndex].itemPower;
						UpdateDisplayDetails ();
						return;
					}
				}
			}
			if (InputCapture.fireRightDown) {
				List<Item> items = Database.instance.ItemsByLocation (playerLo.loadout[loadoutIndex].itemLoc);
				for (int i = 0; i < items.Count; i++) {
					if (items[i].itemID == playerLo.loadout[loadoutIndex].itemID && (i + 1) < items.Count) {
						playerLo.loadout[loadoutIndex] = items[i + 1];
						playerLo.hitPoints[loadoutIndex] = playerLo.loadout[loadoutIndex].itemHitpoints;
						playerLo.power[loadoutIndex] = playerLo.loadout[loadoutIndex].itemPower;
						UpdateDisplayDetails ();
						return;
					}
				}
			}
		}
	}
}