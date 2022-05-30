using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private GameInfo gameInfo;
    private EnemySpawner enemySpawner;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathText;
    private TextMeshProUGUI difficultyText;

    private Slider healthBar;
    private PlayerState playerState;

    public void SetScoreText()
    {
        scoreText.text = string.Format("Score:{0}", gameInfo.Score);
    }

    public void SetDeathText()
    {
        deathText.text = string.Format("Deaths:{0}", gameInfo.Deaths);
    }

    public void SetDifficultyText()
    {
        var level = enemySpawner.GetIntensity();
        var displayedLevel = level >= enemySpawner.GetMaxDifficultyLevel() ?
                            "Max" : level.ToString();
        difficultyText.text = string.Format("Difficulty:{0}", displayedLevel);
    }

    public void UpdateHealthBar()
    {
        var barValue = playerState.Health;
        healthBar.value = barValue;
    }

    private void Awake()
    {
        gameInfo = FindObjectOfType<GameInfo>();

        scoreText = GameObject.Find("Score Text")
                    .GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("Deaths Text")
                    .GetComponent<TextMeshProUGUI>();
        difficultyText = GameObject.Find("Difficulty Text")
                         .GetComponent<TextMeshProUGUI>();

        healthBar = GameObject.Find("HealthBarSlider").GetComponent<Slider>();
        playerState = FindObjectOfType<PlayerState>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
    }

    private void Start()
    {
        gameInfo.RedefineGameUI();

        healthBar.maxValue = playerState.Health;
        healthBar.value = playerState.Health;

        SetScoreText();
        SetDeathText();
        SetDifficultyText();
    }
}
