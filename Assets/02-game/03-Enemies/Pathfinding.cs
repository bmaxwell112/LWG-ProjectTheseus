using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour {
	[SerializeField] Waypoint startWaypoint, endWaypoint;

	Dictionary<Vector2Int, Waypoint> grid = new Dictionary<Vector2Int, Waypoint>();
	Queue<Waypoint> queue = new Queue<Waypoint>();
	bool isRunning = true;
	Waypoint searchCenter;
	List<Waypoint> path = new List<Waypoint>(); //todo make private


	public List<Waypoint> GetPath(Transform end)
	{
		startWaypoint = TransformToWaypoint(transform);
		endWaypoint = TransformToWaypoint(end);
		ClearAll();
		LoadNodes();
		BreathFirstSearch();
		CreatePath();
		return path;
	}
	Waypoint TransformToWaypoint(Transform pos)
	{
		return new Waypoint(new Vector2Int(Mathf.RoundToInt(pos.position.x), Mathf.RoundToInt(pos.position.y)));
	}

	private void ClearAll()
	{
		grid.Clear();
		path.Clear();
		queue.Clear();
		isRunning = true;
	}

	private void CreatePath()
	{		
		path.Add(endWaypoint);
		if (startWaypoint.position != endWaypoint.position)
		{
			Waypoint previous = endWaypoint.exploredFrom;
			while (previous != startWaypoint)
			{
				path.Add(previous);
				previous = previous.exploredFrom;
			}
			path.Add(startWaypoint);
		}
		path.Reverse();
	}

	private void BreathFirstSearch()
	{
		queue.Enqueue(startWaypoint);

		while (queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			HaltIfEndFound();
			ExploreNeighbors();
			searchCenter.isExplored = true;
		}
	}

	private void HaltIfEndFound()
	{
		if (searchCenter.position == endWaypoint.position)
		{
			print("found end");
			endWaypoint = searchCenter;
			isRunning = false;
		}
	}

	private void ExploreNeighbors()
	{
		if (isRunning)
		{
			foreach (Vector2Int direction in CustomNavMesh.directions)
			{
				Vector2Int neighborCoordinate = searchCenter.position + direction;
				if (grid.ContainsKey(neighborCoordinate))
				{
					QueueNewNeighbors(neighborCoordinate);
				}
			}
		}
	}

	private void QueueNewNeighbors(Vector2Int neighborCoordinate)
	{
		Waypoint neighbor = grid[neighborCoordinate];
		if (neighbor.isExplored || queue.Contains(neighbor))
		{
			//do nothing
		}
		else
		{
			queue.Enqueue(neighbor);
			neighbor.exploredFrom = searchCenter;
		}
	}

	private void LoadNodes()
	{
		var waypoints = FindObjectOfType<CustomNavMesh>().points;		
		foreach (Vector2Int waypoint in waypoints)
		{
			// overlapping blocks?			
			if (grid.ContainsKey(waypoint))
			{
				Debug.LogWarning("Skipping Overlapping Block " + waypoint);
			}
			else
			{
				// add to dictionary
				grid.Add(waypoint, new Waypoint(waypoint));
			}
		}
	}
}
