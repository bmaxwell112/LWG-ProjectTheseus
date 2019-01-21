using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Theseus.Core;

// TODO look at later
using Theseus.Core.DialogueSystem;

public class LoadScreen : MonoBehaviour {

	[SerializeField] Image loadingProgress, panel;
	Color currentColor = Color.white;
	float fadeInTime = 1;
	float den;
	bool loaded, ready;
	UserInterfaceDialogueTrigger dt;

	// Use this for initialization
	void Start () {
		den = RoomManager.instance.roomCap;
		dt = FindObjectOfType<UserInterfaceDialogueTrigger>();
		if (dt)
		{
			dt.TriggerDialogue();
		}
	}
	void Update()
	{
		
		if (RoomManager.gameSetupComplete && InputCapture.back)
		{
			StartGame();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float num = RoomGeneration.roomsInExistence;
		float xScale;
		if (den > 0)
		{
			xScale = num / den;
			loadingProgress.fillAmount = xScale;
		}
		else
		{
			xScale = 1;
		}
		if (xScale >= 1)
		{			
			if (!loaded && !ready && !UserInterfaceDialogue.dialogueRunning)
			{
				ready = true;
			}
		}
		if (loaded)
		{
			float alphaChange = Time.deltaTime / fadeInTime;
			currentColor.a -= alphaChange;
			panel.color = currentColor;
		}
	}

	private void Destroy()
	{
		TutorialFunctions tutorial = FindObjectOfType<TutorialFunctions>();
		if (tutorial)
		{
			tutorial.LevelLoaded();
		}
		Destroy(gameObject);
	}

	public void StartGame()
	{
		if (!loaded && ready)
		{
			Invoke("Destroy", fadeInTime);	
			loaded = true;
		}
	}
}
