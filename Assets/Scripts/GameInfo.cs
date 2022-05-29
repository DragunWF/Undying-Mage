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

    private float statIncreasePercentage = 0.25f;
    private float enemySpawnTimePercentage = 1;

    static private GameInfo instance;

    public void ExecuteUpgradeCost()
    {
        var upgradeCost = 100;
        Points -= upgradeCost;
    }

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

    public void ResetGame()
    {
        Deaths = 0;
        DamageLevel = 1;
        FireRateLevel = 1;
        AcrobaticsLevel = 1;
    }

    private void Awake()
    {
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
