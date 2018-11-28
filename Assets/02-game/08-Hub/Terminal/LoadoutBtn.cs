using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoadoutBtn : MonoBehaviour, ISelectHandler {

	[SerializeField] int indexValue;
	LoadoutScreen loadoutGUI;

	// Use this for initialization
	void Start () {
		loadoutGUI = FindObjectOfType<LoadoutScreen>();
	}

	public void OnSelect(BaseEventData eventData)
	{
		loadoutGUI.loadoutIndex = indexValue;
	}
}
