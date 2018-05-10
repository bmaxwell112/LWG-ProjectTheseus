﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config18 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);

        Instantiate(wController.hole, new Vector3(transform.position.x - 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 3.27f, transform.position.y - .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 5.45f, transform.position.y - .55f, 0), Quaternion.identity, transform);


    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
