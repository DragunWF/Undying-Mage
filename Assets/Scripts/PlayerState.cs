using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int health = 100;
    bool isInvincibilityOn;

    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    public float DamageCooldown { get; private set; }

    FlashEffect flashEffect;
    int deaths;

    void Awake()
    {
        MoveSpeed = 5.5f;
        JumpForce = 11.5f;

        FireballDamage = 25;
        FiringRate = 1.2f;

        DamageCooldown = 1.5f;

        flashEffect = GetComponent<FlashEffect>();
        deaths = 0;
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
            StartCoroutine(TriggerDamageCooldown());
        }
    }

    IEnumerator TriggerDamageCooldown()
    {
        yield return new WaitForSecondsRealtime(DamageCooldown);
        isInvincibilityOn = false;
    }
}
