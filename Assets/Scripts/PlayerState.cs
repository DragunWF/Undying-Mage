using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float Health { get; private set; }

    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    public float DamageCooldown { get; private set; }

    private bool isInvincibilityOn;

    private FlashEffect flashEffect;
    private AudioPlayer audioPlayer;
    private ParticlesPlayer particlesPlayer;

    private GameUI gameUI;
    private GameInfo gameInfo;
    private GameManager gameManager;

    private void Awake()
    {
        Health = 100;

        flashEffect = GetComponent<FlashEffect>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        particlesPlayer = FindObjectOfType<ParticlesPlayer>();

        gameUI = FindObjectOfType<GameUI>();
        gameInfo = FindObjectOfType<GameInfo>();
        gameManager = FindObjectOfType<GameManager>();

        MoveSpeed = 5.5f;
        JumpForce = 11.5f;

        FireballDamage = 25;
        FiringRate = 1.2f;

        DamageCooldown = 1.5f;
    }

    public void DamageHealth(int damageAmount)
    {
        if (!isInvincibilityOn)
        {
            Health -= damageAmount;
            gameUI.UpdateHealthBar();
            if (Health <= 0)
                Death();

            isInvincibilityOn = true;
            flashEffect.Flash();
            audioPlayer.PlayPlayerDamaged();

            StartCoroutine(TriggerDamageCooldown());
        }
    }

    private void Death()
    {
        particlesPlayer.PlayDeathEffect(transform.position);
        FindObjectOfType<EnemySpawner>().StopSpawner();
        FindObjectOfType<FadeToBlack>().InitializeFade();

        gameInfo.IncrementDeaths();
        gameUI.SetDeathText();

        var enemies = FindObjectsOfType<Enemy>();
        foreach (var enemy in enemies)
            enemy.OnPlayerDeath();

        Destroy(gameObject);
        // gameManager.LoadUpgradeMenu();
    }

    private IEnumerator TriggerDamageCooldown()
    {
        yield return new WaitForSecondsRealtime(DamageCooldown);
        isInvincibilityOn = false;
    }
}
