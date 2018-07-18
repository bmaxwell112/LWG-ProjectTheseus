using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour {
    RoomGeneration containingRoom;
    QuestController qController;
    QuestEvent qMarked;
    private bool firstTimeSetup, questReady;

	// Use this for initialization
	void Start () {
        firstTimeSetup = false;
        questReady = false;
        qController = FindObjectOfType<QuestController>();
        containingRoom = GetComponentInParent<RoomGeneration>();

        qMarked = QuestController.activeEvents[Random.Range(0, QuestController.activeEvents.Count)];
    }
	
	// Update is called once per frame
	void Update () {
        CheckForActivation();
    }

    void CheckForActivation()
    {
        if (!containingRoom.GetComponent<MeshRenderer>().enabled && !firstTimeSetup && RoomManager.gameSetupComplete)
        {
            firstTimeSetup = true;
        }

        if (containingRoom.GetComponent<MeshRenderer>().enabled && firstTimeSetup && RoomManager.gameSetupComplete && !questReady)
        {
            questReady = true;
            QuestController.currentQuest = qMarked;
            qController.BeginQuest();
        }
    }
}
