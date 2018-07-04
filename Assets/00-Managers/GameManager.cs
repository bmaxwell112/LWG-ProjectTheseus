using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;                 //Static instance of Database which allows it to be accessed by any other script.
	public static bool mouseInput, paused, levelLoaded;
	public static int RandomDropModifier;
	public bool playerAlive;
	public static Item[] playerLoadout = new Item[7];
	bool dialogueFlag = false;
    static int level;

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
        level = SceneManager.GetActiveScene().buildIndex;
	}

	private void DialogueTracker()
	{
		if (DialogueManager.dialogueRunning && !dialogueFlag)
		{
			StartCoroutine(PauseForDialogue());
			dialogueFlag = true;
		}
		else if (!DialogueManager.dialogueRunning && dialogueFlag)
		{
			GamePause(false);
			dialogueFlag = false;
		}
	}
	IEnumerator PauseForDialogue()
	{
		yield return new WaitForSeconds(0.25f);
		GamePause(true);
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
		DialogueTracker();
		print(InputCapture.triggerRight);
		print(InputCapture.triggerLeft);

        if(level != SceneManager.GetActiveScene().buildIndex)
        {
            StartCoroutine(RunMeOnce());
            level = SceneManager.GetActiveScene().buildIndex;
        }
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

    IEnumerator RunMeOnce()
    {
        levelLoaded = true;
        yield return null;
        levelLoaded = false;
    }
}
