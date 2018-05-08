using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config4 : MonoBehaviour {

    private RoomManager wController;

    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.enemySpawner, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity, transform);
    }

    // Update is called once per frame
    void Update () {
		
	}
}
