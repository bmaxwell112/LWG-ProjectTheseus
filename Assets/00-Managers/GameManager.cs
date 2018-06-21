using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;                 //Static instance of Database which allows it to be accessed by any other script.
	public static bool mouseInput, paused;
	public static int RandomDropModifier;
	public bool playerAlive;
	public static Item[] playerLoadout = new Item[7];

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	void Start()
	{
		RandomDropModifier = 0;		
		mouseInput = MouseCheck();
	}

	private bool MouseCheck()
	{
		string[] joysticks = Input.GetJoystickNames();
		if (joysticks.Length > 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}

	void Update()
	{
		InputCapture.InputCheck();
		print(InputCapture.triggerRight);
		print(InputCapture.triggerLeft);
	}

	public static void GamePause(bool pause)
	{
		paused = pause;
		if (paused)
		{
			Time.timeScale = 0.0f;
			Time.fixedDeltaTime = 0.0f;
		}
		else		
		{
			Time.timeScale = 1.0f;
			Time.fixedDeltaTime = 0.02f;
		}
	}

	public static void SetSavedLoadout(Item[] loadout)
	{
		for (int i = 0; i < loadout.Length; i++)
		{
			playerLoadout[i] = loadout[i];
		}
	}

	public static void LoadLevelInGame(string level)
	{
		SetSavedLoadout(FindObjectOfType<PlayerController>().GetComponent<RobotLoadout>().loadout);
		LevelManager.LOADLEVEL(level);
	}
}
