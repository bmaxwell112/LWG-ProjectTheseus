using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewCurrentRoom : MonoBehaviour {

    private RoomGeneration roomGen;
    List<RoomGeneration> adjacentRooms;
    RoomGeneration[] rooms;

    // Use this for initialization
    void Start () {
        roomGen = GetComponentInParent<RoomGeneration>();
        GetAdjRooms();
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && RoomManager.allActive == true)
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

    bool AdjacentRoomExists(Vector3 location, RoomGeneration[] rooms)
    {
        foreach (RoomGeneration room in rooms)
        {
            if (room.transform.position == new Vector3(
                        transform.position.x + location.x,
                        transform.position.y + location.y,
                        0))
            {
                return true;
            }
        }
        return false;
    }

    void GetAdjRooms()
    {
        List<RoomGeneration> adjacentRooms = new List<RoomGeneration>();
        RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();

        foreach (RoomGeneration room in rooms)
        {
             for (int i = 0; i < RoomGeneration.spawnLocation.Length; i++)
            {
                if (AdjacentRoomExists(RoomGeneration.spawnLocation[i], rooms))
                {
                    adjacentRooms.Add(room);
                    
                }
            }
        }

        print(adjacentRooms.Count);

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
        float endDistance = Vector3.Distance(transform.position, player.position) - 1.5f;
        while (distance > endDistance)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, transform.position, 2 * Time.deltaTime);
            distance = Vector3.Distance(transform.position, player.position);
            yield return null;
        }
        parentRoom.ToggleRoomUnlock();
        RoomManager.allActive = true;
    }
}
