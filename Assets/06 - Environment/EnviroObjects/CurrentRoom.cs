using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Player" && RoomManager.allActive == true)
        {
			StartCoroutine(ActivateRoom(collision.transform));	
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

	IEnumerator ActivateRoom(Transform player)
	{
		RoomManager.allActive = false;
		RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();		
		parentRoom.roomActive = true;
		CameraController cam = FindObjectOfType<CameraController>();
		cam.MoveCamera(transform.position);
		Vector3 playerStartPos = player.transform.position;
		BulletWeapon[] bullets = FindObjectsOfType<BulletWeapon>();
		foreach (BulletWeapon bullet in bullets)
		{
			Destroy(bullet.gameObject);
		}
		float distance = Vector3.Distance(transform.position, player.position);
		float endDistance = Vector3.Distance(transform.position, player.position) - 1.25f;
		while (distance > endDistance)
		{
			player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 2 * Time.deltaTime);
			distance = Vector3.Distance(transform.position, player.position);
			yield return null;
		}
		RoomManager.allActive = true;
	}

	void DeactivateRoom()
	{

	}
}
