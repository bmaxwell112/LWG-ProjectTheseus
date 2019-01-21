using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
// TODO possibly change later
using Theseus.Character;

public class NewCurrentRoom : MonoBehaviour {

    private RoomGeneration roomGen;
    // public List<RoomGeneration> adjacentRooms;
    // public RoomGeneration[] rooms;	
    [SerializeField] Vector3 adjOffset;
    private DoorGen parentWall;
    Vector3 nextRoomPos, nextRoomAdjust;
    private BoxCollider2D thisColl;

    // Use this for initialization
    void Start () {
        roomGen = GetComponentInParent<RoomGeneration>();
        parentWall = GetComponentInParent<DoorGen>();

		nextRoomPos = roomGen.transform.position + adjOffset;
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (collision.gameObject.name == "Player" && RoomManager.gameSetupComplete == true && !collision.isTrigger)
        {
			StartCoroutine(ActivateRoom(collision.transform));			
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
		if (collision.gameObject.name == "Player" && RoomManager.gameSetupComplete == true)
        {
			roomGen.SetActive(false);
        }
    }

	RoomGeneration FindNearbyRoom()
	{
		//RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
		RoomGeneration nearbyRoom = roomGen;
		foreach (RoomGeneration room in RoomManager.instance.allRooms)
		{
			for (int i = 0; i < RoomGeneration.spawnLocation.Length; i++)
			{				
				if (room.transform.position == nextRoomPos)
				{
					nearbyRoom = room;
				}
			}
		}
		return nearbyRoom;
	}

   void GetAdjustment()
    {
        if(parentWall.name == "WallTop")
        {
            nextRoomAdjust = new Vector3(0, -1, 0);
        }
        if (parentWall.name == "WallBottom")
        {
            nextRoomAdjust = new Vector3(0, 2, 0);
        }
        if (parentWall.name == "WallTL")
        {
            nextRoomAdjust = new Vector3(0, 2, 0);
        }
        if (parentWall.name == "WallTR")
        {
            nextRoomAdjust = new Vector3(0, 2, 0);
        }
        if (parentWall.name == "WallBL")
        {
            nextRoomAdjust = new Vector3(0, -2, 0);
        }
        if (parentWall.name == "WallBR")
        {
            nextRoomAdjust = new Vector3(0, -2, 0);
        }
    }

    IEnumerator ActivateRoom(Transform player)
    {
		GetAdjustment();

		RoomManager.gameSetupComplete = false;
		roomGen.SetActive(false);
		roomGen.ToggleActiveRooms();
		RoomGeneration nextRoom = FindNearbyRoom();
		nextRoom.SetActive(true);		
		CameraController cam = FindObjectOfType<CameraController>();
        cam.MoveCamera(nextRoomPos);
        Vector3 playerStartPos = player.transform.position;
        BulletWeapon[] bullets = FindObjectsOfType<BulletWeapon>();
		nextRoom.ToggleActiveRooms();
		foreach (BulletWeapon bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
		float endDistance = Vector3.Distance(nextRoomPos, player.position) - 2f;
		float distance = Vector3.Distance(nextRoomPos, player.position); 
		while (distance > endDistance)
        {
			player.transform.position = Vector3.MoveTowards(player.transform.position, (nextRoomPos + nextRoomAdjust), 2 * Time.deltaTime);
            distance = Vector3.Distance(nextRoomPos, player.position);
			yield return null;
        }
		nextRoom.CheckEnemies();
		FindObjectOfType<MinimapContoller>().UpdateActiveMiniRoom();
		RoomManager.gameSetupComplete = true;
	}
}
