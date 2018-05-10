using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config5 : MonoBehaviour {

    // Use this for initialization

    private RoomManager wController;

	void Start () {

        wController = FindObjectOfType<RoomManager>();

        //walls
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);

        //enemies
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + .55f, 0), Quaternion.identity, transform);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
