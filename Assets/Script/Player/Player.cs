using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script user to control the player.
/// </summary>
public class Player : MonoBehaviour
{
    /// <summary>
    /// Movement speed.
    /// </summary>
    public float speed;

    /// <summary>
    /// Force to be applied to the rigid body upon jump.
    /// </summary>
    public int jumpForce;

    /// <summary>
    /// Used to check if the player is touching the ground.
    /// </summary>
    public Transform groundCheck;

    /// <summary>
    /// Reference layer for the ground.
    /// </summary>
    public LayerMask groundLayer;       // FIX: was layerGround

    /// <summary>
    /// Radius for ground check evaluation.
    /// </summary>
    public float radiusCheck;

    /// <summary>
    /// Is the player touching the ground?
    /// </summary>
    private bool isTouchingGround;     // FIX: was grounded

    /// <summary>
    /// Is the player jumping;
    /// </summary>
    private bool isJumping;

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {

    }

    /// <summary>
    /// Frame-based update. Called once per frame
    /// </summary>
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    /// <summary>
    /// Physics related updates
    /// </summary>
    void FixedUpdate()
    {

    }

}
