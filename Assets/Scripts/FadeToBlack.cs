using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    private bool transitionInitialized;
    private const float transitionDelay = 0.25f;
    private Image blackScreen;

    private float alphaSlider = 0;
    private const float fadeSpeed = 150.5f;
    private float loadSceneDelay = Mathf.Ceil(255 / fadeSpeed) + 1.5f;

    public void InitializeFade()
    {
        Invoke("FadeScreen", transitionDelay);
        StartCoroutine(CallLoadScene());
    }

    private void Awake()
    {
        blackScreen = GameObject.Find("Black").GetComponent<Image>();
    }

    private void Update()
    {
        if (transitionInitialized)
        {
            alphaSlider += fadeSpeed * Time.deltaTime;
            alphaSlider = Mathf.Clamp(alphaSlider, 0, 255);
            blackScreen.color = new Color(0, 0, 0, alphaSlider / 255);
        }
    }

    private void FadeScreen()
    {
        transitionInitialized = true;
    }

    private IEnumerator CallLoadScene()
    {
        yield return new WaitForSeconds(loadSceneDelay);
        FindObjectOfType<GameManager>().LoadUpgradeMenu();
    }
}
