using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : MonoBehaviour {

	BoxCollider2D bc2D;
	[SerializeField] GameObject terminalGUI;
	// Use this for initialization
	void Start () {
		bc2D = GetComponent<BoxCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (bc2D.IsTouchingLayers(LayerMask.NameToLayer("Player")) && !GameManager.gamePaused)
		{
			Instantiate(terminalGUI, FindObjectOfType<UserInterface>().transform);
			GameManager.gamePaused = true;
		}
	}
}
