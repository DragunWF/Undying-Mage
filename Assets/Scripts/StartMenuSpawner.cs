using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuSpawner : MonoBehaviour
{
    private const int maxSpawnTimeInterval = 6;
    private const int minSpawnTimeInterval = 2;

    private GameObject monster;
    private Vector2 spawnPos;

    private void Awake()
    {
        monster = Resources.Load("Prefabs/StartMenuMonster") as GameObject;
        spawnPos = GameObject.Find("SpawnPosition").transform.position;
    }

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private IEnumerator SpawnMonsters()
    {
        while (true)
        {
            Instantiate(monster, spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(minSpawnTimeInterval,
                                                         maxSpawnTimeInterval));
        }
    }
}
