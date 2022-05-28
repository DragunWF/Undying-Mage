using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioClip playerDamaged;
    private const float playerDamagedVolume = 0.3f;

    private void Awake()
    {
        playerDamaged = Resources.Load("Audio/Damage") as AudioClip;
    }

    public void PlayPlayerDamaged()
    {
        PlayClip(playerDamaged, playerDamagedVolume);
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
