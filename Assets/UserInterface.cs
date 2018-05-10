using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	[SerializeField] Text[] names, stats;
	[SerializeField] Image head, body;
	[SerializeField] Image[] leftArm, rightArm, leftLeg, rightLeg, liveStats;
	[SerializeField] GameObject PauseScreen;
	RobotLoadout playerLo;	

	// Use this for initialization
	void Start () {

		playerLo = FindObjectOfType<PlayerController>().GetComponent<RobotLoadout>();
		PauseSceenUpdate();
		PauseScreen.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (InputCapture.pause && GameManager.paused)
		{
			GameManager.GamePause(false);
			PauseScreen.SetActive(false);
		}
		else if (InputCapture.pause && !GameManager.paused)
		{
			GameManager.GamePause(true);			
			PauseScreen.SetActive(true);
			PauseSceenUpdate();
		}
		LoadOutCheck();
	}

	void PauseSceenUpdate()
	{
		for (int i = 0; i < playerLo.loadout.Length; i++)
		{
			names[i].text = playerLo.loadout[i].itemName;
		}		
		head.sprite = playerLo.loadout[0].itemSprite[0];
		body.sprite = playerLo.loadout[1].itemSprite[0];
		for (int i = 0; i < leftArm.Length; i++)
		{
			leftArm[i].sprite = playerLo.loadout[2].itemSprite[i];
		}
		for (int i = 0; i < rightArm.Length; i++)
		{
			rightArm[i].sprite = playerLo.loadout[3].itemSprite[i];
		}
		leftLeg[0].sprite = playerLo.loadout[4].itemSprite[3];
		leftLeg[1].sprite = playerLo.loadout[4].itemSprite[0];
		rightLeg[0].sprite = playerLo.loadout[4].itemSprite[3];
		rightLeg[1].sprite = playerLo.loadout[4].itemSprite[0];
		for (int i = 0; i < playerLo.loadout.Length; i++)
		{
			stats[i].text =
				"Integrity: " + playerLo.hitPoints[i] + "/" + playerLo.loadout[i].itemHitpoints + "\n" +
				"Power: " + "ERR" + "/" + playerLo.loadout[i].itemPower;
		}
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
				}
				else if (hitPointPercent <= 0.25f && hitPointPercent > 0)
				{
					liveStats[i].color = Color.red;
				}
				else if (hitPointPercent <= 0.50f && hitPointPercent > 0.25f)
				{
					liveStats[i].color = new Color(1, 0.65f, 0, 1); ;
				}
				else if (hitPointPercent <= 0.75f && hitPointPercent > 0.50f)
				{
					liveStats[i].color = Color.yellow;
				}
				else 
				{
					liveStats[i].color = Color.green;
				}
			}
		}
	}
}
