using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour {

    private RoomManager roomManager;
    private RoomGeneration roomGen;
    public bool QuestActivated;
    public static QuestEvent currentQuest;

        //check maptype for randomization constraints, use Tileset enum in RoomManager on Awake?

    public static List<QuestEvent> activeEvents = new List<QuestEvent>();
    public static List<spawnFunc> availConfigs = new List<spawnFunc>();

    // Use this for initialization
    void Start () {
        roomManager = FindObjectOfType<RoomManager>();
        roomGen = FindObjectOfType<RoomGeneration>();
        LoadQuestStatus();
        DeactivateAllQuests();
	}
	
	// Update is called once per frame
	void Update ()
    {

    }

    private void LoadQuestStatus()
    {
		for (int i = 0; i < QuestFunctions.instance.questEvents.Count; i++)
        {
            PlayerPrefsManager.GetEventComplete(i);
        }
    }

    private void DeactivateAllQuests()
    {
        for (int i = 0; i < QuestFunctions.instance.questEvents.Count; i++)
        {
            QuestFunctions.instance.questEvents[i].active = false;
        }
    }

	// Making this static as the active events is not static. 
	public static void PullQuest()
    {

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
        }

        //pull random quest that can work for an available configuration
        //Should have something denoting if a quest has already spawned
    }


    public static void ListActiveQuests()
    {
        foreach (QuestEvent qEvent in activeEvents)
        {
            print(qEvent.eventName + " Not complete");
        }
    }

    public void BeginQuest()
    {
        currentQuest.active = true;
        print(currentQuest.eventName + " has started and it's active status is now set to " + currentQuest.active);
    }

    public static void CompleteCurrentQuest()
    {
        currentQuest.completed = true;
        currentQuest.active = false;
        print(currentQuest.eventName + " has been resolved and completion is now set to " + currentQuest.completed);
    }

    //function to spawn items associated with quest in correct room
    //Objects need to have a property for either "activates on setactive" or "activate on interact", set as active quest when this happens
    //Objects should also have win conditions that report to this
   

}