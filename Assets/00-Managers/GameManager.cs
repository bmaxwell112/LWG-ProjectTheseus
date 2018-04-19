using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;                 //Static instance of Database which allows it to be accessed by any other script.
	public static bool gamePaused, mouseInput;

	//Awake is always called before any Start functions
	void Awake()
	{
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy(gameObject);
		DontDestroyOnLoad(gameObject);
	}
	void Update()
	{
		InputCapture.InputCheck();
	}	
}
