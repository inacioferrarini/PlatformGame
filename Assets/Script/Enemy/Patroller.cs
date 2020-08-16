using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for patroller objects.
/// A patroller is an object that moves from one side to the other.
/// </summary>
public class Patroller : MonoBehaviour
{
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float speed;

    /// <summary>
    /// Used to check if the patroller is touching the ground.
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
    /// Is the patroller touching the ground?
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
    /// The animator for the patroller.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Is visible on screen?
    /// </summary>
    private bool isVisible = false;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!isTouchingGround)
        {
            Flip();
        }
    }

    private void FixedUpdate()
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

    /// <summary>
    /// Switches the side the patroller is facing.
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
    }

    /// <summary>
    /// The patroller has became visible in the game.
    /// </summary>
    void OnBecameVisible()
    {
        Invoke("MoveEnemy", 3f);
    }

    /// <summary>
    /// The patroller is no longer visible in the game.
    /// </summary>
    void OnBecameInvisible()
    {
        Invoke("StopEnemy", 3f);
    }

    /// <summary>
    /// Stars moving the patroller.
    /// </summary>
    void MoveEnemy()
    {
        isVisible = true;
        animator.Play("Run");
    }

    /// <summary>
    /// Stops moving the patroller.
    /// </summary>
    void StopEnemy()
    {
        isVisible = false;
        animator.Play("Idle");
    }

}
