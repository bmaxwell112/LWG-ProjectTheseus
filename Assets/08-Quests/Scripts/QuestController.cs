using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

    private RoomManager roomManager;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        roomManager = FindObjectOfType<RoomManager>();
        LoadQuestStatus();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void LoadQuestStatus()
    {
        for (int i = 0; i < QuestFunctions.questEvents.Count; i++)
        {
            PlayerPrefsManager.GetEventComplete(i);
            print("Events Loaded");
        }
    }
}
