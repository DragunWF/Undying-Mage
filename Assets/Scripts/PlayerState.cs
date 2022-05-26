using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float MoveSpeed { get; private set; }
    public float JumpForce { get; private set; }

    public int Deaths { get; private set; }
    public int FireballDamage { get; private set; }
    public float FiringRate { get; private set; }

    void Awake()
    {
        MoveSpeed = 5.5f;
        JumpForce = 11.5f;

        Deaths = 0;
        FireballDamage = 25;
        FiringRate = 1.2f;
    }
}
