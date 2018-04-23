using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {
    public static int spawncap;
	public static bool first;
    public bool roomActive;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room, player;	
    private RoomManager worldController;
    private int minDoors, totalRooms;
    private DoorGen walls;

	// Use this for initialization
	void Start () {
		//QueuedStart();
		roomActive = false;
		doors = GetComponentsInChildren<DoorGen>();
		worldController = FindObjectOfType<RoomManager>();
		walls = FindObjectOfType<DoorGen>();
		spawnLocation = new Vector3Int[]
		{
			new Vector3Int(0, 8, 0) ,
			new Vector3Int(0, -8, 0) ,
			new Vector3Int(-12, 4, 0) ,
			new Vector3Int(12, 4, 0) ,
			new Vector3Int(-12, -4, 0) ,
			new Vector3Int(12, -4, 0)
		};
		RoomManager.AdditionalRoom(this);
	}

    public void QueuedStart()
    {
		print("Spawned Room");
        if (spawncap > worldController.roomCap)
        {
            spawncap = worldController.roomCap;
        }
		roomActive = false;
		if (!first)
		{
			roomActive = true;
			first = true;
		}		
		CheckDoor();
	}
    
    //sets minimum required doors based on spawncap, NEED to find a way to make that count up min increments, right now because 1-6 doors spawn at a time it skips numbers and checks constantly
    void SetMinDoors()
    {
		if (worldController == null)
		{
			worldController = FindObjectOfType<RoomManager>();
		}
        if(spawncap < worldController.roomCap)
        {
			if (spawncap < 2)
			{
				minDoors = 2;
			}
			else if (spawncap >= 2)
			{
				minDoors = 1;
			}
		}
    }

	// Update is called once per frame
	void Update () {
        //print("minDoors: " + minDoors);
        //print(abyss);
        ToggleActiveRooms();
    }

    //something to differentiate rooms that are spawned

    void CheckDoor()
    {
		RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		for (int i=0; i < doors.Length; i++)
        {			
			if (CheckForRoomClearance(spawnLocation[i], rooms) && doors[i].doorWall && spawncap < worldController.roomCap-1)
            {
                {
					spawncap++;
					print("Spawned Room From Gen");
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

        }
        else
        {
            foreach (Renderer r in renderers)
            {
                r.enabled = true;
            }
        }

    }

    public void DoorsLeft()
    {
		SetMinDoors();
		List<int> doorsLeft = new List<int>();
        DoorGen[] Walls = GetComponentsInChildren<DoorGen>();

        for (int i = 0; i < Walls.Length; i++)
        {
            //walls that are not doors and not adjacent to other rooms!!!
            if (!Walls[i].done && !Walls[i].doorWall)
            {
                doorsLeft.Add(i);
			}
        }
		
        int doorsOpen = 0;

        foreach(DoorGen Wall in Walls)
        {
            if(Wall.doorWall)
            {
                doorsOpen++;
            }
        }

		//minDoors set in SetMinDoors crazily enough
		print("door min " + minDoors);
		for (int i = minDoors; i >= doorsOpen; i--)
        {
			if (doorsLeft.Count > 0)
			{
				int Rand = Random.Range(0, doorsLeft.Count - 1);
				//print("Random Value: " + Rand);
				//print("DoorsLeft: " + doorsLeft[Rand]);
				//print("DoorsLeft: " + doorsLeft.Count);
				//
				//Walls[doorsLeft[Rand]].doorWall = true;
			}
        }
    }

	//define an array of objects (how am I going to define them, tag?)
	//On start pull a random object from the array and instantiate it
	//Objects will be configurations of spawn locations

}
