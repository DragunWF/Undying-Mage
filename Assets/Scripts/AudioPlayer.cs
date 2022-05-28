using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    AudioClip playerDamaged;
    const float playerDamagedVolume = 0.3f;

    void Awake()
    {
        playerDamaged = Resources.Load("Audio/Damage") as AudioClip;
    }

    public void PlayPlayerDamaged()
    {
        PlayClip(playerDamaged, playerDamagedVolume);
    }

    void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
