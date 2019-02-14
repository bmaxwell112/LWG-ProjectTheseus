using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Core {
    public class RogueBtn : MonoBehaviour {

        private TechTree ui;

        // Use this for initialization
        void Start () {
            ui = FindObjectOfType<TechTree> ();
        }

        // Update is called once per frame
        void Update () {

        }

        public void RogueTree () {
            ui.abilitySet = 2;
        }
    }
}