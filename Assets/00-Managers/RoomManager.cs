using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour {

    enum TileSet {Fabrication, Terraforming, Disposal, Purification, Security, Medical};
    [SerializeField] GameObject room, player;
    public int roomCap;


    // Use this for initialization
    void Start () {
        SpawnFirstRoom();
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void SpawnFirstRoom()
    {
        Instantiate(room, Vector3.zero, Quaternion.identity);
    }

    //Minimap processes here
    //Track number of rooms for special rooms

}
