using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    GameObject fireball;
    Transform fireballSpawnPos;

    PlayerState playerState;
    bool canCast = true;

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
            canCast = false;
            Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
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
