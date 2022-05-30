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
        var type = "acrobatics";

        if (HasEnoughPoints())
        {
            if (!gameInfo.CheckLevelLimit(type))
            {
                gameInfo.IncrementAcrobaticsLevel();
                gameInfo.ExecuteUpgradeCost();
                SetTextUI();
                CheckMaxLevel(type);
            }
            else
                audioPlayer.PlayError();
        }
    }

    public void UpgradeFireRate()
    {
        var type = "firerate";

        if (HasEnoughPoints())
        {
            if (!gameInfo.CheckLevelLimit(type))
            {
                gameInfo.IncrementFireRateLevel();
                gameInfo.ExecuteUpgradeCost();
                SetTextUI();
                CheckMaxLevel(type);
            }
            else
                audioPlayer.PlayError();
        }
    }

    public void UpgradeDamage()
    {
        var type = "damage";

        if (HasEnoughPoints())
        {
            if (!gameInfo.CheckLevelLimit(type))
            {
                gameInfo.IncrementDamageLevel();
                gameInfo.ExecuteUpgradeCost();
                SetTextUI();
                CheckMaxLevel(type);
            }
            else
                audioPlayer.PlayError();
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
        acrobaticsLevel.text = string.Format("Acrobatics: Lvl {0}", gameInfo.AcrobaticsLevel + 1);
        fireRateLevel.text = string.Format("Firerate: Lvl {0}", gameInfo.FireRateLevel + 1);
        damageLevel.text = string.Format("Damage: Lvl {0}", gameInfo.DamageLevel + 1);

        pointsText.text = string.Format("Points:{0}", gameInfo.Points);
        deathsText.text = string.Format("Deaths:{0}", gameInfo.Deaths);
    }

    private void SetMaxLevelTextUI(string type)
    {
        switch (type.ToLower())
        {
            case "damage":
                damageLevel.text = string.Format("Damage:Max");
                break;
            case "acrobatics":
                acrobaticsLevel.text = string.Format("Acrobatics:Max");
                break;
            case "firerate":
                fireRateLevel.text = string.Format("Firerate:Max");
                break;
        }
    }

    private void CheckMaxLevel(string type)
    {
        if (gameInfo.CheckLevelLimit(type))
            SetMaxLevelTextUI(type);
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
