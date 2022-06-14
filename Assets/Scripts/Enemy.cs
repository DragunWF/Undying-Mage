using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] float health = 50;
    [SerializeField] float damage = 15;
    [SerializeField] float moveSpeed = 1.1f;

    [Header("Score Gain")]
    [SerializeField] int minScoreGain = 10;
    [SerializeField] int maxScoreGain = 50;

    [Header("Enemy Type")]
    [SerializeField] bool isFlyingEnemy;

    public float DamageCooldown { get; private set; }

    private PlayerState playerState;
    private PlayerMovement playerMovement;
    private EnemySpawner enemySpawner;

    private AudioPlayer audioPlayer;
    private FlashEffect flashEffect;
    private int playerDamage;

    #region Player Death Fields
    private bool playerAlive = true;
    private bool finalFlip;
    private float lastPlayerPosX;
    #endregion

    #region Flying Enemy Only Fields
    private int lifespan = 15;
    private int xAxis;
    #endregion

    public void DamageHealth()
    {
        audioPlayer.PlayDamage();
        health -= playerDamage;
        flashEffect.Flash();

        if (health <= 0)
        {
            FindObjectOfType<ParticlesPlayer>().PlayDeathEffect(transform.position);
            FindObjectOfType<GameInfo>().IncreaseScore(GetScore());
            Destroy(gameObject);
        }
    }

    public void OnPlayerDeath()
    {
        playerAlive = false;
    }

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        flashEffect = GetComponent<FlashEffect>();
        DamageCooldown = 0.25f;

        playerMovement = FindObjectOfType<PlayerMovement>();
        playerState = FindObjectOfType<PlayerState>();
        playerDamage = playerState.FireballDamage;
    }

    private void Start()
    {
        SetStats();
        if (isFlyingEnemy)
        {
            PickFlyingDirection();
            FlipSprite(playerMovement.GetPosition().x);
            Destroy(gameObject, lifespan);
        }
    }

    private void Update()
    {
        Move();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
            playerState.DamageHealth(Mathf.RoundToInt(damage));
    }

    private void SetStats()
    {
        var intensity = enemySpawner.GetIntensity();

        damage += damage * (intensity * 0.1f);
        health += health * (intensity * 0.5f);

        const int maxDamage = 50;
        if (damage > maxDamage)
            damage = maxDamage;

        if (!isFlyingEnemy)
        {
            moveSpeed += intensity * 0.1f;

            const int maxSpeed = 10;
            if (moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }
    }

    private void Move()
    {
        var speed = moveSpeed * Time.deltaTime;
        Vector2 direction;

        if (!isFlyingEnemy)
        {
            if (playerAlive)
            {
                var playerPosX = playerMovement.GetPosition().x;
                direction = new Vector2(playerPosX, transform.position.y);
                lastPlayerPosX = playerPosX;
                FlipSprite(playerPosX);
            }
            else
            {
                var oppositeDirection = lastPlayerPosX >= 0 ? -1 : 1;
                var destination = 1000 * oppositeDirection;
                direction = new Vector2(destination, transform.position.y);

                FlipSprite(oppositeDirection);
                finalFlip = true;
            }
        }
        else
            direction = new Vector2(xAxis * 1000, transform.position.y);

        transform.position = Vector2.MoveTowards(transform.position, direction, speed);
    }

    private void FlipSprite(float playerPosX)
    {
        if (!finalFlip)
            transform.localScale = playerPosX >= transform.position.x ?
                                   new Vector2(1, 1) : new Vector2(-1, 1);
    }

    private void PickFlyingDirection()
    {
        var playerPosX = playerMovement.GetPosition().x;
        xAxis = playerPosX >= transform.position.x ? 1 : -1;
    }

    private int GetScore()
    {
        return Random.Range(minScoreGain, maxScoreGain);
    }
}
