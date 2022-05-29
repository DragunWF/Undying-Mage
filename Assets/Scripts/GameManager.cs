using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int mainMenuSceneIndex = 0;
    private const int mainGameSceneIndex = 1;
    private const int upgradeMenuSceneIndex = 2;

    public void LoadMainMenu()
    {
        ResetGameInfo();
        SceneManager.LoadScene(mainMenuSceneIndex);
    }

    public void LoadGame()
    {
        SceneManager.LoadScene(mainGameSceneIndex);
    }

    public void LoadUpgradeMenu()
    {
        SceneManager.LoadScene(upgradeMenuSceneIndex);
    }

    private void ResetGameInfo()
    {
        var gameInfo = FindObjectOfType<GameInfo>();
        if (gameInfo != null)
            Destroy(gameInfo.gameObject);
    }
}
