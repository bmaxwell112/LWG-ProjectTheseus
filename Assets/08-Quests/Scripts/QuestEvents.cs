using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public enum TriggerID
    {
        pickup = 0,
        onRoomEnter = 1
    }

    public enum EventType
    {
        kill,
        fetch,
        hack,
        defend
    }

    [System.Serializable]
    public class QuestEvent
    {
        public int eventID;
        public string eventName;
        public string eventDesc;
        public TriggerID triggerID;
        public EventType eventType;
        public int eventValue;
        public bool completed;

        public QuestEvent(int id, string name, string desc, TriggerID trigID, EventType eType, int value, bool trueIfCompleted)
        {
            eventID = id;
            eventName = name;
            eventDesc = desc;
            triggerID = trigID;
            eventType = eType;
            eventValue = value;
            completed = trueIfCompleted;
        }

        public QuestEvent()
        {
            eventID = -1;
        }
    }
