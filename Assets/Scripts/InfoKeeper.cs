using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoKeeper : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathText;
    private int score = 0;

    private void Awake()
    {
        scoreText = GameObject.Find("Score Text")
                    .GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("Death Text")
                    .GetComponent<TextMeshProUGUI>();
    }

    public void IncreaseScore(int gainAmount)
    {
        score += gainAmount;
        scoreText.text = string.Format("Score:{0}", score);
    }
}
