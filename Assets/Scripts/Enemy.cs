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

        if (isFlyingEnemy)
        {
            lifeSpan = 15;
            rigidBody = GetComponent<Rigidbody2D>();
        }
    }

    void Start()
    {
        if (isFlyingEnemy)
        {
            PickFlyingDirection();
            FlipSprite(playerMovement.GetPosition().x);
            Invoke("ExpireFlyingEnemy", lifeSpan);
        }
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        var speed = moveSpeed * Time.deltaTime;
        Vector2 direction;

        if (!isFlyingEnemy)
        {
            var playerPosX = playerMovement.GetPosition().x;
            direction = new Vector2(playerPosX, transform.position.y);
            FlipSprite(playerPosX);
        }
        else
            direction = new Vector2(xAxis * 1000, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, direction, speed);
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

    void ExpireFlyingEnemy()
    {
        Destroy(gameObject);
    }

    int GetScore()
    {
        return Random.Range(minScoreGain, maxScoreGain);
    }
}
