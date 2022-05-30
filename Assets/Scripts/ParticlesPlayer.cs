using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    private GameObject hitEffect;
    private GameObject deathEffect;
    private const float effectLifespan = 2.5f;

    public void PlayHitEffect(Vector2 pos)
    {
        PlayEffect(pos, hitEffect);
    }

    public void PlayDeathEffect(Vector2 pos)
    {
        PlayEffect(pos, deathEffect);
    }

    private void Awake()
    {
        hitEffect = Resources.Load("Prefabs/HitEffect") as GameObject;
        deathEffect = Resources.Load("Prefabs/DeathEffect") as GameObject;
    }

    private void PlayEffect(Vector2 pos, GameObject effect)
    {
        var instance = Instantiate(effect, pos, Quaternion.identity);
        Destroy(instance.gameObject, effectLifespan);
    }
}
