using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] int enemyType;
	public float stoppingDistance;
	BasicEnemy basic;
	RangeShortEnemy rangeShort;
	PlayerController player;
	Pathfinding pathfinding;
	CustomNavMesh cNavMesh;
	RoomGeneration myRoom;
	RobotLoadout roLo;
	public bool stunned;
	bool recalculate, startMovement;
	Vector3 tracking;
	Vector3 heightOffset = new Vector3(0, 0.45f, 0);

	// Use this for initialization
	void Start () {
		tracking = transform.position;
		player = FindObjectOfType<PlayerController>();
		pathfinding = GetComponent<Pathfinding>();
		cNavMesh = GetComponentInParent<CustomNavMesh>();
		myRoom = GetComponentInParent<RoomGeneration>();
		roLo = GetComponent<RobotLoadout>();
		if (enemyType == 0)
		{
			basic = GetComponent<BasicEnemy>();
		}
		if (enemyType == 1)
		{
			rangeShort = GetComponent<RangeShortEnemy>();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (!stunned && player)
		{
			if (enemyType == 0)
			{				
				basic.EnemyUpdate();
			}
			if (enemyType == 1)
			{
				rangeShort.EnemyUpdate();
			}
		}
		else if (!player)
		{
			player = FindObjectOfType<PlayerController>();
		}
		if (myRoom.roomActive && !startMovement)
		{
			StopAllCoroutines();
			StartCoroutine(UpdateMovement());
			StartCoroutine(RecalculateTime());
			startMovement = true;
		}
		if (!myRoom.roomActive && startMovement)
		{
			StopAllCoroutines();
			startMovement = false;
		}
	}
	public void ChangeBehaviour(int enemy)
	{
		enemyType = enemy;
		if (enemyType == 0)
		{
			if(rangeShort)
				Destroy(rangeShort);
			if (!basic)
				basic = gameObject.AddComponent<BasicEnemy>();
		}
	}

	IEnumerator UpdateMovement()
	{
		yield return new WaitForSeconds(2);
		Waypoint currentNodePos = pathfinding.TransformToWaypoint(transform);
		while (true)
		{
			recalculate = false;
			if (player && cNavMesh)
			{				
				List<Waypoint> path = pathfinding.GetPath(player.transform, cNavMesh, currentNodePos);
				if (path.Count > 1)
				{
					foreach (Waypoint node in path)
					{
						Vector3 pos = new Vector3(node.position.x, node.position.y + 0.45f);
						float checkTime = Time.time;
						while (Vector3.Distance(pos, transform.position) > stoppingDistance)
						{
							currentNodePos = node;
							transform.position = Vector3.MoveTowards(transform.position, pos, (roLo.loadout[(int)ItemLoc.legs].itemSpeed - 0.5f) * Time.deltaTime);
							roLo.walk = true;
							if (Time.time > (checkTime + 5))
							{
								print("break");
								path = pathfinding.GetPath(player.transform, cNavMesh, node);
								break;
							}
							yield return null;
						}
						roLo.walk = false;
						if (recalculate)
						{
							break;
						}
					}
				}
			}
			roLo.walk = false;
			yield return null;
		}
	}
	IEnumerator RecalculateTime()
	{
		while (true)
		{
			yield return new WaitForSeconds(1);
			recalculate = true;
		}
	}
}
