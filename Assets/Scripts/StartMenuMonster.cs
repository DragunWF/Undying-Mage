using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMonster : MonoBehaviour
{
    private const float moveSpeed = -1.5f;
    private Rigidbody2D rigidBody;

    private Animator[] animators;

    private void Awake()
    {
        // animators = new Animator[4] {
        //     Resources.Load("Animations/")
        // }
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        var moveVelocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = moveVelocity;
    }
}
