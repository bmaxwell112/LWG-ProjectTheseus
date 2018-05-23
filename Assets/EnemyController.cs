using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] int enemyType;
	BasicEnemy basic;
	RangeShortEnemy rangeShort;
	PlayerController player;
	Pathfinding pathfinding;
	public bool stunned;
	bool recalculate;
	Vector3 tracking;

	// Use this for initialization
	void Start () {
		tracking = transform.position;
		player = FindObjectOfType<PlayerController>();
		pathfinding = GetComponent<Pathfinding>();
		StartCoroutine(UpdateMovement());
		StartCoroutine(RecalculateTime());
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
				//basic.EnemyUpdate();
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
		yield return new WaitForSeconds(5);
		while (true)
		{
			recalculate = false;
			print("starting");
			var path = pathfinding.GetPath(player.transform);
			foreach (Waypoint node in path)
			{
				Vector3 pos = new Vector3(node.position.x, node.position.y);
				while (Vector3.Distance(pos, transform.position) > 0.05f)
				{
					transform.position = Vector3.MoveTowards(transform.position, pos, (2 - 0.5f) * Time.deltaTime);
					yield return null;
				}
				if (recalculate)
				{
					break;
				}
			}
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
