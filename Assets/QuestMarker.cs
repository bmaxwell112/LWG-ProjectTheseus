using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour {
    RoomGeneration containingRoom;
    QuestController qController;
    QuestEvent qMarked;
    GameObject questDrop1;
    private bool firstTimeSetup, questReady;

	// Use this for initialization
	void Start () {
        firstTimeSetup = false;
        questReady = false;
        qController = FindObjectOfType<QuestController>();
        containingRoom = GetComponentInParent<RoomGeneration>();

        qMarked = QuestController.activeEvents[Random.Range(1, QuestController.activeEvents.Count)];
    }
	
	// Update is called once per frame
	void Update () {
        CheckForActivation();
        CheckForCompletion();
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
            RunSetups();
            QuestController.currentQuest = qMarked;
            qController.BeginQuest();
        }
    }

    void CheckForCompletion()
    {
        if (questReady && questDrop1 == null)
        {
            qController.CompleteQuest();
        }
    }

    void RunSetups()
    {
        if(questReady)
        {
            SetupQ1();
        }
    }

    void SetupQ1()
    {
        if(QuestController.currentQuest.eventID == 1)
        {
            //instantiate a drop item as the child of this object, set drop object to chainsaw (itemID 26)
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            questDrop1 = Instantiate(Resources.Load("Drops"), transform.position, Quaternion.identity, currentRoom) as GameObject;
            questDrop1.GetComponent<Drops>().databaseItemID = 26;

            //Set item to special name or identifier? Once that special item is destroyed (picked up), complete quest
        }
    }
}
