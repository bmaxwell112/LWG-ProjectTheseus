using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.ProGen {
    public enum TriggerID {
        pickup = 0,
        onRoomEnter = 1
    }

    public enum EventType {
        kill,
        fetch,
        hack,
        defend
    }

    [System.Serializable]
    public class QuestEvent {
        public int eventID;
        public string eventName;
        public string eventDesc;
        public TriggerID triggerID;
        public EventType eventType;
        public int eventValue;
        public bool completed;
        public bool active;

        public QuestEvent (int id, string name, string desc, TriggerID trigID, EventType eType, int value, bool trueIfCompleted, bool trueIfActive) {
            eventID = id;
            eventName = name;
            eventDesc = desc;
            triggerID = trigID;
            eventType = eType;
            eventValue = value;
            completed = trueIfCompleted;
            active = trueIfActive;
        }

        public QuestEvent () {
            eventID = -1;
        }
    }
}