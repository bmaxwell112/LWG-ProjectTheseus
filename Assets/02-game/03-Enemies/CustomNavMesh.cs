using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomNavMesh : MonoBehaviour {

	[SerializeField] int yOffset, yMax;
	[SerializeField] int[] xOffset, xMax;
	[SerializeField] LayerMask canNotMovePast;
	public static Vector2Int[] directions = new Vector2Int[] {
		new Vector2Int(0,1),
		new Vector2Int(1,1),
		new Vector2Int(1,0),
		new Vector2Int(1,-1),
		new Vector2Int(0,-1),
		new Vector2Int(-1,-1),
		new Vector2Int(-1,0),
		new Vector2Int(-1,1)
		};

	public List<Vector2Int> points = new List<Vector2Int>();
	void Start()
	{
		SetPoints();
		Invoke("CheckAllDirections", 1f);
	}


	void OnDrawGizmos()
	{
		DrawPoints();
	}
	private void DrawPoints()
	{
		foreach (Vector2Int point in points)
		{
			Gizmos.DrawWireCube((Vector2)point, Vector3.one*0.25f);
		}
	}

	private void SetPoints()
	{
		int yPos = yOffset;
		for (int i = 0; i < yMax; i++)
		{			
			for (int j = 0; j < xMax[i]; j++)
			{
				int xPos = xOffset[i] + j;
				if (i % 2 != 0)
				{
					xPos = xOffset[i] + j;
				}
				points.Add(new Vector2Int(xPos, yPos));
			}
			yPos -= 1;
		}
	}
	private void CheckAllDirections()
	{
		List<Vector2Int> badPoints = new List<Vector2Int>();
		Collider2D[] colls = GetComponentsInChildren<Collider2D>();
		foreach (Vector2Int point in points)
		{
			bool removed = false;
			foreach (Collider2D coll in colls)
			{
				if (coll.gameObject.layer == canNotMovePast)
				{
					if (coll.bounds.Contains((Vector2)point))
					{
						badPoints.Add(point);
						removed = true;
						break;
					}
				}
			}
			if (!removed)
			{
				for (int i = 0; i < directions.Length; i++)
				{
					if (Physics2D.Raycast((Vector2)point, directions[i], 0.15f, canNotMovePast))
					{
						badPoints.Add(point);
						break;
					}
				}
			}
			
		}		
		foreach (Vector2Int point in badPoints)
		{
			points.Remove(point);
		}
	}
}

