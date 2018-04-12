using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {

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
    }
	
	// Update is called once per frame
	void Update () {
		print(spawnNextRoom);
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
		print("Ranthis " + doors.Length);
        for(int i=0; i < doors.Length; i++)
        {
			print(doors[i].doorWall);
            if(doors[i].doorWall)
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
