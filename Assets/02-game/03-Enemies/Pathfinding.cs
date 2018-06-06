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


	public List<Waypoint> GetPath(Transform end, CustomNavMesh cNavMesh, Waypoint currentNodePos)
	{
		startWaypoint = currentNodePos;
		endWaypoint = TransformToWaypoint(end);
		ClearAll();
		LoadNodes(cNavMesh);
		BreathFirstSearch(cNavMesh);
		CreatePath();
		return path;
	}
	public Waypoint TransformToWaypoint(Transform pos)
	{
		Vector2Int posV = new Vector2Int(Mathf.RoundToInt(pos.position.x), Mathf.RoundToInt(pos.position.y));
		Waypoint shortestWaypoint = new Waypoint(posV);
		float shortestDistance = -1;
		if (grid.ContainsKey(posV))
		{
			shortestWaypoint = grid[posV];
		}
		else
		{
			foreach (var node in grid)
			{
				float distance = Vector2.Distance(posV, node.Key);
				if (distance <= shortestDistance || shortestDistance < 0)
				{
					shortestDistance = distance;
					shortestWaypoint = node.Value;
				}
			}
		}
		return shortestWaypoint;
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
				//Debug.Log(previous.position);
				//Debug.Log(previous.exploredFrom);
				try
				{
					previous = previous.exploredFrom;
				}
				catch
				{
					break;
				}
			}
			path.Add(startWaypoint);
		}
		path.Reverse();
	}

	private void BreathFirstSearch(CustomNavMesh cNavMesh)
	{
		queue.Enqueue(startWaypoint);

		while (queue.Count > 0 && isRunning)
		{
			searchCenter = queue.Dequeue();
			HaltIfEndFound();
			ExploreNeighbors(cNavMesh.canNotMovePast);
			searchCenter.isExplored = true;
		}
	}

	private void HaltIfEndFound()
	{
		if (searchCenter.position == endWaypoint.position)
		{
			endWaypoint = searchCenter;
			isRunning = false;
		}
	}

	private void ExploreNeighbors(LayerMask canNotMovePast)
	{
		if (isRunning)
		{
			foreach (Vector2Int direction in CustomNavMesh.directions)
			{
				Vector2Int neighborCoordinate = searchCenter.position + direction;
				if (grid.ContainsKey(neighborCoordinate))
				{
					if (!Physics2D.Raycast(searchCenter.position, direction, 1, canNotMovePast))
					{
						QueueNewNeighbors(neighborCoordinate);
					}
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

	private void LoadNodes(CustomNavMesh cNavMesh)
	{
		var waypoints = cNavMesh.points;		
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
