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

    private const float scaleDifficultyTime = 12.5f;
    private const int maxDifficultyLevel = 20;

    public int GetIntensity()
    {
        return intensity;
    }

    public int GetMaxDifficultyLevel()
    {
        return maxDifficultyLevel;
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
        intensity = Mathf.Clamp(gameInfo.Deaths + ScaleLevel, 1, maxDifficultyLevel);
        var baseMaxSpawnTime = 7;
        var baseMinSpawnTime = 3;

        spawnTimeMin = Mathf.Clamp(Mathf.Round(baseMinSpawnTime - (0.15f * intensity)), 0.1f, 30);
        spawnTimeMax = Mathf.Clamp(Mathf.Round(baseMaxSpawnTime - (0.25f * intensity)), 0.3f, 30);

        gameUI.SetDifficultyText();
    }

    private void ScaleIntensity()
    {
        ScaleLevel++;
        SetSpawnerIntensity();
        if (ScaleLevel < maxDifficultyLevel)
            Invoke("ScaleIntensity", scaleDifficultyTime);
    }

    private float GetSpawnInterval()
    {
        var spawnTimes = new List<float>();
        var currentInterval = spawnTimeMin;

        while (currentInterval < spawnTimeMax)
        {
            spawnTimes.Add(currentInterval);
            currentInterval += 0.1f;
        };

        return spawnTimes[Random.Range(0, spawnTimes.Count)];
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
        chosenGroup = chance >= 2 ? enemies : flyingEnemies;

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
