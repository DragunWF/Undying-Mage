using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 50;
    Rigidbody2D rigidBody;

    int playerDamage;

    public void DamageHealth()
    {
        health -= playerDamage;
        if (health <= 0)
            Destroy(gameObject);
    }

    void Start()
    {
        playerDamage = FindObjectOfType<Player>().FireballDamage;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 movement = new Vector2(1, rigidBody.velocity.y);
        rigidBody.velocity = movement;
    }
}
