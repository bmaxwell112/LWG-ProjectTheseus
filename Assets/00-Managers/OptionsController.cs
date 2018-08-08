/*
==========================================================
	controls everything on the options scene
==========================================================
The script exists on OptionsController prefab it manages 
changes in the options scene from what the sliders do to
setting preferences for jump guide.
 */

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OptionsController : MonoBehaviour {

	public Slider volumeSlider;
	public Slider sfxVolumeSlider;
	public LevelManager levelManager;
	[SerializeField] Button resetButton, backBtn, defaultBtn, resetBtn;

	private GameObject confirmReset;
	private MusicManager musicManager;

	// Use this for initialization
	void Start () {
		musicManager = FindObjectOfType<MusicManager>();
		//Debug.Log(musicManager);		
		volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
		sfxVolumeSlider.value = PlayerPrefsManager.GetSFXVolume();		
		confirmReset = GameObject.Find("ConfirmReset");
		confirmReset.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if (musicManager)
		{
			musicManager.ChangeVolume(volumeSlider.value);
		}
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		PlayerPrefsManager.SetSFXVolume(sfxVolumeSlider.value);
	}
	
	// Used on the exit button for the scene
	public void SaveAndExit(){
		PlayerPrefsManager.SetMasterVolume (volumeSlider.value);
		levelManager.LoadLevel("01a Start");
	}

	/* Used on Default button
	 sets the volume default values. */
	public void SetDefaults(){
		volumeSlider.value = 0.65f;
		sfxVolumeSlider.value = 0.75f;
	}

	/* Used on Yes confirmation button 
	   resets data for all listed in the function */
	public void ResetAll()
	{
		PlayerPrefs.DeleteAll();
		SetDefaults();
		confirmReset.SetActive(false);
		MainSelectable(true);
		backBtn.Select();

	}


	// turns on reset warning
	public void ResetBtn()
	{
		confirmReset.SetActive(true);
		MainSelectable(false);
		resetButton.Select();
	}

	/* turns off reset warning
	this is on bothe the No button as well as the 
	image in the background of the warning screen. */
	public void ResetCancelBtn()
	{
		confirmReset.SetActive(false);
		MainSelectable(true);
		backBtn.Select();
	}

	public void MainSelectable(bool selectable)
	{
		backBtn.interactable = selectable;
		defaultBtn.interactable = selectable;
		resetBtn.interactable = selectable;
	}
}
