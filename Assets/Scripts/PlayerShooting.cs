using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    private AudioPlayer audioPlayer;
    private GameObject fireball;
    private Transform fireballSpawnPos;

    private GameUI gameUI;
    private PlayerState playerState;
    private bool canCast = true;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameUI = FindObjectOfType<GameUI>();
        playerState = GetComponent<PlayerState>();
        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;
    }

    private void OnFire()
    {
        if (canCast)
        {
            audioPlayer.PlayShoot();
            canCast = false;
            gameUI.UpdateFireballBar(true);
            Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
            StartCoroutine(CastingCooldown());
        }
    }

    private IEnumerator CastingCooldown()
    {
        yield return new WaitForSecondsRealtime(playerState.FiringRate);
        canCast = true;
        gameUI.UpdateFireballBar(false);
    }
}
