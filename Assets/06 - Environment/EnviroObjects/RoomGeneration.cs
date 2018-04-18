using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {
	static int spawncap;
   public bool roomActive;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room, player;
	public bool spawnNextRoom, done;
    private RoomManager worldController;
    private CurrentRoom currentRoom;

	// Use this for initialization
	void Start () {
		spawnNextRoom = false;
        roomActive = false;
		doors = GetComponentsInChildren<DoorGen>();
        worldController = FindObjectOfType<RoomManager>();
        currentRoom = GetComponentInChildren<CurrentRoom>();

        spawnLocation = new Vector3Int[]
        {
            new Vector3Int(0, 8, 0) ,
            new Vector3Int(0, -8, 0) ,
            new Vector3Int(-12, 4, 0) ,
            new Vector3Int(12, 4, 0) ,
            new Vector3Int(-12, -4, 0) ,
            new Vector3Int(12, -4, 0)
        };
		if (spawncap <= 0)
		{
			Invoke("SpawnDungeon", 5f);
		}
		else if (spawncap >= 1 && spawncap < worldController.roomCap)
		{
			Invoke("SpawnDungeon", 0.5f);
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
				CheckDoor();
				done = true;
			}
		}

      if(currentRoom.currentActive)
        {
            roomActive = true;
        }
      else
        {
            roomActive = false;
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
					Instantiate(
						room,
						new Vector3(
							transform.position.x + spawnLocation[i].x,
							transform.position.y + spawnLocation[i].y,
							0),
						Quaternion.identity);
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
