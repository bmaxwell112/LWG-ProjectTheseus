using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {
	static int spawncap;
	static bool first;
	public bool roomActive = false;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room, player;
	public bool spawnNextRoom, done;
    private RoomManager worldController;

	// Use this for initialization
	void Start () {
		spawnNextRoom = false;
		ToggleActiveRooms();
		doors = GetComponentsInChildren<DoorGen>();
        worldController = FindObjectOfType<RoomManager>();

        spawnLocation = new Vector3Int[]
        {
            new Vector3Int(0, 8, 0) ,
            new Vector3Int(0, -8, 0) ,
            new Vector3Int(-12, 4, 0) ,
            new Vector3Int(12, 4, 0) ,
            new Vector3Int(-12, -4, 0) ,
            new Vector3Int(12, -4, 0)
        };
		if (spawncap < worldController.roomCap)
		{
			SpawnDungeon();
		}
	}
	void SpawnDungeon()
	{
		spawnNextRoom = true;
		spawncap++;
	}

    //Another SpawnDungeon with forced minimums

	// Update is called once per frame
	void Update () {

        if (spawnNextRoom)
		{
			print("Running update");
			if (!done)
			{
				roomActive = false;
				CheckDoor();
				done = true;
				if (!first)
				{
					roomActive = true;
					first = true;
				}
				spawnNextRoom = false;
			}
		}
        ToggleActiveRooms();
    }

    //something to differentiate rooms that are spawned

    void CheckDoor()
    {
		RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		for (int i=0; i < doors.Length; i++)
        {			
			if (CheckForRoomClearance(spawnLocation[i], rooms))
			{
				print(doors[i].doorWall);
				if (doors[i].doorWall)
				{
					GameObject locRoom = Instantiate(
						room,
						new Vector3(
							transform.position.x + spawnLocation[i].x,
							transform.position.y + spawnLocation[i].y,
							0),
						Quaternion.identity) as GameObject;
				}
			}
        }
    }

	bool CheckForRoomClearance(Vector3 location, RoomGeneration[] rooms)
	{		
		foreach (RoomGeneration room in rooms)
		{
			if (room.transform.position == new Vector3(
						transform.position.x + location.x,
						transform.position.y + location.y,
						0))
			{
				return false;
			}
		}		
		return true;
	}

    void ToggleActiveRooms()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        if (!roomActive)
        {

            foreach (Renderer r in renderers)
            {
                r.enabled = false;
            }
            print("room inactive");

        }
        else
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
            print("room active");
        }

    }
}
