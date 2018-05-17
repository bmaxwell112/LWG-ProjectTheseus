using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	[SerializeField] int enemyType;
	BasicEnemy basic;
	RangeShortEnemy rangeShort;
	public bool stunned;

	// Use this for initialization
	void Start () {
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
		if (!stunned)
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
	}
}
