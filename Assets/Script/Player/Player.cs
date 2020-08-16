using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

// TODO: Create a Character to handle common parameters this.

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
    /// Is the player executing a jump movement?
    /// </summary>
    private bool isJumping;

    /// <summary>
    /// If the current sprite is facing right.
    /// Makes sure that the player sprite is facing the same direction
    /// it is moving to.
    /// </summary>
    private bool isFacingRight = true;

    /// <summary>
    /// Is the player alive?
    /// Used to control what the player can and cannot do.
    /// </summary>
    private bool isAlive = true;

    /// <summary>
    /// The current level was completed.
    /// </summary>
    private bool levelCompleted = false;

    /// <summary>
    /// If the Player runned out of time.
    /// </summary>
    private bool timeIsOver = false;

    /// <summary>
    /// The player's physics body.
    /// </summary>
    private Rigidbody2D rigidBody;

    /// <summary>
    /// The animator for the player.
    /// </summary>
    private Animator animator;

    public AudioClip fxWin;
    public AudioClip fxDie;
    public AudioClip fxJump;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (Input.GetButtonDown(Constants.Input.Keys.jump) && isTouchingGround)
        {
            isJumping = true;
            if (isAlive && !levelCompleted)
            {
                SoundManager.instance.PlayFxPlayer(fxJump);
            }
        }

        if (((int)GameManager.instance.time <= 0) && !timeIsOver) // TODO: Fix. This logic is bizarre and weird.
        {
            timeIsOver = true;
            PlayerDie();
        }

        PlayAnimations();
    }

    private void FixedUpdate()
    {
        if (isAlive && !levelCompleted) // TODO: why not call levelCompleted to something like `allowedToMove` or `freeze` ?!?
        {
            float move = Input.GetAxis(Constants.Input.Axis.horizontal);
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
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
        }
    }










    /// <summary>
    /// Executes the player's animation, taking into consideration the player vars.
    /// </summary>
    void PlayAnimations()
    {
        if (levelCompleted)
        {
            animator.Play(Animations.celebrate);
        }
        else if (!isAlive)
        {
            animator.Play(Animations.die);
        }
        else if (isTouchingGround && rigidBody.velocity.x != 0)
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

    //
    // Collisions
    //

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
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
    /// The current level was completed
    /// </summary>
    public void LevelCompleted()
    {
        levelCompleted = true;
    }

    /// <summary>
    /// Player died.
    /// </summary>
    public void PlayerDie()
    {
        isAlive = false;
    }

    /// <summary>
    /// Die animation finished.
    /// </summary>
    void DieAnimationFinished()
    {
        if (timeIsOver)
        {
            GameManager.instance.SetOverlay(GameManager.GameStatus.LOSE);  // TODO: Delegate this to the game manager.
        }
        else
        {
            GameManager.instance.SetOverlay(GameManager.GameStatus.DIE);   // TODO: Delegate this to the game manager.
        }
    }

    /// <summary>
    /// Celebrate animation finished.
    /// </summary>
    void CelebrationAnimationFinished()
    {
        GameManager.instance.SetOverlay(GameManager.GameStatus.WIN);       // TODO: Delegate this to the game manager.
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

}
