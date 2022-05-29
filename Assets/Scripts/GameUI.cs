using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    private GameInfo gameInfo;

    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathText;

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

        healthBar = GameObject.Find("HealthBarSlider").GetComponent<Slider>();
        playerState = FindObjectOfType<PlayerState>();
    }

    private void Start()
    {
        healthBar.maxValue = playerState.Health;
        healthBar.value = playerState.Health;

        SetScoreText();
        SetDeathText();
    }
}
