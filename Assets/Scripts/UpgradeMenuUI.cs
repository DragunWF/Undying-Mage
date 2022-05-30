using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenuUI : MonoBehaviour
{
    private TextMeshProUGUI pointsText;
    private TextMeshProUGUI deathsText;
    private TextMeshProUGUI warningText;

    private TextMeshProUGUI acrobaticsLevel;
    private TextMeshProUGUI fireRateLevel;
    private TextMeshProUGUI damageLevel;

    private GameInfo gameInfo;
    private AudioPlayer audioPlayer;

    public void UpgradeAcrobatics()
    {
        if (HasEnoughPoints())
        {
            gameInfo.IncrementAcrobaticsLevel();
            gameInfo.ExecuteUpgradeCost();
            SetTextUI();
        }
    }

    public void UpgradeFireRate()
    {
        if (HasEnoughPoints())
        {
            gameInfo.IncrementFireRateLevel();
            gameInfo.ExecuteUpgradeCost();
            SetTextUI();
        }
    }

    public void UpgradeDamage()
    {
        if (HasEnoughPoints())
        {
            gameInfo.IncrementDamageLevel();
            gameInfo.ExecuteUpgradeCost();
            SetTextUI();
        }
    }

    private void Awake()
    {
        acrobaticsLevel = GameObject.Find("AcrobaticsLevel").GetComponent<TextMeshProUGUI>();
        fireRateLevel = GameObject.Find("FireRateLevel").GetComponent<TextMeshProUGUI>();
        damageLevel = GameObject.Find("DamageLevel").GetComponent<TextMeshProUGUI>();

        pointsText = GameObject.Find("PointsText").GetComponent<TextMeshProUGUI>();
        deathsText = GameObject.Find("DeathsText").GetComponent<TextMeshProUGUI>();
        warningText = GameObject.Find("WarningText").GetComponent<TextMeshProUGUI>();

        gameInfo = FindObjectOfType<GameInfo>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    private void Start()
    {
        gameInfo.TransferScoreToPoints();
        warningText.gameObject.SetActive(false);
        SetTextUI();
    }

    private void SetTextUI()
    {
        acrobaticsLevel.text = string.Format("Acrobatics: Lvl {0}", gameInfo.AcrobaticsLevel);
        fireRateLevel.text = string.Format("Firerate: Lvl {0}", gameInfo.FireRateLevel);
        damageLevel.text = string.Format("Damage: Lvl {0}", gameInfo.DamageLevel);

        pointsText.text = string.Format("Points:{0}", gameInfo.Points);
        deathsText.text = string.Format("Deaths:{0}", gameInfo.Deaths);
    }

    private bool HasEnoughPoints()
    {
        if (gameInfo.Points >= 100)
        {
            audioPlayer.PlayUpgrade();
            return true;
        }

        audioPlayer.PlayError();
        warningText.gameObject.SetActive(true);
        return false;
    }
}
