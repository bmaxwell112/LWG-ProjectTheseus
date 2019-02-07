using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Theseus.Core;
using Theseus.ProGen;

namespace Theseus.Character {
    public class RechargingStation : MonoBehaviour {

        BoxCollider2D bc2D;
        bool touchingMe;
        PlayerController player;

        QuestController qController;

        // Use this for initialization
        void Start () {
            bc2D = GetComponent<BoxCollider2D> ();
            player = FindObjectOfType<PlayerController> ();
            qController = FindObjectOfType<QuestController>();

            //TAKE INPUT CAPTURE FROM TERMINAL
        }

        // Update is called once per frame
        void Update () {

        }

        void OnCollisionStay2D (Collision2D collision) {
            if (InputCapture.pickup && !GameManager.paused && collision.gameObject.CompareTag ("Player") && !touchingMe) {
                for (int i = 0; i < player.GetComponent<RobotLoadout> ().loadout.Length; i++) {
                    player.GetComponent<RobotLoadout> ().power[i] = player.GetComponent<RobotLoadout> ().loadout[i].itemPower;

                }
                print ("Equipment power restored");
                qController.CompleteCurrentQuest ();
                touchingMe = true;
            }
            if (GameManager.paused && touchingMe) {
                touchingMe = false;
            }
        }
    }
}