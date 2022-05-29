using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathText;
    private int score = 0;

    public void IncreaseScore(int gainAmount)
    {
        score += gainAmount;
        scoreText.text = string.Format("Score:{0}", score);
    }

    private void Awake()
    {
        scoreText = GameObject.Find("Score Text")
                    .GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("Deaths Text")
                    .GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        var deathCount = FindObjectOfType<GameInfo>().Deaths;
        deathText.text = string.Format("Deaths:{0}", deathCount);
        IncreaseScore(score); // Purely just to set the score
    }
}
