using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
    [SerializeField] GameObject room, player;
    public int roomCap;
    private RoomGeneration nextRoom;
    //spawnConfigs array


    // Use this for initialization
    void Start () {
        nextRoom = FindObjectOfType<RoomGeneration>();
        SpawnFirstRoom();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnFirstRoom()
    {
        Instantiate(room, Vector3.zero, Quaternion.identity);
        ManageRoomQueue();
    }

    public void ManageRoomQueue()
    {
        Queue<RoomGeneration> roomQueue = new Queue<RoomGeneration>();
        roomQueue.Enqueue(nextRoom);
        RoomGeneration thisRoom = roomQueue.Peek();
        roomQueue.Dequeue();
        thisRoom.QueuedStart();
    }

    //Minimap processes here
    //Track number of rooms for special rooms

}
