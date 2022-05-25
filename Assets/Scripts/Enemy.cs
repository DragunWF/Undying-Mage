using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 50;
    Rigidbody2D rigidBody;

    const float moveSpeed = 1.1f;

    Player player;
    int playerDamage;

    public void DamageHealth()
    {
        health -= playerDamage;
        if (health <= 0)
            Destroy(gameObject);
    }

    void Awake()
    {
        player = FindObjectOfType<Player>();
        playerDamage = player.FireballDamage;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var playerPosX = player.GetPosition().x;
        transform.position = Vector2.MoveTowards(transform.position,
                                                 new Vector2(playerPosX,
                                                            transform.position.y),
                                                 moveSpeed * Time.deltaTime);
        FlipSprite(playerPosX);
    }

    void FlipSprite(float playerPosX)
    {
        transform.localScale = playerPosX >= transform.position.x ?
                               new Vector2(1, 1) : new Vector2(-1, 1);
    }
}
