using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    private const float baseSpeed = 9.5f;
    private const float fireballLifespan = 7.5f;
    private float moveSpeed;

    private PlayerMovement player;
    private Rigidbody2D rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();

        if (player.IsFacingRight)
        {
            moveSpeed = baseSpeed;
            transform.localScale = new Vector2(1, 1);
        }
        else
        {
            moveSpeed = -baseSpeed;
            transform.localScale = new Vector2(-1, 1);
        }

        Destroy(gameObject, fireballLifespan);
    }

    private void Update()
    {
        rigidBody.velocity = new Vector2(moveSpeed, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponentInParent<Enemy>().DamageHealth();
            Destroy(gameObject);
        }
    }
}
