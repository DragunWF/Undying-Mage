using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo : MonoBehaviour
{
    public int Deaths { get; private set; }

    private int damageLevel = 1;
    private int fireRateLevel = 1;
    private int acrobaticsLevel = 1; // Movement and jumping

    private float statIncreasePercentage = 0.25f;
    private float enemySpawnTimePercentage = 1;

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
        damageLevel++;
    }

    public void IncrementFireRateLevel()
    {
        fireRateLevel++;
    }

    public void IncrementAcrobaticsLevel()
    {
        acrobaticsLevel++;
    }

    public void IncrementDeaths()
    {
        Deaths++;
    }

    #endregion

    public void ResetGame()
    {
        Deaths = 0;
        damageLevel = 1;
        fireRateLevel = 1;
        acrobaticsLevel = 1;
    }

    private void Awake()
    {
        ResetGame();
    }

    private float GetMultipler(string type)
    {
        int baseLevel;

        switch (type.ToLower())
        {
            case "damage":
                baseLevel = damageLevel;
                break;
            case "firerate":
                baseLevel = fireRateLevel;
                break;
            case "acrobatics":
                baseLevel = acrobaticsLevel;
                break;
            default:
                throw new System.Exception("Unknown Stat Type Entered");
        }

        return baseLevel * statIncreasePercentage;
    }
}
