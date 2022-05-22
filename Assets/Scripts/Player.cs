using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    const float moveSpeed = 5.5f;
    const float jumpForce = 11.5f;

    CapsuleCollider2D playerCollider;
    Rigidbody2D rigidBody;
    Vector2 rawInput;

    void Awake()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    void OnMove(InputValue value)
    {
        rawInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rigidBody.velocity += new Vector2(rigidBody.velocity.x, jumpForce);
            // Add jump sound effect in the future
        }
    }

    void Move()
    {
        Vector3 delta = rawInput * moveSpeed * Time.deltaTime;
        transform.position += delta;
    }
}
