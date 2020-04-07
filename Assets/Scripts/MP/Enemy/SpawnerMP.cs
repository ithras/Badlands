using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SpawnerMP : NetworkBehaviour
{
	[SerializeField] private GameObject[] enemies = null;
	[SerializeField] private float secondsBetweenSpawn = 0f;
	[SerializeField] private Vector2 minRange = Vector2.zero;
	[SerializeField] private Vector2 maxRange = Vector2.one;
	private float nextSpawn = 0;

	[Server]
	void Update()
	{
		if (Time.time > nextSpawn)
		{
			SpawnEnemies();
			nextSpawn = Time.time + secondsBetweenSpawn;
		}
	}

	void SpawnEnemies()
	{
		float spawnX = transform.position.x + Random.Range(minRange.x, maxRange.x);
		float spawnZ = transform.position.z + Random.Range(minRange.y, maxRange.y);
		Vector3 spawnPosition = new Vector3(spawnX, 1.5f, spawnZ);
		int enemyType = Random.Range(0, enemies.Length);

		GameObject spawnedEnemy = Instantiate(enemies[enemyType], spawnPosition, transform.rotation);
		spawnedEnemy.transform.parent = gameObject.transform;
	}
}
