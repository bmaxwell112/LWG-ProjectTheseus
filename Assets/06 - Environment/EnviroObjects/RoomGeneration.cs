using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {
	static int spawncap;
    //public bool roomActive;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room;
	public bool spawnNextRoom, done;

	// Use this for initialization
	void Start () {
		spawnNextRoom = false;
		doors = GetComponentsInChildren<DoorGen>();   

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
		else if (spawncap >= 1 && spawncap < 100)
		{
			Invoke("SpawnDungeon", 0.5f);
		}
	}
	void SpawnDungeon()
	{
		spawnNextRoom = true;
		spawncap++;
	}

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
	}

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
}
