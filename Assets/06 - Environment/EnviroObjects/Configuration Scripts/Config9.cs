using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config9 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0, 0), Quaternion.identity, transform);

        Instantiate(wController.hole, new Vector3(transform.position.x + 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.hole, new Vector3(transform.position.x - 2.18f, transform.position.y - 1.1f, 0), Quaternion.identity, transform);

        Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 0, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x - 2.18f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x - 1.09f, transform.position.y + 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.internalWall, new Vector3(transform.position.x + 0f, transform.position.y + 1.1f, 0), Quaternion.identity, transform);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
