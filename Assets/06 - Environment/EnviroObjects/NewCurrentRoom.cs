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
    public Vector3 nextRoom;
    private BoxCollider2D thisColl;

    // Use this for initialization
    void Start () {
        roomGen = GetComponentInParent<RoomGeneration>();
        parentWall = GetComponentInParent<DoorGen>();

        List<RoomGeneration> adjacentRooms = new List<RoomGeneration>();

        StartCoroutine(WarmUpTime(0.1f));
    }
	
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(nextRoom);

        if (collision.gameObject.name == "Player" && RoomManager.allActive == true)
        {
            StartCoroutine(ActivateRoom(collision.transform));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            roomGen.roomActive = false;
        }
    }

    public void GetAdjRooms()
    {
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
    
        foreach (RoomGeneration adj in adjacentRooms)
        {
            if(parentWall.name == "WallTop")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[0]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }
            if (parentWall.name == "WallBottom")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[1]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }
            if (parentWall.name == "WallTL")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[2]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }
            if (parentWall.name == "WallTR")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[3]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }
            if (parentWall.name == "WallBL")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[4]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }
            if (parentWall.name == "WallBR")
            {
                if (adj.transform.position == (roomGen.transform.position + RoomGeneration.spawnLocation[5]))
                {
                    nextRoom = adj.transform.position;
                    break;
                }
            }

        }
    }


    IEnumerator ActivateRoom(Transform player)
    {
        RoomManager.allActive = false;
        roomGen.roomActive = true;
        CameraController cam = FindObjectOfType<CameraController>();
        cam.MoveCamera(nextRoom);
        Vector3 playerStartPos = player.transform.position;
        BulletWeapon[] bullets = FindObjectsOfType<BulletWeapon>();
        foreach (BulletWeapon bullet in bullets)
        {
            Destroy(bullet.gameObject);
        }
        float distance = Vector3.Distance(nextRoom, player.position);
        float endDistance = Vector3.Distance(nextRoom, player.position);
        while (distance > endDistance)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, nextRoom, 2 * Time.deltaTime);
            distance = Vector3.Distance(nextRoom, player.position);
            yield return null;
        }
        RoomManager.allActive = true;

        roomGen.ToggleRoomUnlock();
    }

    IEnumerator WarmUpTime(float wait)
    {
        yield return new WaitForSeconds(wait);
        GetAdjRooms();
    }
}
