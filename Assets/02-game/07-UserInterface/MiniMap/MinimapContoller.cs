using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinimapContoller : MonoBehaviour {

	[SerializeField] MiniRoom mapRoom;	
	List<MiniRoom> miniRooms = new List<MiniRoom>();
	Vector3 startPos;

	void Start()
	{
		startPos = transform.localPosition;
	}

	public void GetRoomPos()
	{
		foreach (RoomGeneration room in RoomManager.instance.allRooms)
		{
			var spawnLoc = new Vector3(room.transform.position.x, room.transform.position.y);
			MiniRoom miniRoom = Instantiate(mapRoom, transform) as MiniRoom;
			miniRoom.transform.localPosition = spawnLoc*7;
			miniRoom.SpawnConfig();
			miniRooms.Add(miniRoom);
		}
		UpdateActiveMiniRoom();
	}

	public void UpdateActiveMiniRoom()
	{
		for (int i = 0; i < miniRooms.Count; i++)
		{
			if (RoomManager.instance.allRooms[i].GetActive())
			{
				if (!miniRooms[i].GetFound())
				{
					miniRooms[i].CheckRoomForOpenDoors(RoomManager.instance.allRooms[i]);
				}
				miniRooms[i].UpdateActive(true);
				transform.localPosition = startPos + miniRooms[i].transform.localPosition * -1;
			}
			else
			{
				miniRooms[i].UpdateActive(false);
			}
		}		
	}
}
