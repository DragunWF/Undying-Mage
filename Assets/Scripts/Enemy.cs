using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] int health = 50;
    [SerializeField] int damage = 15;
    [SerializeField] float moveSpeed = 1.1f;

    [Header("Score Gain")]
    [SerializeField] int minScoreGain = 10;
    [SerializeField] int maxScoreGain = 50;

    [Header("Enemy Type")]
    [SerializeField] bool isFlyingEnemy;

    PlayerState playerState;
    PlayerMovement playerMovement;
    int playerDamage;

    // Only for flying enemies
    Rigidbody2D rigidBody;
    int lifeSpan;
    int xAxis;

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

    void Start()
    {
        if (isFlyingEnemy)
        {
            rigidBody = GetComponent<Rigidbody2D>();
            PickFlyingDirection();
            FlipSprite(playerMovement.GetPosition().x);
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        if (!isFlyingEnemy)
        {
            var playerPosX = playerMovement.GetPosition().x;
            transform.position = Vector2.MoveTowards(transform.position,
                                                    new Vector2(playerPosX,
                                                                transform.position.y),
                                                    moveSpeed * Time.deltaTime);
            FlipSprite(playerPosX);
        }
        else
        {
            var speed = Time.deltaTime * moveSpeed * xAxis;
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
    }

    void FlipSprite(float playerPosX)
    {
        transform.localScale = playerPosX >= transform.position.x ?
                               new Vector2(1, 1) : new Vector2(-1, 1);
    }

    void PickFlyingDirection()
    {
        var playerPosX = playerMovement.GetPosition().x;
        xAxis = playerPosX >= transform.position.x ? 1 : -1;
    }

    int GetScore()
    {
        return Random.Range(minScoreGain, maxScoreGain);
    }
}
