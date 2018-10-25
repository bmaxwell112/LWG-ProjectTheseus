using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseScreen : MonoBehaviour {

	[SerializeField] GameObject header, loadOut, techTree, questLog;
	QuestListRE questList;
	[SerializeField] Button loadout;

	// Use this for initialization
	void Start () {
		questList = GetComponentInChildren<QuestListRE>();
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
	}

	public void LoudoutButton()
	{
		ActiveScreen(true, false, false);
	}
	public void TechTreeButton()
	{
		ActiveScreen(false, true, false);
	}
	public void QuestButton()
	{
		ActiveScreen(false, false, true);
		questList.LoadActiveQuests();
	}
	public void BackButton()
	{
		header.SetActive(false);
		ActiveScreen(false, false, false);
		//GameManager.GamePause(false);
	}
	public void PauseScreenActive()
	{
		header.SetActive(true);
		questLog.SetActive(true);
		loadout.Select();
	}

	public void ActiveScreen(bool lo, bool tt, bool ql)
	{
		loadOut.SetActive(lo);
		techTree.SetActive(tt);
		questLog.SetActive(ql);
	}
}
