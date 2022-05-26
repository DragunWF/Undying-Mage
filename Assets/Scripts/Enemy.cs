using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int health = 50;
    [SerializeField] int damage = 15;
    [SerializeField] float speed = 1.1f;

    [Header("Score Gain")]
    [SerializeField] int minScoreGain = 10;
    [SerializeField] int maxScoreGain = 50;

    PlayerState playerState;
    PlayerMovement playerMovement;
    int playerDamage;

    public void DamageHealth()
    {
        health -= playerDamage;
        if (health <= 0)
        {
            FindObjectOfType<InfoKeeper>().IncreaseScore(GetScore());
            Destroy(gameObject);
        }
    }

    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        playerState = FindObjectOfType<PlayerState>();
        playerDamage = playerState.FireballDamage;
    }

    void Update()
    {
        var playerPosX = playerMovement.GetPosition().x;
        transform.position = Vector2.MoveTowards(transform.position,
                                                 new Vector2(playerPosX,
                                                            transform.position.y),
                                                 speed * Time.deltaTime);
        FlipSprite(playerPosX);
    }

    void FlipSprite(float playerPosX)
    {
        transform.localScale = playerPosX >= transform.position.x ?
                               new Vector2(1, 1) : new Vector2(-1, 1);
    }

    int GetScore()
    {
        return Random.Range(minScoreGain, maxScoreGain);
    }
}
