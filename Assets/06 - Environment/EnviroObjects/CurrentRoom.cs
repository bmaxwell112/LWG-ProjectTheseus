using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player")
        {
			RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();
			parentRoom.roomActive = true;
			CameraController cam = FindObjectOfType<CameraController>();
			cam.MoveCamera(transform.position);
			print("activating through trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
			RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();
			parentRoom.roomActive = false;            
        }
    }
}
