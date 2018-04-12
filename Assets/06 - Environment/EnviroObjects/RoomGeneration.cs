using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomGeneration : MonoBehaviour {

    //public bool roomActive;
    public DoorGen[] doors;
    private Vector3Int[] spawnLocation;
    [SerializeField] GameObject room;

	// Use this for initialization
	void Start () {
        doors = GetComponentsInChildren<DoorGen>();   

        spawnLocation = new Vector3Int[]
        {
            new Vector3Int(0, 40, 0) ,
            new Vector3Int(0, -40, 0) ,
            new Vector3Int(-120, 40, 0) ,
            new Vector3Int(120, 40, 0) ,
            new Vector3Int(-120, -40, 0) ,
            new Vector3Int(120, -40, 0)
        };
        CheckDoor();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void CheckDoor()
    {
        for(int i=0; i < doors.Length; i++)
        {
            if(doors[i].doorWall)
            {
                Instantiate(room, spawnLocation[i], Quaternion.identity);
            }
        }
    }
}
