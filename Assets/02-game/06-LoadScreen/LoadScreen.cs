using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadScreen : MonoBehaviour {

	[SerializeField] Image loadingProgress, panel;
	[SerializeField] Button startGame;
	Color currentColor = Color.white;
	float fadeInTime = 1;
	float den;
	bool loaded, ready;
	DialogueTrigger dt;

	// Use this for initialization
	void Start () {
		den = RoomManager.instance.roomCap;
		startGame.gameObject.SetActive(false);
		dt = FindObjectOfType<DialogueTrigger>();
		dt.TriggerDialogue();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		float num = RoomGeneration.roomsInExistence;
		float xScale;
		if (den > 0)
		{
			xScale = num / den;
			loadingProgress.transform.localScale = new Vector3(xScale, 1, 1);
		}
		else
		{
			xScale = 1;
		}
		if (xScale == 1)
		{			
			if (!loaded && !ready && !DialogueManager.dialogueRunning)
			{
				startGame.gameObject.SetActive(true);
				startGame.Select();
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
