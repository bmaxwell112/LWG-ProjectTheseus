using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Waypoint {
	public bool isExplored;
	public Waypoint exploredFrom;
	public Vector2Int position;

	public Waypoint(Vector2Int pos)
	{
		position = pos;
	}
}
