using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    private AudioPlayer audioPlayer;
    private GameObject fireball;
    private Transform fireballSpawnPos;

    private GameUI gameUI;
    private PlayerState playerState;
    private bool canCast = true;
    private bool isHoldingDownFire = false;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        gameUI = FindObjectOfType<GameUI>();
        playerState = GetComponent<PlayerState>();
        fireball = Resources.Load("Prefabs/Fireball") as GameObject;
        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;
    }

    private void Update()
    {
        if (canCast && isHoldingDownFire)
        {
            audioPlayer.PlayShoot();
            canCast = false;
            gameUI.UpdateFireballBar(true);
            Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
            StartCoroutine(CastingCooldown());
        }
    }

    private void OnFire(InputValue value)
    {
        isHoldingDownFire = value.Get<float>() > 0;
    }

    private IEnumerator CastingCooldown()
    {
        yield return new WaitForSecondsRealtime(playerState.FiringRate);
        canCast = true;
        gameUI.UpdateFireballBar(false);
    }
}
