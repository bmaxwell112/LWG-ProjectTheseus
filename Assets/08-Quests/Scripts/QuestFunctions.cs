using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestFunctions : MonoBehaviour {

    public static QuestFunctions instance = null;
    [SerializeField] GameObject questController;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public List<QuestEvent> questEvents = new List<QuestEvent>();

    // Use this for initialization
    void Start () {
        SetupEvents();
        SpawnController();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SpawnController()
    {
        Instantiate(questController);
    }

    private void SetupEvents()
    {
        questEvents.Add((new QuestEvent(0, "Reactivation", "Turn on the computer in the hub", TriggerID.onRoomEnter, EventType.fetch, 100, false)));
        questEvents.Add((new QuestEvent(1, "Gear Collection 01", "You found a locator tag for a powerful chainsaw part. Collect the part to earn its schematic.", TriggerID.pickup, EventType.fetch, 100, false)));
        questEvents.Add((new QuestEvent(2, "Mad Security Drone", "A fast and powerful security drone was found in a room. Destroying it may yield a part.", TriggerID.onRoomEnter, EventType.kill, 100, false)));
        questEvents.Add((new QuestEvent(3, "Climate Control", "The climate control module in a room is malfunctioning, you will take heat damage in that room until you hack and fix it.", TriggerID.onRoomEnter, EventType.hack, 100, false)));
        questEvents.Add((new QuestEvent(4, "Treasure Defender", "A scan found that a heavily armored turret is protecting loot within itself, destroy it to take it.", TriggerID.onRoomEnter, EventType.kill, 100, false)));
        questEvents.Add((new QuestEvent(5, "Recharge Station", "A gear recharge station is in the middle of the room, prevent it from being destroyed to make use of it!", TriggerID.onRoomEnter, EventType.defend, 100, false)));
    }

    public QuestEvent GetQuestByID(int eventID)
    {
        QuestEvent QE = new QuestEvent();
        for (int i = 0; i < questEvents.Count; i++)
        {
            if (questEvents[i].eventID == eventID)
            {
                QE = questEvents[i];
                break;
            }
        }
        return QE;
    }
}
