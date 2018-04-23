using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
    [SerializeField] GameObject room, player;
    public int roomCap;
    private RoomGeneration nextRoom;
	static Queue<RoomGeneration> roomQueue = new Queue<RoomGeneration>();
	//spawnConfigs array


	// Use this for initialization
	void Start () {
        nextRoom = FindObjectOfType<RoomGeneration>();
        SpawnFirstRoom();
	}
	
	// Update is called once per frame

    void SpawnFirstRoom()
    {
        Instantiate(room, Vector3.zero, Quaternion.identity);
        StartCoroutine(ManageRoomQueue());
    }

    IEnumerator ManageRoomQueue()
    {
		yield return new WaitForFixedUpdate();
		while (RoomGeneration.spawncap < roomCap-1)
		{
			if (roomQueue.Count > 0)
			{
				RoomGeneration thisRoom = roomQueue.Peek();
				thisRoom.QueuedStart();
				roomQueue.Dequeue();				
			}			
			yield return null;
		}
		print("Capped at: " + RoomGeneration.spawncap);
		RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		foreach(RoomGeneration room in rooms)
		{
			DoorGen[] doors = room.GetComponentsInChildren<DoorGen>();
			foreach (DoorGen door in doors)
			{
				door.EndSpawningCheck();
				print("doors to the void closed");
			}
		}
	}

	public static void AdditionalRoom(RoomGeneration room)
	{
		roomQueue.Enqueue(room);
	}

    //Minimap processes here
    //Track number of rooms for special rooms

}
