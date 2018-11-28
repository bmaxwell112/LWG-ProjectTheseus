using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

	[SerializeField] GameObject header, loadOut, techTree, questLog;
	QuestListRE questList;
	LoadoutScreen loScreen;
	[SerializeField] Button loadout;
	[SerializeField] Text headerTxt;

	// Use this for initialization
	void Start () {
		questList = GetComponentInChildren<QuestListRE>();
		loScreen = GetComponentInChildren<LoadoutScreen>();
		header.SetActive(false);
		loadOut.SetActive(false);
		techTree.SetActive(false);
		questLog.SetActive(false);
	}
	void Update()
	{
		if (InputCapture.pause && !GameManager.paused)
		{
			PauseScreenActive();
			//GameManager.GamePause(true);
		}
		else if (InputCapture.pause && GameManager.paused)
		{
			BackButton();
		}
	}

	public void LoudoutButton()
	{
		ActiveScreen(true, false, false);
		if (loScreen == null)
			loScreen = GetComponentInChildren<LoadoutScreen>();
		loScreen.LoadLoadoutScreen();
		headerTxt.text = "LOADOUT";
	}
	public void TechTreeButton()
	{
		ActiveScreen(false, true, false);
		headerTxt.text = "TECH TREE";
	}
	public void QuestButton()
	{
		ActiveScreen(false, false, true);
		if(questList == null)
			questList = GetComponentInChildren<QuestListRE>();
		questList.LoadActiveQuests();
		headerTxt.text = "QUEST LOG";
	}
	public void BackButton()
	{
		header.SetActive(false);
		ActiveScreen(false, false, false);
		GameManager.GamePause(false);
	}
	public void ExitButton()
	{
		GameManager.GamePause(false);
		GameManager.instance.playerAlive = false;
		LevelManager.LOADLEVEL("01a Start");
	}
	public void PauseScreenActive()
	{
		header.SetActive(true);
		questLog.SetActive(true);
		loadout.Select();
		LoudoutButton();
		GameManager.GamePause(true);
	}

	public void ActiveScreen(bool lo, bool tt, bool ql)
	{
		loadOut.SetActive(lo);
		techTree.SetActive(tt);
		questLog.SetActive(ql);
	}
}
