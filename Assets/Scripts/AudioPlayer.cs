using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioClip playerDamaged;
    private const float playerDamagedVolume = 0.3f;

    private AudioClip buttonClick;
    private const float buttonClickVolume = 1.25f;

    private AudioClip error;
    private const float errorVolume = 0.85f;

    public void PlayPlayerDamaged()
    {
        PlayClip(playerDamaged, playerDamagedVolume);
    }

    public void PlayButtonClick()
    {
        PlayClip(buttonClick, buttonClickVolume);
    }

    public void PlayError()
    {
        PlayClip(error, errorVolume);
    }

    private void Awake()
    {
        playerDamaged = Resources.Load("Audio/Damage") as AudioClip;
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
