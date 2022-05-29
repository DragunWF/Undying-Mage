using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuMonster : MonoBehaviour
{
    private const float moveSpeed = -1.5f;
    private const float lifespan = 8.5f;
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

        Invoke("DespawnMonster", lifespan);
    }

    private void Update()
    {
        var moveVelocity = new Vector2(moveSpeed, rigidBody.velocity.y);
        rigidBody.velocity = moveVelocity;
    }

    private void DespawnMonster()
    {
        Destroy(gameObject);
    }
}
