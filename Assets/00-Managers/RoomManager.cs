using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    public static RoomManager instance = null;
    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
	[SerializeField] bool hub;
	[SerializeField] GameObject room, player, userInterface;
	
    public int roomCap;
	public static bool allActive;
	static Queue<RoomGeneration> roomQueue = new Queue<RoomGeneration>();
    public GameObject[] spawnConfigs;
    //spawnConfigs array

    //Objects to Spawn
    public WallHealth internalWall;
    public EnemySpawner enemySpawner;
    public TurretAI turretAI;
    public Pillar pillar;
    public HoleCollisions hole;
    public DropSpawner dropSpawner;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
		Instantiate(userInterface);
		if (!hub)
		{
			SpawnFirstRoom();
		}
		else
		{
			allActive = true;
		}
	}
	
	// Update is called once per frame

    void SpawnFirstRoom()
    {
		allActive = false;
		RoomGeneration.roomsInExistence = 0;
		RoomGeneration.spawncap = 0;
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
                roomQueue.Clear();
			}
		}
		allActive = true;
	}

	public static void AdditionalRoom(RoomGeneration room)
	{
		roomQueue.Enqueue(room);
	}

    public GameObject GetRandomRoom()
    {
        int layoutNumber = Random.Range(0, spawnConfigs.Length -1);
        return spawnConfigs[layoutNumber];
    }

    //Minimap processes here
    //Track number of rooms for special rooms

}
