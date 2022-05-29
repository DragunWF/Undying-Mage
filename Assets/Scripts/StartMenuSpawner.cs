using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSpawner : MonoBehaviour
{
    private const int maxSpawnTimeInterval = 6;
    private const int minSpawnTimeInterval = 2;

    private GameObject monster;
    private Transform spawnPos;

    private void Awake()
    {
        spawnPos = GameObject.Find("SpawnPosition").transform;
    }

    private void SpawnMonster()
    {
        // Instantiate()
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {

            yield return new WaitForSeconds(Random.Range(minSpawnTimeInterval,
                                                         maxSpawnTimeInterval));
        }
    }
}
