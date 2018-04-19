using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {
    public static int spawncap;
    public bool roomActive;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room, player;
	public bool spawnNextRoom, done;
    private RoomManager worldController;

	// Use this for initialization
	void Start () {
		spawnNextRoom = false;
        roomActive = true;
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
		if (spawncap <= 0)
		{
			Invoke("SpawnDungeon", 0.5f);
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
		for (int i=0; i < doorsLeft.Count; i++)
		{
			print(i + " = " + doorsLeft[i] + " = " + Walls[doorsLeft[i]].doorLocation);
		}

        int doorsOpen = 0;

        foreach(DoorGen Wall in Walls)
        {
            if(Wall.doorWall)
            {
                doorsOpen++;
            }
        }

        for(int i = 2; i >= doorsOpen; i--)
        {
            int Rand = Random.Range(0, doorsLeft.Count - 1);
            Walls[doorsLeft[Rand]].doorWall = true;
            print("Opening " + Rand + " = " + Walls[doorsLeft[Rand]].doorLocation);
        }



    }

}
