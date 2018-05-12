using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadoutBtn : MonoBehaviour, ISelectHandler {

	[SerializeField] int indexValue;
	UserInterface loadoutGUI;

	// Use this for initialization
	void Start () {
		loadoutGUI = FindObjectOfType<UserInterface>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		loadoutGUI.loadoutIndex = indexValue;
	}
}
