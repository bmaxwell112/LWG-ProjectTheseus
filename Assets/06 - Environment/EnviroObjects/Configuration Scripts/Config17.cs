using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config17 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0f, 0), Quaternion.identity, transform);

        Instantiate(wController.hole, new Vector3(transform.position.x + 0f, transform.position.y - 2.2f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x - 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

        Instantiate(wController.pillar, new Vector3(transform.position.x + 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x - 4.36f, transform.position.y + 0f, 0), Quaternion.identity, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
