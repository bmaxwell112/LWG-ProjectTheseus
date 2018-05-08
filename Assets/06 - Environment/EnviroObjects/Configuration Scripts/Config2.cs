using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config2 : MonoBehaviour {

    private RoomManager wController;

    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
        Instantiate(wController.enemySpawner, new Vector3(transform.position.x + 3.27f, transform.position.y - 1.65f, 0), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
