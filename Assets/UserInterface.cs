using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserInterface : MonoBehaviour {

	[SerializeField] Text[] names, stats, liveStats;
	[SerializeField] Image head, body;
	[SerializeField] Image[] leftArm, rightArm, leftLeg, rightLeg;
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
		names[0].text = playerLo.loadout[0].itemName;
		names[1].text = playerLo.loadout[1].itemName;
		names[2].text = playerLo.loadout[2].itemName;
		names[3].text = playerLo.loadout[3].itemName;
		names[4].text = playerLo.loadout[4].itemName;
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
	}

	void LoadOutCheck()
	{
		liveStats[1].text = "HP:\n" + playerLo.hitPoints;
	}
}
