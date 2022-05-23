using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] GameObject fireball;

    const float moveSpeed = 5.5f;
    const float jumpForce = 11.5f;

    Animator animator;
    BoxCollider2D playerCollider;
    Rigidbody2D rigidBody;
    Vector2 rawInput;

    Transform fireballSpawnPos;
    public bool IsFacingRight { get; private set; }

    void Awake()
    {
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        fireballSpawnPos = GameObject.Find("ProjectileSpawn").transform;
        IsFacingRight = true;
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpForce);
            // Add jump sound effect in the future
        }
    }

    void OnFire()
    {
        Instantiate(fireball, fireballSpawnPos.position, transform.rotation);
        // Add fireball sound effect in the future
    }

    void FlipSprite(bool isMoving)
    {
        if (isMoving)
        {
            var inputValue = Mathf.Sign(rawInput.x);
            IsFacingRight = inputValue >= 1;
            transform.localScale = new Vector2(inputValue, 1);
        }
    }

    void Move()
    {
        var speed = rawInput.x * moveSpeed;

        Vector2 playerVelocity = new Vector2(speed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        animator.SetBool("Moving", Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon);
        FlipSprite(Mathf.Abs(speed) > Mathf.Epsilon);
    }
}
