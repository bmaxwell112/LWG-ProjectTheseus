using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drops : MonoBehaviour {

	public Item thisItem = new Item();

	public void IdentifyItem(Item item)
	{
		thisItem = item;
	}
}
