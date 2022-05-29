using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuUI : MonoBehaviour
{
    private TextMeshProUGUI scoreText;
    private TextMeshProUGUI deathsText;

    private TextMeshProUGUI acrobaticsLevel;
    private TextMeshProUGUI fireRateLevel;
    private TextMeshProUGUI damageLevel;

    private GameInfo gameInfo;

    public void UpgradeAcrobatics()
    {
        gameInfo.IncrementDamageLevel();
        SetTextUI();
    }

    public void UpgradeFireRate()
    {
        gameInfo.IncrementFireRateLevel();
        SetTextUI();
    }

    public void UpgradeDamageLevel()
    {
        gameInfo.IncrementDamageLevel();
        SetTextUI();
    }

    private void Awake()
    {
        acrobaticsLevel = GameObject.Find("AcrobaticsLevel").GetComponent<TextMeshProUGUI>();
        fireRateLevel = GameObject.Find("FireRateLevel").GetComponent<TextMeshProUGUI>();
        damageLevel = GameObject.Find("DamageLevel").GetComponent<TextMeshProUGUI>();

        gameInfo = FindObjectOfType<GameInfo>();
        SetTextUI();
    }

    private void SetTextUI()
    {
        acrobaticsLevel.text = string.Format("Acrobatics: Lvl {0}");
        fireRateLevel.text = string.Format("Firerate: Lvl {0}");
        damageLevel.text = string.Format("Damage: Lvl {0}");
    }
}
