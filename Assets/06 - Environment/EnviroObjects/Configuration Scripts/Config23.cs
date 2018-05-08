using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config23 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 1.09f, transform.position.y - .55f, 0), Quaternion.identity, transform);

        Instantiate(wController.dropSpawner, new Vector3(transform.position.x + 0f, transform.position.y + 0f, 0), Quaternion.identity, transform);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
