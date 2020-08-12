﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for Patroller enemy.
/// </summary>
public class Patroller : MonoBehaviour
{
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float speed;

    /// <summary>
    /// Used to check if the enemy is touching the ground.
    /// </summary>
    public Transform groundCheck;

    /// <summary>
    /// Reference layer for the ground.
    /// </summary>
    public LayerMask groundLayer;

    /// <summary>
    /// Radius for ground check evaluation.
    /// </summary>
    public float radiusCheck;

    /// <summary>
    /// Is the enemy touching the ground?
    /// </summary>
    private bool isTouchingGround;

    /// <summary>
    /// If the current sprite is facing right.
    /// Makes sure that the player sprite is facing the same direction
    /// it is moving to.
    /// </summary>
    private bool isFacingRight = true;

    /// <summary>
    /// The player's physics body.
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// The animator for the enemy.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Is visible on screen?
    /// </summary>
    private bool isVisible = false;

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Frame-based update. Called once per frame
    /// </summary>
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!isTouchingGround)
        {
            Flip();
        }
    }

    /// <summary>
    /// Physics related updates
    /// </summary>
    void FixedUpdate()
    {
        if (isVisible)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }

    void OnBecameVisible()
    {
        Invoke("MoveEnemy", 3f);
    }

    void OnBecameInvisible()
    {
        Invoke("StopEnemy", 3f);
    }

    void MoveEnemy()
    {
        isVisible = true;
        animator.Play("Run");
    }

    void StopEnemy()
    {
        isVisible = false;
        animator.Play("Idle");
    }

}
