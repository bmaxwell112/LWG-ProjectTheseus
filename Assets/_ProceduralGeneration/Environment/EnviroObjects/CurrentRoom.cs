using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// TODO possibly change later
using Theseus.Character;

public class CurrentRoom : MonoBehaviour {

    private RoomGeneration roomGen;

    private void Start()
    {
        roomGen = GetComponentInParent<RoomGeneration>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if(collision.gameObject.name == "Player" && RoomManager.gameSetupComplete == true)
        {
			StartCoroutine(ActivateRoom(collision.transform));	
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
			RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();
			parentRoom.SetActive(false);            
        }
    }

    //grab this
	IEnumerator ActivateRoom(Transform player)
	{
		RoomManager.gameSetupComplete = false;
		RoomGeneration parentRoom = GetComponentInParent<RoomGeneration>();		
		parentRoom.SetActive(true);
		CameraController cam = FindObjectOfType<CameraController>();
		cam.MoveCamera(transform.position);
		Vector3 playerStartPos = player.transform.position;
		BulletWeapon[] bullets = FindObjectsOfType<BulletWeapon>();
		foreach (BulletWeapon bullet in bullets)
		{
			Destroy(bullet.gameObject);
		}
		float distance = Vector3.Distance(transform.position, player.position);
		float endDistance = Vector3.Distance(transform.position, player.position) - 1.5f;
		while (distance > endDistance)
		{
			player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 2 * Time.deltaTime);
			distance = Vector3.Distance(transform.position, player.position);
			yield return null;
		}
		RoomManager.gameSetupComplete = true;

	}

	void DeactivateRoom()
	{

	}
}
