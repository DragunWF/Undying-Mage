using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathText;

    private GameInfo gameInfo;

    public void SetScoreText()
    {
        scoreText.text = string.Format("Score:{0}", gameInfo.Score);
    }

    public void SetDeathText()
    {
        deathText.text = string.Format("Deaths:{0}", gameInfo.Deaths);
    }

    private void Awake()
    {
        scoreText = GameObject.Find("Score Text")
                    .GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("Deaths Text")
                    .GetComponent<TextMeshProUGUI>();
        gameInfo = FindObjectOfType<GameInfo>();
    }

    private void Start()
    {
        SetScoreText();
        SetDeathText();
    }
}
