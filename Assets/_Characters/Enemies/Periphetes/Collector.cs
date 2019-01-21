using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Theseus.Character {
    public class Collector : MonoBehaviour {

        [SerializeField] int junkConversion;
        public enum Side { Top, Bottom, Left, Right };
        [SerializeField] Side collectorSide = Side.Top;
        private Vector3 droneSpawn;

        // Use this for initialization
        void Start () {
            junkConversion = 0;
            SetDroneSpawnLoc ();
        }

        private void OnTriggerStay2D (Collider2D collision) {
            if (collision.GetComponent<DisableGravity> () != null) {
                Destroy (collision.gameObject);
                junkConversion += 1;
                CheckJunk ();
            }
        }

        void CheckJunk () {
            if (junkConversion >= 5) {
                Instantiate (Resources.Load ("DroneBossSpawn"), droneSpawn, Quaternion.identity, FindObjectOfType<RoomEditor> ().transform);
                junkConversion = 0;
            } else {
                print ("Current junk level is " + junkConversion);
            }
        }

        void SetDroneSpawnLoc () {
            switch (collectorSide) {
                case Side.Top:
                    droneSpawn = new Vector3 (transform.position.x, transform.position.y + 2f, 0);
                    break;

                case Side.Bottom:
                    droneSpawn = new Vector3 (transform.position.x, transform.position.y - 2f, 0);
                    break;

                case Side.Left:
                    droneSpawn = new Vector3 (transform.position.x - 2f, transform.position.y, 0);
                    break;

                case Side.Right:
                    droneSpawn = new Vector3 (transform.position.x + 2f, transform.position.y, 0);
                    break;

                default:
                    Debug.Log ("Periphetes doesn't know where to spawn a Drone, check enum!");
                    break;
            }
        }
    }
}