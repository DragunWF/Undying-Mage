using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private GameObject fireball;
    private Transform fireballSpawnPos;

    private PlayerState playerState;
    private bool canCast = true;

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();
        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;
    }

    private void OnFire()
    {
        if (canCast)
        {
            canCast = false;
            Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
            StartCoroutine(CastingCooldown());
            // Add fireball sound effect in the future
        }
    }

    private IEnumerator CastingCooldown()
    {
        yield return new WaitForSecondsRealtime(playerState.FiringRate);
        canCast = true;
    }
}
