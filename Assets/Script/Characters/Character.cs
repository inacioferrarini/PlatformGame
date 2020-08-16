using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A Character
/// </summary>
public class Character : MonoBehaviour
{
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float speed;

    /// <summary>
    /// Used to check if the player is touching the ground.
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
    protected bool grounded;

    /// <summary>
    /// If the current sprite is facing right.
    /// Makes sure that the player sprite is facing the same direction
    /// it is moving to.
    /// </summary>
    protected bool isFacingRight = true;

    /// <summary>
    /// The player's physics body.
    /// </summary>
    protected Rigidbody2D rigidBody;

    /// <summary>
    /// The animator for the player.
    /// </summary>
    protected Animator animator;

    protected void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
