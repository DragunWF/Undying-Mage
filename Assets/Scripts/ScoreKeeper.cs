using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreKeeper : MonoBehaviour
{
    TextMeshProUGUI scoreText;
    int score = 0;

    public void IncreaseScore(int gainAmount)
    {
        score += gainAmount;
        scoreText.text = string.Format("Score:{0}", score);
    }

    void Awake()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
    }
}
