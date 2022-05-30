using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int ScaleLevel { get; private set; }
    private int intensity = 1;

    private GameObject[] enemies;
    private GameObject[] flyingEnemies;

    private Vector2 rightPos;
    private Vector2 leftPos;

    private Coroutine spawning;
    private float spawnTimeMin;
    private float spawnTimeMax;

    private GameInfo gameInfo;
    private GameUI gameUI;
    private const float scaleDifficultyTime = 15;

    public int GetIntensity()
    {
        return intensity;
    }

    public void StopSpawner()
    {
        StopCoroutine(spawning);
    }

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

        gameInfo = FindObjectOfType<GameInfo>();
        gameUI = FindObjectOfType<GameUI>();
    }

    private void Start()
    {
        SetSpawnerIntensity();
        Invoke("ScaleIntensity", scaleDifficultyTime);
        spawning = StartCoroutine(SpawnEnemies());
    }

    private void SetSpawnerIntensity()
    {
        intensity = Mathf.Clamp(gameInfo.Deaths + ScaleLevel, 1, 10);
        var baseMaxSpawnTime = 8;
        var baseMinSpawnTime = 4;

        spawnTimeMin = Mathf.Clamp(Mathf.Round(baseMinSpawnTime - (0.25f * intensity)), 0.5f, 30);
        spawnTimeMax = Mathf.Clamp(Mathf.Round(baseMaxSpawnTime - (0.5f * intensity)), 1.5f, 30);

        gameUI.SetDifficultyText();
    }

    private void ScaleIntensity()
    {
        ScaleLevel++;
        SetSpawnerIntensity();
        if (ScaleLevel < 10)
            Invoke("ScaleIntensity", scaleDifficultyTime);
    }

    private float GetSpawnInterval()
    {
        return Random.Range(spawnTimeMin, spawnTimeMax);
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
