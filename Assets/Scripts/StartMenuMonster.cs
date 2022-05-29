using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMonster : MonoBehaviour
{
    private float[] moveSpeeds = new float[3] { -1.5f, -2.5f, -3.5f };
    private float chosenMoveSpeed;
    private const float lifespan = 16.5f;
    private Rigidbody2D rigidBody;

    private Animator animator;
    private RuntimeAnimatorController[] animators;

    private void Awake()
    {
        animators = new RuntimeAnimatorController[4] {
            Resources.Load("Animations/Ground Enemies/Crawler Animator") as RuntimeAnimatorController,
            Resources.Load("Animations/Ground Enemies/Fighter Animator") as RuntimeAnimatorController,
            Resources.Load("Animations/Ground Enemies/Viking Animator") as RuntimeAnimatorController,
            Resources.Load("Animations/Ground Enemies/Spider Animator") as RuntimeAnimatorController
        };
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        RuntimeAnimatorController chosenController;
        chosenController = animators[Random.Range(0, animators.Length)];
        animator.runtimeAnimatorController = chosenController;
        chosenMoveSpeed = moveSpeeds[Random.Range(0, moveSpeeds.Length)];

        Destroy(gameObject, lifespan);
    }

    private void Update()
    {
        var moveVelocity = new Vector2(chosenMoveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = moveVelocity;
    }
}
