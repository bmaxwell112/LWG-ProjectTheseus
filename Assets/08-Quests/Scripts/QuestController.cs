using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

    private RoomManager roomManager;
    private QuestFunctions qFunctions;
    private RoomGeneration roomGen;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //check maptype for randomization constraints, use Tileset enum in RoomManager
    }

    static public List<QuestEvent> activeEvents = new List<QuestEvent>();
    static public List<spawnFunc> availConfigs = new List<spawnFunc>();
    static public List<RoomGeneration> roomList = new List<RoomGeneration>();

    // Use this for initialization
    void Start () {
        roomManager = FindObjectOfType<RoomManager>();
        qFunctions = FindObjectOfType<QuestFunctions>();
        roomGen = FindObjectOfType<RoomGeneration>();
        LoadQuestStatus();
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (GameManager.levelLoaded)
        {
            RoomManager newRoomManager = FindObjectOfType<RoomManager>();

            if (!newRoomManager.hub)
            {
                PullQuest();
                print(roomList.Count);
                GameManager.levelLoaded = false;
            }
        }

    }

    private void LoadQuestStatus()
    {
        for (int i = 0; i < qFunctions.questEvents.Count; i++)
        {
            PlayerPrefsManager.GetEventComplete(i);
            print("Events Loaded");
        }
    }

    public void PullQuest()
    {
        for (int i = 0; i < qFunctions.questEvents.Count; i++)
        {
            if (PlayerPrefsManager.ReturnEventComplete(i) == 0)
            {
                activeEvents.Add(qFunctions.GetQuestByID(i));
                print("Event added to active pool");
            }
        }

        //pull random quest that can work for an available configuration
    }



    //Should have something denoting if a quest has already spawned

    //function to spawn items associated with quest in correct room
    //Objects need to have a property for either "activates on setactive" or "activate on interact", set as active quest when this happens
    //Objects should also have win conditions that report to this


    //Set completed quest as "complete"

}
