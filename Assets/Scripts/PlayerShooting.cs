using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    GameObject fireball;
    Transform fireballSpawnPos;

    bool canCast = true;
    float castCooldown = 0.75f;

    PlayerState playerState;

    void Awake()
    {
        playerState = GetComponent<PlayerState>();
        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;
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
        yield return new WaitForSecondsRealtime(playerState.FiringRate);
        canCast = true;
    }
}
