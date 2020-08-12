using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public LayerMask groundLayer;

    /// <summary>
    /// Radius for ground check evaluation.
    /// </summary>
    public float radiusCheck;

    /// <summary>
    /// Is the player touching the ground.
    /// </summary>
    private bool isTouchingGround;

    /// <summary>
    /// Is the player jumping;
    /// </summary>
    private bool isJumping;

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
    /// The animator for the player.
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Frame-based update. Called once per frame.
    /// </summary>
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown(InputKeys.jump) && isTouchingGround)
        {
            isJumping = true;
        }

        PlayAnimations();
    }

    /// <summary>
    /// Physics related updates.
    /// </summary>
    void FixedUpdate()
    {
        float move = 0f;

        move = Input.GetAxis(InputAxis.horizontal);

        rigidBody.velocity = new Vector2(move * speed, rigidBody.velocity.y);

        if ((move < 0 && isFacingRight) || (move > 0 && !isFacingRight))
        {
            Flip();
        }

        if (isJumping)
        {
            rigidBody.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    /// <summary>
    /// Executes the player's animation, taking into consideration the player vars.
    /// </summary>
    void PlayAnimations()
    {
        if (isTouchingGround && rigidBody.velocity.x != 0)
        {
            animator.Play(Animations.run);
        }
        else if (isTouchingGround && rigidBody.velocity.x == 0)
        {
            animator.Play(Animations.idle);
        }
        else if (!isTouchingGround)
        {
            animator.Play(Animations.jump);
        }
    }

    /// <summary>
    /// Switches the side the player is facing.
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// Player animations.
    /// </summary>
    static class Animations
    {
        public const string celebrate = "Celebrate";
        public const string die = "Die";
        public const string idle = "Idle";
        public const string jump = "Jump";
        public const string run = "Run";
    }

    /// <summary>
    /// Keys used by the player.
    /// </summary>
    static class InputKeys
    {
        public const string jump = "Jump";
    }

    /// <summary>
    /// Axis used by the player.
    /// </summary>
    static class InputAxis
    {
        public const string horizontal = "Horizontal";
    }

}
