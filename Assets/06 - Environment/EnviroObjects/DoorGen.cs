using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorGen : MonoBehaviour
{

    public bool doorWall, done;
    SpriteRenderer spriteRenderer;
    Collider2D cbox;
    [SerializeField] GameObject open, close;
    public enum DoorLoc { top, bottom, upperLeft, upperRight, lowerLeft, lowerRight };
    public DoorLoc doorLocation;
    Vector3Int[] roomLocation;
    private RoomManager worldController;
    public float doorChance;
	bool doorListener;
    private RoomGeneration roomgen;

    // Use this for initialization
    void Start()
    {
        roomLocation = new Vector3Int[]
        {
            new Vector3Int(0, 8, 0) ,
            new Vector3Int(0, -8, 0) ,
            new Vector3Int(-12, 4, 0) ,
            new Vector3Int(12, 4, 0) ,
            new Vector3Int(-12, -4, 0) ,
            new Vector3Int(12, -4, 0)
        };
        done = false;
        StaticWallCheck();
        spriteRenderer = GetComponent<SpriteRenderer>();
        cbox = GetComponent<Collider2D>();
        worldController = FindObjectOfType<RoomManager>();
        LastDoorCheck();
        SpawnDoor();
		doorListener = doorWall;
        roomgen = GetComponentInParent<RoomGeneration>();
	}

    // Update is called once per frame
    void Update()
    {
		if (doorListener != doorWall)
		{
			SpawnDoor();
			doorListener = doorWall;
		}

        //print("Door chance: " + doorChance);
	}

    //checks abyss at the end of RoomGen, 1 means it will never spawn a door
    void SetDoorChance()
    {
        if (RoomGeneration.abyss == true)
        {
            doorChance = 1f;
        }
        else
        {
            doorChance = 0.66f;
        }
    }

    void RandomizeWall()
    {
        SetDoorChance();
        if (Random.value > doorChance)
        {
            doorWall = true;
        }
        else
        {
            doorWall = false;
        }

    }

    void StaticWallCheck()
    {

        RoomGeneration[] rooms = FindObjectsOfType<RoomGeneration>();
        Transform parent = transform.parent;
        foreach (RoomGeneration room in rooms)
        {
            if (room.transform.position == new Vector3(
                            parent.transform.position.x + roomLocation[(int)doorLocation].x,
                            parent.transform.position.y + roomLocation[(int)doorLocation].y,
                            0))
            {
                if (IsThereAnOpenDoor(room))
                {
                    doorWall = true;
                }
                else
                {
                    doorWall = false;
                }
                done = true;
                return;
            }

        }
        RandomizeWall();
    }

    private bool IsThereAnOpenDoor(RoomGeneration room)
    {
        DoorGen[] walls = room.GetComponentsInChildren<DoorGen>();
        DoorGen adjacentWall;
        switch (doorLocation)
        {
            case DoorLoc.top:
                adjacentWall = walls[(int)DoorLoc.bottom];
                break;
            case DoorLoc.bottom:
                adjacentWall = walls[(int)DoorLoc.top];
                break;
            case DoorLoc.upperLeft:
                adjacentWall = walls[(int)DoorLoc.lowerRight];
                break;
            case DoorLoc.upperRight:
                adjacentWall = walls[(int)DoorLoc.lowerLeft];
                break;
            case DoorLoc.lowerLeft:
                adjacentWall = walls[(int)DoorLoc.upperRight];
                break;
            case DoorLoc.lowerRight:
                adjacentWall = walls[(int)DoorLoc.upperLeft];
                break;
            default:
                adjacentWall = walls[(int)DoorLoc.top];
                break;
        }
        if (adjacentWall.doorWall)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

void SpawnDoor()
    {
        if (doorWall)
        {
            if (close)
            {
                close.SetActive(false);
            }
            else
            {
                spriteRenderer.color = Color.green;
                cbox.enabled = false;

            }
        }
        else
        {
            if (open)
            {
                open.SetActive(false);
            }
            else
            {
                spriteRenderer.color = Color.red;
                cbox.enabled = true;

            }
        }
    }

    void LastDoorCheck()
    {
        if(doorLocation == DoorLoc.lowerRight )
        {			
            RoomGeneration room = GetComponentInParent<RoomGeneration>();
			room.DoorsLeft();
		}
	}
}
