using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private GameObject[] enemies;
    private GameObject[] flyingEnemies;

    private Vector2 rightPos;
    private Vector2 leftPos;

    private float spawnIntervalMin = 0.5f;
    private float spawnIntervalMax = 1.5f;

    private void Awake()
    {
        enemies = new GameObject[4] {
            Resources.Load("Prefabs/Enemy [Fighter]") as GameObject,
            Resources.Load("Prefabs/Enemy [Spider]") as GameObject,
            Resources.Load("Prefabs/Enemy [Crawler]") as GameObject,
            Resources.Load("Prefabs/Enemy [Viking]") as GameObject
        };
        flyingEnemies = new GameObject[2] {
            Resources.Load("Prefabs/Flying Enemy") as GameObject,
            Resources.Load("Prefabs/Flying Enemy [Blue]") as GameObject
        };

        rightPos = GameObject.Find("RightPosition").transform.position;
        leftPos = GameObject.Find("LeftPosition").transform.position;
    }

    private void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    private float GetSpawnInterval()
    {
        return Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    private Vector2 GetRandomPosition()
    {
        var positions = new Vector2[2] { leftPos, rightPos };
        return positions[Random.Range(0, positions.Length)];
    }

    private GameObject ChooseRandomEnemy()
    {
        GameObject[] chosenGroup;

        var chance = Random.Range(1, 7);
        chosenGroup = chance >= 3 ? enemies : flyingEnemies;

        return chosenGroup[Random.Range(0, chosenGroup.Length)];
    }

    private IEnumerator SpawnEnemies()
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
