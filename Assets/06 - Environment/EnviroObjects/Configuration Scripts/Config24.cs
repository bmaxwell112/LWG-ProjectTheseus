using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Config24 : MonoBehaviour {

    private RoomManager wController;
    // Use this for initialization
    void Start () {
        wController = FindObjectOfType<RoomManager>();

        Instantiate(wController.turretAI, new Vector3(transform.position.x + 6.54f, transform.position.y + 0f, 0), Quaternion.identity, transform);
        Instantiate(wController.turretAI, new Vector3(transform.position.x - 6.54f, transform.position.y + 0f, 0), Quaternion.identity, transform);

        Instantiate(wController.pillar, new Vector3(transform.position.x - 5.45f, transform.position.y + .55f, 0), Quaternion.identity, transform);
        Instantiate(wController.pillar, new Vector3(transform.position.x + 5.45f, transform.position.y - .55f, 0), Quaternion.identity, transform);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
