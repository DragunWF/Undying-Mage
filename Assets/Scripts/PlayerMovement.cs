using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerState playerState;

    BoxCollider2D playerCollider;
    Rigidbody2D rigidBody;
    Vector2 rawInput;
    Animator animator;

    public bool IsFacingRight { get; private set; }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    void Awake()
    {
        playerState = GetComponent<PlayerState>();

        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

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
            rigidBody.velocity += new Vector2(rigidBody.velocity.x, playerState.JumpForce);
            // Add jump sound effect in the future
        }
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
        var speed = rawInput.x * playerState.MoveSpeed;

        Vector2 playerVelocity = new Vector2(speed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        animator.SetBool("Moving", Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon);
        FlipSprite(Mathf.Abs(speed) > Mathf.Epsilon);
    }
}
