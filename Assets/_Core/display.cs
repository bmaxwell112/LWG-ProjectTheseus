using System.Collections;
using System.Collections.Generic;
using Theseus.ProGen;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {

    public class display : MonoBehaviour {

        private QuestFunctions questGiver;
        private Text dispText;

        // Use this for initialization
        void Start () {
            questGiver = FindObjectOfType<QuestFunctions> ();
            dispText = GetComponent<Text> ();
            dispText.text = questGiver.questEvents[1].eventName;

            //maybe change this to have the questcontroller pull an item from a list and make that the current quest for when this instatiates
        }

        // Update is called once per frame
        void Update () {

        }

        //When something is set to active for the first time, instantiate button in ScrollView
    }
}