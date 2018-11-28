using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LiveLoadoutDisplay : MonoBehaviour {

	[SerializeField] Image[] liveStats;
	[SerializeField] PowerMeter LeftArmPower, RightArmPower, core, backpack;
	RobotLoadout playerLo;

	// Use this for initialization
	void Start()
	{
		playerLo = FindObjectOfType<PlayerController>().GetComponent<RobotLoadout>();
	}

	// Update is called once per frame
	void Update () {
		LoadOutCheck();
	}
	void LoadOutCheck()
	{
		for (int i = 0; i < playerLo.loadout.Length; i++)
		{
			if (liveStats[i].sprite)
			{
				float tempPoints = playerLo.hitPoints[i];
				float tempTotal = playerLo.loadout[i].itemHitpoints;
				float hitPointPercent = tempPoints / tempTotal;
				//print(playerLo.loadout[i].itemName + ": " + hitPointPercent);
				if (hitPointPercent <= 0)
				{
					liveStats[i].color = Color.grey;
					RobotFunctions.itemDropMultiplier[i] = 5;
				}
				else if (hitPointPercent <= 0.25f && hitPointPercent > 0)
				{
					liveStats[i].color = Color.red;
					RobotFunctions.itemDropMultiplier[i] = 4;
				}
				else if (hitPointPercent <= 0.50f && hitPointPercent > 0.25f)
				{
					liveStats[i].color = new Color(1, 0.65f, 0, 1);
					RobotFunctions.itemDropMultiplier[i] = 3;
				}
				else if (hitPointPercent <= 0.75f && hitPointPercent > 0.50f)
				{
					liveStats[i].color = Color.yellow;
					RobotFunctions.itemDropMultiplier[i] = 2;
				}
				else
				{
					liveStats[i].color = Color.green;
					RobotFunctions.itemDropMultiplier[i] = 1;
				}
			}
		}
		// Left arm
		UpdatePowerLevels(2, LeftArmPower);
		UpdatePowerLevels(3, RightArmPower);
		UpdatePowerLevels(5, backpack);
		UpdatePowerLevels(6, core);
	}
	void UpdatePowerLevels(int itemLoc, PowerMeter meter)
	{
		if (playerLo.loadout[itemLoc].itemSpecial || playerLo.loadout[itemLoc].itemType == ItemType.range)
		{
			meter.gameObject.SetActive(true);
			meter.value = playerLo.power[itemLoc] / playerLo.loadout[itemLoc].itemPower;
		}
		else
		{
			meter.gameObject.SetActive(false);
		}
	}
}
