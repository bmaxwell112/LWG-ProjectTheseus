using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public static RoomManager instance = null;
    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
    [SerializeField] GameObject room, player;
    public int roomCap;
	public static bool SpawningComplete;
    private RoomGeneration nextRoom;
	static Queue<RoomGeneration> roomQueue = new Queue<RoomGeneration>();
    private int layoutNumber;
    public GameObject[] spawnConfigs;
    //spawnConfigs array


    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        nextRoom = FindObjectOfType<RoomGeneration>();
        SpawnFirstRoom();
	}
	
	// Update is called once per frame

    void SpawnFirstRoom()
    {
        RoomGeneration.first = true;
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
		RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		foreach(RoomGeneration room in rooms)
		{
			DoorGen[] doors = room.GetComponentsInChildren<DoorGen>();
			foreach (DoorGen door in doors)
			{
				door.EndSpawningCheck();
				print("doors to the void closed");
                roomQueue.Clear();
			}
		}
		SpawningComplete = true;
	}

	public static void AdditionalRoom(RoomGeneration room)
	{
		roomQueue.Enqueue(room);
	}

    public GameObject GetRandomRoom()
    {
        layoutNumber = Random.Range(0, spawnConfigs.Length);
        return spawnConfigs[layoutNumber];
    }

    //Minimap processes here
    //Track number of rooms for special rooms

}
