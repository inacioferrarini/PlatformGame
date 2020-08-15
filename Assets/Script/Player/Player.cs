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

    /// <summary>
    /// Physics related updates.
    /// </summary>
    void FixedUpdate()
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

    /// <summary>
    /// Switches the side the player is facing.
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }


    /// <summary>
    /// When the player collides with an object that is not a trigger.
    /// </summary>
    /// <param name="collision">The collision source</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: Delegate this to the game manager.
        if (collision.gameObject.CompareTag(Constants.Collision.Tags.enemy))
        {
            PlayerDie();
        }
    }

    /// <summary>
    /// Player died.
    /// </summary>
    void PlayerDie()
    {
        // TODO: Delegate this to the game manager.
        isAlive = false;
        Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy);
        SoundManager.instance.PlayFxPlayer(fxDie);
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
    /// 
    /// </summary>
    void CelebrationAnimationFinished()
    {
        GameManager.instance.SetOverlay(GameManager.GameStatus.WIN);       // TODO: Delegate this to the game manager.
    }

    /// <summary>
    /// When the player collides with a trigger.
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        // TODO: Delegate this to the game manager.
        if (other.CompareTag(Constants.Collision.Tags.exit))
        {
            levelCompleted = true;
            SoundManager.instance.PlayFxPlayer(fxWin);
        }
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
