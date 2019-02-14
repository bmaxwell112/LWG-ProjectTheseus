using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Theseus.Core {
    public class PrimaryAbility : MonoBehaviour {

        private TechTree ui;
        [SerializeField] Text abtext;

        // Use this for initialization
        void Start () {
            ui = FindObjectOfType<TechTree> ();
        }

        // Update is called once per frame
        void Update () {
            CheckSet ();
        }

        void CheckSet () {
            // Block, Hacking, Dodge Roll
            if (ui.abilitySet == 0) {
                abtext.text = "Locked";
            } else if (ui.abilitySet == 1) {
                abtext.text = "Locked";
            } else if (ui.abilitySet == 2) {
                abtext.text = "Locked";
            }
        }
    }
}