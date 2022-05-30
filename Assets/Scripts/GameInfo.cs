using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Deaths { get; private set; }
    public int Score { get; private set; }
    public int Points { get; private set; }

    public int DamageLevel { get; private set; }
    public int FireRateLevel { get; private set; }
    public int AcrobaticsLevel { get; private set; }

    private const int maxAcrobaticsLevel = 4;
    private const int maxFireRateLevel = 9;
    private const int maxDamageLevel = 14;

    static private GameInfo instance;
    private GameUI gameUI;

    public bool CheckLevelLimit(string statType)
    {
        int limit = 0;
        int level = 0;

        switch (statType.ToLower())
        {
            case "damage":
                level = DamageLevel;
                limit = maxDamageLevel;
                break;
            case "acrobatics":
                level = AcrobaticsLevel;
                limit = maxAcrobaticsLevel;
                break;
            case "firerate":
                level = FireRateLevel;
                limit = maxFireRateLevel;
                break;
        }

        return level >= limit;
    }

    #region Incrementor Setter Methods

    public void IncrementDamageLevel()
    {
        DamageLevel++;
    }

    public void IncrementFireRateLevel()
    {
        FireRateLevel++;
    }

    public void IncrementAcrobaticsLevel()
    {
        AcrobaticsLevel++;
    }

    public void IncrementDeaths()
    {
        Deaths++;
    }

    #endregion

    public void ExecuteUpgradeCost()
    {
        var upgradeCost = 100;
        Points -= upgradeCost;
    }

    public void IncreaseScore(int gainAmount)
    {
        if (gameUI != null)
        {
            Score += gainAmount;
            gameUI.SetScoreText();
        }
    }

    public void TransferScoreToPoints()
    {
        Points += Score;
        Score = 0;
    }

    public void ResetGame()
    {
        Deaths = 0;
        DamageLevel = 0;
        FireRateLevel = 0;
        AcrobaticsLevel = 0;
    }

    public void RedefineGameUI()
    {
        if (gameUI == null)
            gameUI = FindObjectOfType<GameUI>();
    }

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
        ResetGame();
        ManageSingleton();
    }

    private void ManageSingleton()
    {
        if (instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
