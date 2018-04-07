using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

	[SerializeField] float spawnRate;
	[SerializeField] GameObject enemyToSpawn;

	void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(transform.position, Vector3.one);
	}

	// Use this for initialization
	void Start () {
		if (spawnRate == 0)
		{
			Spawn();
		}
		else
		{
			StartCoroutine(SpawnNow(spawnRate));
		}
	}

	IEnumerator SpawnNow(float time)
	{
		while (true)
		{
			Spawn();
			yield return new WaitForSeconds(time);
		}
	}

	void Spawn()
	{
		Instantiate(enemyToSpawn, transform.position, Quaternion.identity);		
	}
}
