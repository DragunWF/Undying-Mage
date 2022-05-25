using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    GameObject[] enemies;

    Vector2 rightPos;
    Vector2 leftPos;

    float spawnIntervalMin = 0.5f;
    float spawnIntervalMax = 1.5f;

    void Awake()
    {
        enemies = new GameObject[4] {
            Resources.Load("Prefabs/Enemy [Fighter]") as GameObject,
            Resources.Load("Prefabs/Enemy [Spider]") as GameObject,
            Resources.Load("Prefabs/Enemy [Crawler]") as GameObject,
            Resources.Load("Prefabs/Enemy [Viking]") as GameObject
        };
        rightPos = GameObject.Find("RightPosition").transform.position;
        leftPos = GameObject.Find("LeftPosition").transform.position;
    }

    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    float GetSpawnInterval()
    {
        return Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    Vector2 GetRandomPosition()
    {
        var positions = new Vector2[2] { leftPos, rightPos };
        return positions[Random.Range(0, positions.Length)];
    }

    GameObject ChooseRandomEnemy()
    {
        return enemies[Random.Range(0, enemies.Length)];
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Instantiate(ChooseRandomEnemy(),
                        GetRandomPosition(),
                        Quaternion.identity);

            yield return new WaitForSecondsRealtime(GetSpawnInterval());
        }
    }
}
