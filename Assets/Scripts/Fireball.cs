using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    const float baseSpeed = 9.5f;
    const float fireballLifespan = 7.5f;
    float moveSpeed;

    Player player;
    Rigidbody2D rigidBody;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();

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

        StartCoroutine(ExtinguishFlames());
    }

    void Update()
    {
        rigidBody.velocity = new Vector2(moveSpeed, 0);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
            Destroy(gameObject);
    }

    IEnumerator ExtinguishFlames()
    {
        yield return new WaitForSecondsRealtime(fireballLifespan);
        Destroy(gameObject);
    }
}
