using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NewCurrentRoom : MonoBehaviour {

    private RoomGeneration roomGen;
    public List<RoomGeneration> adjacentRooms;
    public RoomGeneration[] rooms;
    [SerializeField] Vector3 adjOffset;
    private DoorGen parentWall;
    public RoomGeneration nextRoom;

    // Use this for initialization
    void Start () {
        roomGen = GetComponentInParent<RoomGeneration>();
        parentWall = GetComponentInParent<DoorGen>();

        StartCoroutine(WarmUpTime(0.1f));
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SetOffset();
        print(nextRoom.transform.position);

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

    public void GetAdjRooms()
    {
        List<RoomGeneration> adjacentRooms = new List<RoomGeneration>();
        RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();

        foreach (RoomGeneration room in rooms)
        {
             for (int i = 0; i < RoomGeneration.spawnLocation.Length; i++)
            {
                if (!roomGen.CheckForRoomClearance(RoomGeneration.spawnLocation[i], rooms))
                {
                    adjacentRooms.Add(room);
                }
            }
        }
        print(adjacentRooms.Count);
    }

    public RoomGeneration SetOffset()
    {
        if(parentWall.name == "WallTop")
        {
            for (int i = 0; i < adjacentRooms.Count; i++)
            {
                if(adjacentRooms[i].transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[0]))
                {
                    nextRoom = adjacentRooms[i];
                }
            }
        }

        return nextRoom;
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
        //transform position needs to be grabbed from the room from the list (nextRoom)
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

    IEnumerator WarmUpTime(float wait)
    {
        yield return new WaitForSeconds(wait);
        GetAdjRooms();
    }
}
