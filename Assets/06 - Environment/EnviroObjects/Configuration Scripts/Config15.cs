using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config15 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.dropSpawner, new Vector3(transform.position.x - 3.27f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.turretAI, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
