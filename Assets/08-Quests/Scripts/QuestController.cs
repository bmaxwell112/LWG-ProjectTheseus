/* Place this in room manager as it is something managed by the rooms
 * I don't think this should be an static instance as thing here need to reload
 * with each new level load.
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

    private RoomManager roomManager;
    private RoomGeneration roomGen;

	// TODO remove this Pull from allRooms in RoomManager instead
    //public RoomGeneration[] roomList;

	// TODO remove this
	/*
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //check maptype for randomization constraints, use Tileset enum in RoomManager
    } */

	// Made this static so it is accessable outside this instance. 
    public static List<QuestEvent> activeEvents = new List<QuestEvent>();
    public static List<spawnFunc> availConfigs = new List<spawnFunc>();

    // Use this for initialization
    void Start () {
        roomManager = FindObjectOfType<RoomManager>();
        roomGen = FindObjectOfType<RoomGeneration>();
        LoadQuestStatus();
	}
	
	// Update is called once per frame
	void Update ()
    {
        //RoomGeneration[] roomList = FindObjectsOfType<RoomGeneration>();

		/* TODO remvoe this. Moving this to be run on level load in the RoomManager
        if (GameManager.levelLoaded)
        {
            RoomManager newRoomManager = FindObjectOfType<RoomManager>();

            if (!newRoomManager.hub)
            {
                PullQuest();
                GameManager.levelLoaded = false;
            }
        }*/
    }

    private void LoadQuestStatus()
    {
		for (int i = 0; i < QuestFunctions.instance.questEvents.Count; i++)
        {
            PlayerPrefsManager.GetEventComplete(i);
        }
    }

	// Making this static as the active events is not static. 
	public static void PullQuest()
    {
		// TEST DATA
		PlayerPrefsManager.SetEventComplete(1, 1);
		PlayerPrefsManager.SetEventComplete(2, 1);
		print(QuestFunctions.instance.GetQuestByID(1).eventName + " And " + QuestFunctions.instance.GetQuestByID(2).eventName + " Complete");
		// =========
		for (int i = 0; i < QuestFunctions.instance.questEvents.Count; i++)
        {
            if (PlayerPrefsManager.ReturnEventComplete(i) == 0)
            {
				activeEvents.Add(QuestFunctions.instance.GetQuestByID(i));
            }
        }

        //pull a list of available room configurations v

		for (int i = 0; i < RoomManager.instance.allRooms.Count; i++)
        {
			availConfigs.Add(RoomManager.instance.allRooms[i].GetComponentInChildren<spawnFunc>());
            //print("Added available spawn Configurations to list");
        }

        //pull random quest that can work for an available configuration
        //Should have something denoting if a quest has already spawned
    }

    //function to spawn items associated with quest in correct room
    //Objects need to have a property for either "activates on setactive" or "activate on interact", set as active quest when this happens
    //Objects should also have win conditions that report to this
    

    //Set completed quest as "complete"

}
