//  ==========================================================
//  				Controls New Scene Loading
//  ==========================================================
//  This exists on the LevelManager prefab which is applied 
//  to several buttons load levels from the menus. The prefab 
//  needs to exist in all game scenes (02) for levels to 
//	load properly.

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

	public float autoLoadNextLevelAfter;
	public int CurrentSceneBuildIndex;

	void Start(){
		if(autoLoadNextLevelAfter <= 0){
		} else {
			Invoke ("LoadNextLevel", autoLoadNextLevelAfter);
		}
		CurrentSceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
	}

	//  When provided a string that matches a scene name that 
	//  exists in the build order it will load that level.
	public void LoadLevel(string name){
		SceneManager.LoadScene (name);
	}
	public void LoadLevelInGame(string name)
	{
		// TODO remove these stupid and redundent things.
		GameManager.GamePause(false);
		GameManager.instance.playerAlive = false;
		SceneManager.LoadScene(name);
	}
	
	public void LoadLevel(int number)
	{
		SceneManager.LoadScene(number);
	}
	public static void LOADLEVEL(string name)
	{
		SceneManager.LoadScene(name);
	}
	public static void LOADLEVEL(int number)
	{
		SceneManager.LoadScene(number);
	}

	//  stops the application
	public void QuitRequest(){
		Application.Quit ();
	}

	//  Loads the next level in the build order
	public void LoadNextLevel(){
		SceneManager.LoadScene(CurrentSceneBuildIndex + 1);
	}

	//  When provided a string it will load it in a web browser 
	//	as a url NOTE: must contain http:// or https:// 
	public void LoadExternalURL(string url)
	{
		Application.OpenURL(url);
	}

	public int GetLevelNumber()
	{
		return SceneManager.GetActiveScene().buildIndex;
	}
}
