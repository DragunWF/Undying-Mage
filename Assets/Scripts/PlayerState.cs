using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    [SerializeField] int health = 100;
    [SerializeField] float damageCooldownTime = 0.5f;
    bool isInvincibilityOn;

    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int Deaths { get; private set; }
    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    void Awake()
    {
        MoveSpeed = 5.5f;
        JumpForce = 11.5f;

        Deaths = 0;
        FireballDamage = 25;
        FiringRate = 1.2f;
    }

    public void DamageHealth(int damageAmount)
    {
        if (!isInvincibilityOn)
        {
            health -= damageAmount;
            isInvincibilityOn = true;
            StartCoroutine(TriggerDamageCooldown());
        }
    }

    IEnumerator TriggerDamageCooldown()
    {
        yield return new WaitForSecondsRealtime(damageCooldownTime);
        isInvincibilityOn = false;
    }
}
