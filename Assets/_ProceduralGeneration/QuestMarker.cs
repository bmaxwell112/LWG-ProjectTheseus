using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO possibly change later
using Theseus.Character;

namespace Theseus.ProGen {
    public class QuestMarker : MonoBehaviour {
        RoomGeneration containingRoom;
        QuestController qController;
        QuestEvent qMarked;
        GameObject questDrop1, specialTurret, cStation;
        GeneralAI specialDrone;
        Sprite questDrop1spr;
        private bool firstTimeSetup, questReady, dropGet, setupDone, droneUp;

        // Use this for initialization
        void Start () {
            firstTimeSetup = false;
            questReady = false;
            qController = FindObjectOfType<QuestController> ();
            containingRoom = GetComponentInParent<RoomGeneration> ();
            setupDone = false;
            droneUp = false;

            qMarked = QuestController.activeEvents[Random.Range (1, QuestController.activeEvents.Count)];
        }

        // Update is called once per frame
        void Update () {
            CheckForActivation ();
            CheckForCompletion ();
        }

        void CheckForActivation () {
            if (!containingRoom.GetComponent<MeshRenderer> ().enabled && !firstTimeSetup && RoomManager.gameSetupComplete) {
                firstTimeSetup = true;
            }

            if (containingRoom.GetComponent<MeshRenderer> ().enabled && firstTimeSetup && RoomManager.gameSetupComplete && !questReady) {
                questReady = true;
                QuestController.currentQuest = qMarked;
                RunSetups ();
                qController.BeginQuest ();
            }
        }

        void CheckForCompletion () {
            if (setupDone) {

                if (QuestController.currentQuest.eventID == 1 && questReady && questDrop1.GetComponentInChildren<SpriteRenderer> ().sprite != questDrop1spr) {
                    QuestController.CompleteCurrentQuest ();
                }
                if (QuestController.currentQuest.eventID == 2 && questReady && droneUp && specialDrone == null) {
                    QuestController.CompleteCurrentQuest ();
                    droneUp = false;
                }
                if (QuestController.currentQuest.eventID == 4 && questReady && specialTurret == null) {
                    QuestController.CompleteCurrentQuest ();
                }
            }
        }

        void RunSetups () {
            if (questReady) {
                SetupAll ();
            }
        }

        void SetupAll () {
            if (QuestController.currentQuest.eventID == 1) {
                Transform currentRoom = RoomManager.GetCurrentActiveRoom ().transform;
                questDrop1 = Instantiate (Resources.Load ("Drops"), transform.position, Quaternion.identity, currentRoom) as GameObject;
                questDrop1.GetComponent<Drops> ().databaseItemID = 26;
                questDrop1.GetComponentInChildren<SpriteRenderer> ().sprite = questDrop1spr;
            }

            if (QuestController.currentQuest.eventID == 2) {
                //Need to make a special drone for this later, 100% drop rate
                //set a boolean and an ID to specify if there is a specific drop by ID
                Transform currentRoom = RoomManager.GetCurrentActiveRoom ().transform;
                specialDrone = Instantiate (Resources.Load ("Drone"), transform.position, Quaternion.identity, currentRoom) as GeneralAI;
                if (specialDrone != null) {
                    droneUp = true;
                    specialDrone.EnemySetup (2, 1, GeneralAI.Behaviour.random);
                }
            }

            if (QuestController.currentQuest.eventID == 3) {
                print ("heat damage, this quest does not function yet");
                //need something that does continuous damage based on the current room
                //this will need UI components, red around the sides?
                //Do we implement hacking as planned?
            }

            if (QuestController.currentQuest.eventID == 4) {
                Transform currentRoom = RoomManager.GetCurrentActiveRoom ().transform;
                specialTurret = Instantiate (Resources.Load ("Turret"), transform.position, Quaternion.identity, currentRoom) as GameObject;
                specialTurret.GetComponent<RobotLoadout> ().loadout[1].itemHitpoints = 75;
                specialTurret.GetComponentInChildren<SpriteRenderer> ().color = Color.red;

                //100% drop, deactivate color and give a special sprite later?
            }

            if (QuestController.currentQuest.eventID == 5) {
                Transform currentRoom = RoomManager.GetCurrentActiveRoom ().transform;
                cStation = Instantiate (Resources.Load ("ChargingStation"), transform.position, Quaternion.identity, currentRoom) as GameObject;
                print ("Spawn recharging station");
                Instantiate (Resources.Load ("Drone"), transform.position + new Vector3 (10, 5, 0), Quaternion.identity, currentRoom);
                //Give station health
            }
            setupDone = true;
        }
    }
}