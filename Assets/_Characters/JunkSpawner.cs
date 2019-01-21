using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
    public class JunkSpawner : MonoBehaviour {

        private bool junkExists;
        [SerializeField] GameObject Junk;

        // Use this for initialization
        void Start () {
            junkExists = false;
        }

        // Update is called once per frame
        void Update () {

            CheckForJunk ();

            if (!junkExists && FindObjectOfType<BossBehavior> ().beginSequence) {
                WaitAndMake ();
                MakeJunk ();
            }
        }

        void CheckForJunk () {
            if (GetComponentInChildren<DisableGravity> () != null) {
                junkExists = true;
            } else {
                junkExists = false;
            }
        }

        IEnumerator WaitAndMake () {
            int randWait = Random.Range (10, 15);
            yield return new WaitForSeconds (randWait);
        }

        void MakeJunk () {
            Instantiate (Junk, new Vector3 (transform.position.x, transform.position.y, -2), Quaternion.identity, gameObject.transform);
            junkExists = true;
        }
    }
}