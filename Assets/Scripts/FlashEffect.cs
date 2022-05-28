using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [Tooltip("Flash Type")]
    [SerializeField] bool isUsingPlayer;

    SpriteRenderer spriteRenderer;
    Material flashMaterial;
    Material originalMaterial;

    Coroutine flashRoutine;
    float effectDuration;

    void Awake()
    {
        flashMaterial = Resources.Load("Materials/Flash Material") as Material;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Start()
    {
        effectDuration = isUsingPlayer ?
                         GetComponent<PlayerState>().DamageCooldown :
                         GetComponent<Enemy>().DamageCooldown;

        originalMaterial = spriteRenderer.material;
    }

    public void Flash()
    {
        flashRoutine = StartCoroutine(StartFlashEffect());
        Invoke("StopFlash", effectDuration);
    }

    void StopFlash()
    {
        StopCoroutine(flashRoutine);
        spriteRenderer.material = originalMaterial;
    }

    IEnumerator StartFlashEffect()
    {
        var flashDuration = 0.25f;

        while (true)
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(flashDuration);

            spriteRenderer.material = originalMaterial;
            yield return new WaitForSeconds(flashDuration);
        }
    }
}
