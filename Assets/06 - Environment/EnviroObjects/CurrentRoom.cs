using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour {

    private RoomGeneration parentRoom;

	// Use this for initialization
	void Start () {
        parentRoom = GetComponentInParent<RoomGeneration>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            parentRoom.roomActive = true;
			CameraController cam = FindObjectOfType<CameraController>();
			cam.MoveCamera(transform.position);
			print("activating through trigger");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
            parentRoom.roomActive = true;
            print("activating through trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            parentRoom.roomActive = false;            
        }
    }
}
