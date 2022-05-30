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

    private const float statIncreasePercentage = 0.25f;

    static private GameInfo instance;
    private GameUI gameUI;

    #region Multiplier Getter Methods

    public float DamageMultiplier
    {
        get { return GetMultipler("damage"); }
    }

    public float FireRateMultiplier
    {
        get { return GetMultipler("firerate"); }
    }

    public float AcrobaticsMultiplier
    {
        get { return GetMultipler("acrobatics"); }
    }

    #endregion

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

    private float GetMultipler(string type)
    {
        int baseLevel;

        switch (type.ToLower())
        {
            case "damage":
                baseLevel = DamageLevel;
                break;
            case "firerate":
                baseLevel = FireRateLevel;
                break;
            case "acrobatics":
                baseLevel = AcrobaticsLevel;
                break;
            default:
                throw new System.Exception("Unknown Stat Type Entered");
        }

        return baseLevel * statIncreasePercentage;
    }
}
