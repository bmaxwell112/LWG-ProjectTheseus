using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestMarker : MonoBehaviour {
    RoomGeneration containingRoom;
    QuestController qController;
    QuestEvent qMarked;
    GameObject questDrop1, specialTurret;
    GeneralAI specialDrone;
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
            QuestController.currentQuest = qMarked;
            RunSetups();
            qController.BeginQuest();
        }
    }

    void CheckForCompletion()
    {
        if (QuestController.currentQuest.eventID == 1 && questReady && questDrop1 == null)
        {
            QuestController.CompleteCurrentQuest();
        }
    }

    void RunSetups()
    {
        if(questReady)
        {
            SetupAll();
        }
    }

    void SetupAll()
    {
        if(QuestController.currentQuest.eventID == 1)
        {
            //instantiate a drop item as the child of this object, set drop object to chainsaw (itemID 26)
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            questDrop1 = Instantiate(Resources.Load("Drops"), transform.position, Quaternion.identity, currentRoom) as GameObject;
            questDrop1.GetComponent<Drops>().databaseItemID = 26;
            //Set item to special name or identifier? Once that special item is destroyed (picked up), complete quest
        }

        if (QuestController.currentQuest.eventID == 2)
        {
            //spawns an enemy spawner, which makes a drone, need to make a special drone for this later
            //set a boolean and an ID to specify if there is a specific drop by ID
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            specialDrone = Instantiate(Resources.Load("Drone"), transform.position, Quaternion.identity, currentRoom) as GeneralAI;
            specialDrone.EnemySetup(2, 1, GeneralAI.Behaviour.random);
        }

        if(QuestController.currentQuest.eventID == 3)
        {
            print("heat damage");
        }

        if (QuestController.currentQuest.eventID == 4)
        {
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            specialTurret = Instantiate(Resources.Load("Turret"), transform.position, Quaternion.identity, currentRoom) as GameObject;
            //set turret to have higher health
        }

        if (QuestController.currentQuest.eventID == 5)
        {
            print("Spawn recharging station");
        }
    }
}
