using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    private AudioClip damage;
    private const float damageVolume = 0.75f;

    private AudioClip jump;
    private const float jumpVolume = 0.45f;

    private AudioClip shoot;
    private const float shootVolume = 0.65f;

    private AudioClip buttonClick;
    private const float buttonClickVolume = 1.25f;

    private AudioClip upgrade;
    private const float upgradeVolume = 1.25f;

    private AudioClip error;
    private const float errorVolume = 0.85f;

    #region Audio Play Methods

    public void PlayDamage()
    {
        PlayClip(damage, damageVolume);
    }

    public void PlayJump()
    {
        PlayClip(jump, jumpVolume);
    }

    public void PlayShoot()
    {
        PlayClip(shoot, shootVolume);
    }

    public void PlayButtonClick()
    {
        PlayClip(buttonClick, buttonClickVolume);
    }

    public void PlayUpgrade()
    {
        PlayClip(upgrade, upgradeVolume);
    }

    public void PlayError()
    {
        PlayClip(error, errorVolume);
    }

    #endregion

    private void Awake()
    {
        damage = Resources.Load("Audio/Damage") as AudioClip;
        jump = Resources.Load("Audio/Jump") as AudioClip;
        shoot = Resources.Load("Audio/Shoot") as AudioClip;
        buttonClick = Resources.Load("Audio/Click") as AudioClip;
        upgrade = Resources.Load("Audio/Upgrade") as AudioClip;
        error = Resources.Load("Audio/Error") as AudioClip;
    }

    private void PlayClip(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            Vector2 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}
