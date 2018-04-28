using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tempui : MonoBehaviour {

	[SerializeField] Text[] texts;
	RobotLoadout inv;
	PlayerController player;

	// Use this for initialization
	void Start () {		
		player = FindObjectOfType<PlayerController>();
		inv = player.GetComponent<RobotLoadout>();
	}
	
	// Update is called once per frame
	void Update () {
		LoadOutCheck();
	}

	private void LoadOutCheck()
	{
		texts[0].text =			
			inv.loadout[(int)ItemLoc.head] .itemName + "\n" +
			inv.loadout[(int)ItemLoc.body] .itemName + "\n" +
			inv.loadout[(int)ItemLoc.leftArm].itemName + "\n" +
			inv.loadout[(int)ItemLoc.rightArm].itemName + "\n" +
			inv.loadout[(int)ItemLoc.legs] .itemName + "\n" +
			inv.loadout[(int)ItemLoc.back] .itemName + "\n" +
			inv.loadout[(int)ItemLoc.core].itemName + "\n" 
			;
		texts[1].text = inv.hitPoints.ToString();
	}
}
