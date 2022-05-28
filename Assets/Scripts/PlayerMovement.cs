using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private PlayerState playerState;

    private BoxCollider2D playerCollider;
    private Rigidbody2D rigidBody;
    private Vector2 rawInput;
    private Animator animator;

    public bool IsFacingRight { get; private set; }

    public Vector2 GetPosition()
    {
        return transform.position;
    }

    private void Awake()
    {
        playerState = GetComponent<PlayerState>();

        animator = GetComponent<Animator>();
        playerCollider = GetComponent<BoxCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();

        IsFacingRight = true;
    }

    private void Update()
    {
        Move();
    }

    private void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    private void OnJump()
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigidBody.velocity += new Vector2(rigidBody.velocity.x, playerState.JumpForce);
            // Add jump sound effect in the future
        }
    }

    private void FlipSprite(bool isMoving)
    {
        if (isMoving)
        {
            var inputValue = Mathf.Sign(rawInput.x);
            IsFacingRight = inputValue >= 1;
            transform.localScale = new Vector2(inputValue, 1);
        }
    }

    private void Move()
    {
        var speed = rawInput.x * playerState.MoveSpeed;

        Vector2 playerVelocity = new Vector2(speed, rigidBody.velocity.y);
        rigidBody.velocity = playerVelocity;

        animator.SetBool("Moving", Mathf.Abs(rigidBody.velocity.x) > Mathf.Epsilon);
        FlipSprite(Mathf.Abs(speed) > Mathf.Epsilon);
    }
}
