using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoKeeper : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    TextMeshProUGUI deathText;
    int score = 0;

    public void IncreaseScore(int gainAmount)
    {
        score += gainAmount;
        scoreText.text = string.Format("Score:{0}", score);
    }

    void Awake()
    {
        scoreText = GameObject.Find("Score Text")
                    .GetComponent<TextMeshProUGUI>();
        deathText = GameObject.Find("Death Text")
                    .GetComponent<TextMeshProUGUI>();
    }
}
