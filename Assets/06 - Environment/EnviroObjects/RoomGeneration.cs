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
	public bool spawnNextRoom, done;
    private RoomManager worldController;
    private int minDoors, totalRooms;
    private DoorGen walls;
    public static bool abyss;

    // Use this for initialization
    void Start () {
        //QueuedStart();
	}

    public void QueuedStart()
    {
        spawnNextRoom = false;
        roomActive = false;
        abyss = false;
        doors = GetComponentsInChildren<DoorGen>();
        worldController = FindObjectOfType<RoomManager>();
        walls = FindObjectOfType<DoorGen>();
        SetMinDoors();
        spawnLocation = new Vector3Int[]
        {
            new Vector3Int(0, 8, 0) ,
            new Vector3Int(0, -8, 0) ,
            new Vector3Int(-12, 4, 0) ,
            new Vector3Int(12, 4, 0) ,
            new Vector3Int(-12, -4, 0) ,
            new Vector3Int(12, -4, 0)
        };

        if (spawncap > worldController.roomCap)
        {
            spawncap = worldController.roomCap;
        }

        if (spawncap <= 0)
        {
            Invoke("SpawnDungeon", 0.5f);
        }
        else if (spawncap >= 1 && spawncap < worldController.roomCap)
        {
            Invoke("SpawnDungeon", 0.5f);
        }

        worldController.ManageRoomQueue();
    }

	void SpawnDungeon()
	{
		spawnNextRoom = true;
        spawncap++;
    }
    
    //sets minimum required doors based on spawncap, NEED to find a way to make that count up min increments, right now because 1-6 doors spawn at a time it skips numbers and checks constantly
    void SetMinDoors()
    {
        if(spawncap <= 0)
        {
            minDoors = 2;
        }
        else if(spawncap >= 1 && spawncap < (worldController.roomCap -6))
        {
            minDoors = 1;
        }
        else if(spawncap >= (worldController.roomCap - 6))
        {
            minDoors = 0;
        }
    }

	// Update is called once per frame
	void Update () {
        //print("minDoors: " + minDoors);
        //print(abyss);

        AbyssTest();


        if (spawnNextRoom)
		{  
			print("Running update");
			if (!done)
			{
				roomActive = false;
				CheckDoor();
				done = true;
			}
			if (!first)
			{
				roomActive = true;
				first = true;
			}
			spawnNextRoom = false;
		}
        ToggleActiveRooms();
    }

    //something to differentiate rooms that are spawned

    void CheckDoor()
    {
		RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		for (int i=0; i < doors.Length; i++)
        {
            if (CheckForRoomClearance(spawnLocation[i], rooms) && doors[i].doorWall)
            {
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
        for(int i = minDoors; i >= doorsOpen; i--)
        {
			if (doorsLeft.Count > 0)
			{
				int Rand = Random.Range(0, doorsLeft.Count - 1);
				print("Random Value: " + Rand);
				print("DoorsLeft: " + doorsLeft[Rand]);
				print("DoorsLeft: " + doorsLeft.Count);

				Walls[doorsLeft[Rand]].doorWall = true;
			}
        }
    }

    //this checks if there's space for a room and if the spawncap (number of rooms) is within 12 of the roomcap and makes abyss "yes" (this changes the door chance over at DoorGen)
    //this makes all external walls red but doesn't work when the number of rooms is higher than 12 because of how spawncap is counting, seems to happen all at once?
    void AbyssTest()
    {
        RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
        for (int i = 0; i < doors.Length; i++)
        {
            if (CheckForRoomClearance(spawnLocation[i], rooms) && spawncap >= (worldController.roomCap -12))
            {
                abyss = true;
                
            }
        }
    }

    //define an array of objects (how am I going to define them, tag?)
    //On start pull a random object from the array and instantiate it
    //Objects will be configurations of spawn locations

}
