using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int health = 100;
    private bool isInvincibilityOn;
    private int deaths;

    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    public float DamageCooldown { get; private set; }

    private FlashEffect flashEffect;
    private AudioPlayer audioPlayer;

    private void Awake()
    {
        deaths = 0;

        MoveSpeed = 5.5f;
        JumpForce = 11.5f;

        FireballDamage = 25;
        FiringRate = 1.2f;

        DamageCooldown = 1.5f;

        flashEffect = GetComponent<FlashEffect>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }

    public void IncrementDeaths()
    {
        deaths += 1;
    }

    public void DamageHealth(int damageAmount)
    {
        if (!isInvincibilityOn)
        {
            health -= damageAmount;
            isInvincibilityOn = true;

            flashEffect.Flash();
            audioPlayer.PlayPlayerDamaged();

            StartCoroutine(TriggerDamageCooldown());
        }
    }

    private IEnumerator TriggerDamageCooldown()
    {
        yield return new WaitForSecondsRealtime(DamageCooldown);
        isInvincibilityOn = false;
    }
}
