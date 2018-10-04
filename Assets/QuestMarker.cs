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
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            questDrop1 = Instantiate(Resources.Load("Drops"), transform.position, Quaternion.identity, currentRoom) as GameObject;
            questDrop1.GetComponent<Drops>().databaseItemID = 26;
        }

        if (QuestController.currentQuest.eventID == 2)
        {
            //Need to make a special drone for this later, 100% drop rate
            //set a boolean and an ID to specify if there is a specific drop by ID
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            specialDrone = Instantiate(Resources.Load("Drone"), transform.position, Quaternion.identity, currentRoom) as GeneralAI;
            specialDrone.EnemySetup(2, 1, GeneralAI.Behaviour.random);
        }

        if(QuestController.currentQuest.eventID == 3)
        {
            print("heat damage");
            //need something that does continuous damage based on the current room
            //this will need UI components, red around the sides?
            //Do we implement hacking as planned?
        }

        if (QuestController.currentQuest.eventID == 4)
        {
            Transform currentRoom = RoomManager.GetCurrentActiveRoom().transform;
            specialTurret = Instantiate(Resources.Load("Turret"), transform.position, Quaternion.identity, currentRoom) as GameObject;
            
            //set turret to have higher health, see how to change initialize loadout to give heavy torso? 100% drop
        }

        if (QuestController.currentQuest.eventID == 5)
        {
            print("Spawn recharging station");
            //This recharging station has to have health, also spawn random enemies?
            //Use the same way as the console to recharge all items (once?)
        }
    }
}
