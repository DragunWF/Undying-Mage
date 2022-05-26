using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    GameObject fireball;

    bool canCast = true;
    float castCooldown = 0.75f;

    Transform fireballSpawnPos;
    public int FireballDamage { get; private set; }

    void Awake()
    {
        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;

        FireballDamage = 25;
    }

    void OnFire()
    {
        if (canCast)
        {
            Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
            canCast = false;
            StartCoroutine(CastingCooldown());
            // Add fireball sound effect in the future
        }
    }

    IEnumerator CastingCooldown()
    {
        yield return new WaitForSecondsRealtime(castCooldown);
        canCast = true;
    }
}
