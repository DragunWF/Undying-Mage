using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [Tooltip("Flash Type")]
    [SerializeField] bool isUsingPlayer;

    private SpriteRenderer spriteRenderer;
    private Material flashMaterial;
    private Material originalMaterial;

    private Coroutine flashRoutine;
    private float effectDuration;

    private void Awake()
    {
        flashMaterial = Resources.Load("Materials/Flash Material") as Material;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
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

    private void StopFlash()
    {
        StopCoroutine(flashRoutine);
        spriteRenderer.material = originalMaterial;
    }

    private IEnumerator StartFlashEffect()
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
