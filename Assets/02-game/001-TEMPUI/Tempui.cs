using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tempui : MonoBehaviour {

	[SerializeField] Text text;
	PlayerInventory inv;
	PlayerController player;

	// Use this for initialization
	void Start () {
		inv = FindObjectOfType<PlayerInventory>();
		player = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		LoadOutCheck();
	}

	private void LoadOutCheck()
	{
		text.text =
			player.hitPoints + "\n\n\n\n" +
			inv.inventory[0].itemName + "\n" +
			inv.inventory[1].itemName + "\n" +
			inv.inventory[2].itemName + "\n" +
			inv.inventory[3].itemName + "\n" +
			inv.inventory[4].itemName + "\n" +
			inv.inventory[5].itemName + "\n" +
			inv.inventory[6].itemName + "\n" 
			;
	}
}
