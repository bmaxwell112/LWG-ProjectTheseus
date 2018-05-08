using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config14 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 5.45f, transform.position.y + 0, 0), Quaternion.identity, transform);
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x - 5.45f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.pillar, new Vector3(transform.position.x + 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x - 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x + 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x - 1.09f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
