using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO possibly change later
using Theseus.Character;

namespace Theseus.ProGen {
	public class RoomGeneration : MonoBehaviour {
		public static int spawncap, roomsInExistence;
		public static bool first;
		public bool roomActive;
		public DoorGen[] doors;
		public static Vector3Int[] spawnLocation;
		[SerializeField] GameObject room, layout;
		[SerializeField] ClosedDoor[] closedDoors;
		[SerializeField] OpenDoor[] openDoors;
		private RoomManager worldController;
		private AllConfig configScript;
		private int minDoors, totalRooms;
		private DoorGen walls;
		public bool roomListener, enemyListener;
		[SerializeField] bool manualUnlock;

		public bool adjCheckedTrue;

		// Use this for initialization
		void Start () {

			//QueuedStart();
			roomsInExistence++;
			roomActive = true;
			roomListener = !roomActive;
			//enemyListener = false;
			doors = GetComponentsInChildren<DoorGen> ();
			worldController = FindObjectOfType<RoomManager> ();
			walls = FindObjectOfType<DoorGen> ();
			spawnLocation = new Vector3Int[] {
				new Vector3Int (0, 8, 0),
					new Vector3Int (0, -8, 0),
					new Vector3Int (-12, 4, 0),
					new Vector3Int (12, 4, 0),
					new Vector3Int (-12, -4, 0),
					new Vector3Int (12, -4, 0)
			};
			RoomManager.instance.AdditionalRoom (this);
			GetSpawnConfigs ();
			DisableDoors ();
		}

		private void DisableDoors () {
			foreach (ClosedDoor doors in closedDoors) {
				doors.gameObject.SetActive (false);
			}
		}

		public void QueuedStart () {
			if (spawncap > worldController.roomCap) {
				spawncap = worldController.roomCap;
			}
			//roomActive = false;
			if (first) {
				//roomActive = true;
				first = false;
			}
			CheckDoor ();
		}

		//sets minimum required doors based on spawncap, NEED to find a way to make that count up min increments, right now because 1-6 doors spawn at a time it skips numbers and checks constantly
		void SetMinDoors () {
			if (worldController == null) {
				worldController = FindObjectOfType<RoomManager> ();
			}
			if (spawncap < worldController.roomCap) {
				if (spawncap < 2) {
					minDoors = 2;
				} else if (spawncap >= 2) {
					minDoors = 1;
				}
			}
		}

		// Update is called once per frame
		void Update () {
			TrackEnemies ();
		}

		public void TrackEnemies () {
			if (roomActive) {
				BasicEnemy[] livingEnemies = GetComponentsInChildren<BasicEnemy> ();
				RangeShortEnemy[] livingRanged = GetComponentsInChildren<RangeShortEnemy> ();

				if (livingEnemies.Length > 0 || livingRanged.Length > 0) {
					enemyListener = true;
				} else {
					enemyListener = false;
				}
			}

		}

		public void CheckEnemies () {
			if (enemyListener == false) {
				RoomUnlock ();
			} else {
				RoomLock ();
			}
		}

		public void RoomLock () {
			foreach (ClosedDoor doors in closedDoors) {
				doors.gameObject.SetActive (true);
			}

			foreach (OpenDoor doors in openDoors) {
				doors.gameObject.SetActive (false);
			}
		}

		public void RoomUnlock () {
			foreach (ClosedDoor doors in closedDoors) {
				doors.gameObject.SetActive (false);
			}

			foreach (OpenDoor doors in openDoors) {
				doors.gameObject.SetActive (true);
			}
		}

		void CheckDoor () {
			//RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();		
			for (int i = 0; i < doors.Length; i++) {
				if (CheckForRoomClearance (spawnLocation[i]) && doors[i].doorWall && spawncap < worldController.roomCap - 1) {
					{
						spawncap++;
						Instantiate (
							Resources.Load ("room", typeof (GameObject)),
							new Vector3 (
								transform.position.x + spawnLocation[i].x,
								transform.position.y + spawnLocation[i].y,
								0),
							Quaternion.identity);
					}
				}
			}
		}

		public bool CheckForRoomClearance (Vector3 location) {
			foreach (RoomGeneration room in RoomManager.instance.allRooms) {
				if (room.transform.position == new Vector3 (
						transform.position.x + location.x,
						transform.position.y + location.y,
						0)) {
					return false;
				}
			}
			return true;
		}

		public void ToggleActiveRooms () {
			// GameObject[] enemySpawns = GameObject.FindGameObjectsWithTag("SpawnConfig");
			if (!roomActive) {
				layout.SetActive (false);
				Renderer[] renderers = GetComponentsInChildren<Renderer> ();
				foreach (Renderer r in renderers) {
					r.enabled = false;
				}
			} else {
				Renderer[] renderers = GetComponentsInChildren<Renderer> ();
				foreach (Renderer r in renderers) {
					r.enabled = true;
				}
				// TODO remove this		
				GetComponent<CustomNavMesh> ().CheckAllDirections ();
				layout.SetActive (true);
			}
		}

		public void DoorsLeft () {
			SetMinDoors ();
			List<int> doorsLeft = new List<int> ();
			DoorGen[] Walls = GetComponentsInChildren<DoorGen> ();

			for (int i = 0; i < Walls.Length; i++) {
				//walls that are not doors and not adjacent to other rooms!!!
				if (!Walls[i].done && !Walls[i].doorWall) {
					doorsLeft.Add (i);
				}
			}

			int doorsOpen = 0;

			foreach (DoorGen Wall in Walls) {
				if (Wall.doorWall) {
					doorsOpen++;
				}
			}

			//minDoors set in SetMinDoors crazily enough
			for (int i = minDoors; i >= doorsOpen; i--) {
				if (doorsLeft.Count > 0) {
					int Rand = Random.Range (0, doorsLeft.Count - 1);
					//print("Random Value: " + Rand);
					//print("DoorsLeft: " + doorsLeft[Rand]);
					//print("DoorsLeft: " + doorsLeft.Count);
					//
					Walls[doorsLeft[Rand]].doorWall = true;
				}
			}
		}

		public void GetSpawnConfigs () {
			configScript = FindObjectOfType<AllConfig> ();

			if (first) {
				layout = Instantiate (RoomManager.instance.configContainer, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity, transform);
				layout.GetComponent<AllConfig> ().SetConfigurationNumber (0);
			} else if (roomsInExistence == RoomManager.instance.roomCap) {
				layout = Instantiate (RoomManager.instance.configContainer, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity, transform);
				layout.GetComponent<AllConfig> ().SetConfigurationNumber (25);
				//THIS IS LISTED AS 3 IN THE OLD SYSTEM
			} else {
				layout = Instantiate (RoomManager.instance.configContainer, new Vector3 (transform.position.x, transform.position.y, 0), Quaternion.identity, transform);
				layout.GetComponent<AllConfig> ().SetConfigurationNumber (Random.Range (0, 24));

				layout.SetActive (false);
			}
		}

		// Simple external functions
		public void SetActive (bool active) {
			roomActive = active;
		}
		public bool GetActive () {
			return roomActive;
		}

		public bool[] ReturnOpenDoors () {
			bool[] doorsOpen = new bool[6];
			for (int i = 0; i < doors.Length; i++) {
				if (doors[i].doorWall) {
					doorsOpen[i] = true;
				} else {
					doorsOpen[i] = false;
				}
			}
			return doorsOpen;
		}
	}
}