using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int health = 100;

    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    public float DamageCooldown { get; private set; }

    private bool isInvincibilityOn;

    private FlashEffect flashEffect;
    private AudioPlayer audioPlayer;

    private GameInfo gameInfo;
    private GameManager gameManager;

    private void Awake()
    {
        flashEffect = GetComponent<FlashEffect>();
        audioPlayer = FindObjectOfType<AudioPlayer>();

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
            health -= damageAmount;
            if (health <= 0)
                Death();

            isInvincibilityOn = true;
            flashEffect.Flash();
            audioPlayer.PlayPlayerDamaged();

            StartCoroutine(TriggerDamageCooldown());
        }
    }

    private void Death()
    {
        gameManager.LoadUpgradeMenu();
    }

    private IEnumerator TriggerDamageCooldown()
    {
        yield return new WaitForSecondsRealtime(DamageCooldown);
        isInvincibilityOn = false;
    }
}
